using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;
using ITDB.Models.Custom;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace ITDB.Controllers
{
    [Route("api/Order")]
    [Authorize]
    public class OrderController : BaseController
    {
        private IMemoryCache _cache;

        public OrderController(DBContext context, IMemoryCache memoryCache) : base(context)
        {
            _cache = memoryCache;
        }

        [HttpPost]
        public IActionResult Submit([FromBody]SubmitOrder model)
        {
            try
            {
                //防止重复下单
                DateTime cacheEntry;
                string cacheKey = "CacheKey" + CurrentUserID;
                // Look for cache key.
                if (!_cache.TryGetValue(cacheKey, out cacheEntry))
                {
                    // Key not in cache, so get data.
                    cacheEntry = DateTime.Now;

                    // Set cache options.
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        // Keep in cache for this time, reset time if accessed.
                        .SetSlidingExpiration(TimeSpan.FromSeconds(3));
                    // Save data in cache.
                    _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
                }
                else
                {
                    if ((DateTime.Now - cacheEntry).Seconds < 3)
                    {
                        throw new Exception("重复提交");
                    }
                }
                using (var tran = _context.Database.BeginTransaction())
                {
                    //1.查询购物车
                    //2.查询地址
                    //3.提交订单（余额支付）
                    //4.发起支付
                    var list = from a in _context.ShopCarts
                               where model.ShopCartID.Contains(a.ID)
                               select a;
                    if (list == null || list.Count() == 0)
                    {
                        throw new Exception("购物车商品不存在");
                    }

                    var address = _context.ShippingAddress.Find(model.AddressID);
                    if (address == null)
                    {
                        throw new Exception("地址信息不存在");
                    }
                    decimal total = list.Sum(s => s.Price * s.Num);


                    Order order = new Order()
                    {
                        ID = Guid.NewGuid(),
                        OrderCode = DateTime.Now.ToString("BDyyyyMMddHHmmssfff") + CurrentUserID.ToString().PadLeft(13, '0'),
                        IfPay = false,
                        SubmitTime = DateTime.Now,
                        TotalPrice = total,
                        UserID = CurrentUserID,
                        PostUserName = address.UserName,
                        PostUserPhone = address.UserPhone,
                        PostAddress = address.Address,
                        PostDetailAddress = address.DetailAddress,
                        OrderStatus = 0,//待支付
                        IpAddress = "",
                        IpCity = ""

                    };
                    //发起支付 使用余额支付
                    if (model.PayMode == 0)
                    {
                        var user = _context.Users.Find(CurrentUserID);
                        if (user.UserBalance < total)
                        {
                            throw new Exception("余额不足，请充值");
                        }
                        user.UserBalance -= total;
                        order.IfPay = true;
                    }
                    _context.Orders.Add(order);

                    foreach (var item in list)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            ID = Guid.NewGuid(),
                            OrderID = order.ID,
                            DBPeriodsID = item.DBPeriodsID,
                            DBNum = item.Num,
                            DBPrice = item.Num * item.Price,
                            DBPerPrice = item.Price
                        };
                        _context.OrderDetails.Add(orderDetail);
                        ///获取所有的已参与记录
                        var dBPeriods = _context.DBPeriods.Find(item.DBPeriodsID);
                        if (dBPeriods == null)
                        {
                            throw new Exception("商品不存在或已删除");
                        }
                        else if (dBPeriods != null && dBPeriods.Status != 0)
                        {
                            throw new Exception("商品人数已满，请选择下一期夺宝");
                        }
                        //已使用的号
                        var oldPeriod = _context.DBOrderDetails.Where(s => s.DBPeriodsID == item.DBPeriodsID);

                        int[] baseCode = ProduceTicket(dBPeriods.NeedNum);
                        int[] hasCode = oldPeriod.Select(s => s.DBTicket).ToArray();
                        var overplus = baseCode.Except(hasCode).ToList();

                        Random random = new Random();
                        List<DBOrderDetail> dBOrderDetails = new List<DBOrderDetail>();
                        for (int i = 0; i < item.Num; i++)
                        {
                            int index = random.Next(0, overplus.Count);//0到length-1
                            dBOrderDetails.Add(new DBOrderDetail()
                            {
                                ID = Guid.NewGuid(),
                                OrderDetailID = orderDetail.ID,
                                DBPeriodsID = item.DBPeriodsID,
                                DBTicket = overplus[index],
                                UserID = CurrentUserID
                            });
                            overplus.RemoveAt(index);
                        }
                        _context.DBOrderDetails.AddRange(dBOrderDetails);
                        dBPeriods.OverplusNum = dBPeriods.OverplusNum - item.Num;
                        //如果是余额支付直接开奖
                        if (model.PayMode == 0)
                        {
                            if (dBPeriods.OverplusNum == 0)
                            {
                                dBPeriods.WaitOpenTime = DateTime.Now.AddMinutes(10);
                                dBPeriods.Status = 1;//0 进行中 1正在开奖中2开奖成功3开奖失败
                                //OpenPeriods waitopen = new OpenPeriods();
                                //waitopen.WaitOpen(dBPeriods.ID);
                            }
                        }

                    }

                    ///0、1、2 余额、微信、支付宝
                    //发起支付 
                    if (model.PayMode == 1)
                    {
                        //TODO 微信
                    }
                    else if (model.PayMode == 2)
                    {
                        //TODO 支付宝
                    }
                    //user UserBalance 自动保存
                    _context.ShopCarts.RemoveRange(list);
                    _context.SaveChanges();
                    tran.Commit();
                }
                return new ObjectResult(FormatResult.Success("提交成功"));
            }
            catch (Exception e)
            {
                return new ObjectResult(FormatResult.Failure(e.Message));
            }
        }
        private int[] ProduceTicket(int length)
        {
            int[] t = new int[length];
            for (int i = 0; i < length; i++)
            {
                t[i] = i + 1;
            }
            return t;
        }

        [HttpPost("PayCallBack")]
        public IActionResult PayCallBack([FromBody]SubmitOrder model)
        {
            //if (dBPeriods.OverplusNum == 0)
            //{
            //    dBPeriods.WaitOpenTime = DateTime.Now.AddMinutes(10);
            //    dBPeriods.Status = 1;//0 进行中 1正在开奖中2开奖成功3开奖失败
            //    OpenPeriods waitopen = new OpenPeriods();
            //    waitopen.WaitOpen(dBPeriods.ID);
            //}
            return new ObjectResult(FormatResult.Failure("no imp..."));
        }
    }
}
