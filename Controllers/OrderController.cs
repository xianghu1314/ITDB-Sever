using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;
using ITDB.Models.Custom;
using System;
using Microsoft.AspNetCore.Authorization;
using ITDB.Tool;
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

                //1.查询购物车
                //2.查询地址
                //3.提交订单
                //4.发起支付
                var list = from a in _context.ShopCart
                           where model.ShopCartID.Contains(a.ID)
                           select a;
                if(list==null||list.Count()==0){
                    throw new Exception("购物车商品不存在");
                }

                var address = _context.ShippingAddress.Find(model.AddressID);
                if(address==null){
                    throw new Exception("地址信息不存在");
                }
                Order order = new Order()
                {
                    ID = Guid.NewGuid(),
                    OrderCode = DateTime.Now.ToString("BDyyyyMMddHHmmssfff") + CurrentUserID.ToString().PadLeft(13, '0'),
                    IfPay = false,
                    SubmitTime = DateTime.Now,
                    TotalPrice = list.Sum(s => s.Price * s.Num),
                    UserID = CurrentUserID,
                    PostUserName = address.UserName,
                    PostUserPhone = address.UserPhone,
                    PostAddress = address.Address,
                    PostDetailAddress = address.DetailAddress,
                    OrderStatus = 0,//待支付
                    IpAddress = "",
                    IpCity = ""

                };
                _context.Order.Add(order);
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
                    _context.OrderDetail.Add(orderDetail);
                    ///获取所有的已参与记录
                    var dBPeriods = _context.DBPeriods.Find(item.DBPeriodsID);
                    if(dBPeriods==null){
                        throw new Exception("商品不存在或已删除");
                    }
                    else if(dBPeriods!=null&&dBPeriods.Status!=0){
                        throw new Exception("商品人数已满，请选择下一期夺宝");
                    }
                    //已使用的号
                    var oldPeriod = _context.DBOrderDetail.Where(s => s.DBPeriodsID == item.DBPeriodsID);

                    int[] baseCode = ProduceTicket(dBPeriods.NeedNum);
                    int[] hasCode = oldPeriod.Select(s => s.DBTicket).ToArray();
                    var overplus = baseCode.Except(hasCode).ToList();

                    Random random = new Random();
                    for (int i = 0; i < item.Num; i++)
                    {
                        int index = random.Next(0, overplus.Count);//0到length-1
                        DBOrderDetail dBOrderDetail = new DBOrderDetail()
                        {
                            ID = Guid.NewGuid(),
                            OrderDetailID = orderDetail.ID,
                            DBPeriodsID = item.DBPeriodsID,
                            DBTicket = overplus[index],
                            UserID = CurrentUserID
                        };
                        _context.DBOrderDetail.Add(dBOrderDetail);
                        overplus.RemoveAt(index);
                    }
                    dBPeriods.OverplusNum = dBPeriods.OverplusNum - item.Num;
                    if (dBPeriods.OverplusNum == 0)
                    {
                        dBPeriods.WaitOpenTime = DateTime.Now.AddMinutes(10);
                        dBPeriods.Status = 1;//0 进行中 1正在开奖中2开奖成功3开奖失败
                        OpenPeriods waitopen = new OpenPeriods();
                        waitopen.WaitOpen(dBPeriods.ID);
                    }

                }
                _context.ShopCart.RemoveRange(list);
                _context.SaveChanges();
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
    }
}