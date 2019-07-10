using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITDB.Controllers
{
    [Route("api/ShopCart")]
    [Authorize]
    public class ShopCartController : BaseController
    {
        public ShopCartController(DBContext context) : base(context)
        {
        }
        [HttpGet("getShopCart")]
        public IActionResult getShopCart()
        {
            var list = from a in _context.ShopCarts
                       join b in _context.Goods on a.GoodsID equals b.ID
                       join c in _context.DBPeriods on a.DBPeriodsID equals c.ID
                       select new
                       {
                           a.DBPeriodsID,
                           a.GoodsID,
                           a.ID,
                           a.Num,
                           a.Price,
                           c.PeriodsCode,
                           b.GoodsLogo,
                           b.GoodsName,
                           b.GoodsDescribe
                       };
            return new ObjectResult(FormatResult.Success(list));
        }
        [HttpPost("join")]
        public IActionResult join([FromBody] ShopCart model)
        {
            var ifhave = _context.ShopCarts.FirstOrDefault(s => s.DBPeriodsID == model.DBPeriodsID && s.UserID == s.UserID);
            var dbPeriods = _context.DBPeriods.Find(model.DBPeriodsID);

            if (ifhave != null)
            {

                ifhave.Num += model.Num;
                if (dbPeriods.OverplusNum < ifhave.Num)
                {
                    return new ObjectResult(FormatResult.Failure("超过剩余次数，请到购物车查看！"));
                }
            }
            else
            {
                model.UserID = CurrentUserID;
                if (dbPeriods.OverplusNum < model.Num)
                {
                    return new ObjectResult(FormatResult.Failure("超过剩余次数，请刷新重试！"));
                }
                _context.ShopCarts.Add(model);
            }

            _context.SaveChanges();
            return new ObjectResult(FormatResult.Success("添加成功"));
        }
        [HttpPost("add")]
        public IActionResult add([FromBody] ShopCart model)
        {
            var ifhave = _context.ShopCarts.Find(model.ID);
            var dbPeriods = _context.DBPeriods.Find(ifhave.DBPeriodsID);
            if (ifhave != null)
            {
                ifhave.Num++;
                if (dbPeriods.OverplusNum < ifhave.Num)
                {
                    return new ObjectResult(FormatResult.Failure("超过剩余次数！"));
                }
                _context.SaveChanges();
            }
            return new ObjectResult(FormatResult.Success("操作成功"));
        }
        [HttpPost("sub")]
        public IActionResult sub([FromBody] ShopCart model)
        {
            var ifhave = _context.ShopCarts.Find(model.ID);
            if (ifhave != null && ifhave.Num - model.Num > 0)
            {
                ifhave.Num--;
                _context.SaveChanges();
            }
            return new ObjectResult(FormatResult.Success("操作成功"));
        }
        [HttpPost("delete")]
        public IActionResult delete([FromBody] IEnumerable<ShopCart> model)
        {
            _context.ShopCarts.RemoveRange(model);
            _context.SaveChanges();
            return new ObjectResult(FormatResult.Success("删除成功"));
        }
    }
}