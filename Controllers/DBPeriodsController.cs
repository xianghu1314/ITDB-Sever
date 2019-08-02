using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ITDB.Models.Custom;

namespace ITDB.Controllers
{
    [Route("api/DBPeriods")]
    public class DBPeriodsController : BaseController
    {
        public DBPeriodsController(DBContext context) : base(context)
        {

        }

        [HttpGet("{id}", Name = "GetDBPeriodsByID")]
        public IActionResult GetDBPeriodsByID(int id)
        {

            var item = (from db in _context.DBPeriods
                        join go in _context.Goods on db.GoodsID equals go.ID
                        where db.ID == id
                        select new
                        {
                            db.ID,
                            db.GoodsID,
                            db.GoodsPrice,
                            db.NeedNum,
                            db.OverplusNum,
                            db.PeriodsCode,
                            db.PerPrice,
                            go.GoodsName,
                            go.GoodsLogo,
                            go.GoodsDescribe,
                            go.GoodsLogo2,
                            go.GoodsDetail,
                        }).FirstOrDefault();

            return new ObjectResult(FormatResult.Success(item));
        }
        [HttpGet("GetDBPeriods")]
        public IActionResult GetDBPeriods(GetDBPeriodsRequest m)
        {
            var list = (from db in _context.DBPeriods
                       join go in _context.Goods on db.GoodsID equals go.ID
                        where m.CategoryId == 0 || go.CategoryID == m.CategoryId
                        select new
                       {
                           db.ID,
                           db.GoodsID,
                           db.GoodsPrice,
                           db.NeedNum,
                           db.OverplusNum,
                           db.PeriodsCode,
                           db.PerPrice,
                           go.GoodsName,
                           go.GoodsLogo,
                           go.GoodsDescribe
                       }).Take(m.PageSize).Skip(m.PageIndex).ToList();
            return new ObjectResult(FormatResult.Success(list));
        }
        [HttpGet("GetDBPeriodsByCID")]
        public IActionResult GetDBPeriodsByCID(int cid)
        {
            var list = from db in _context.DBPeriods
                       join go in _context.Goods on db.GoodsID equals go.ID
                       where go.CategoryID == cid
                       select new
                       {
                           db.ID,
                           db.GoodsID,
                           db.GoodsPrice,
                           db.NeedNum,
                           db.OverplusNum,
                           db.PeriodsCode,
                           db.PerPrice,
                           go.GoodsName,
                           go.GoodsLogo,
                           go.GoodsDescribe
                       };
            return new ObjectResult(FormatResult.Success(list));
        }
        /// <summary>
        /// 获取最新中奖
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        [HttpGet("GetNewList")]
        public IActionResult GetNewList(request re)
        {
            var list = from db in _context.DBPeriods
                       join go in _context.Goods on db.GoodsID equals go.ID
                       join u in _context.Users on db.LuckyUserID equals u.ID into u1 from user in u1.DefaultIfEmpty()

                       where db.Status != 0
                       select new
                       {
                           db.ID,
                           db.GoodsID,
                           db.GoodsPrice,
                           db.NeedNum,
                           db.OverplusNum,
                           db.PeriodsCode,
                           db.PerPrice,
                           go.GoodsName,
                           go.GoodsLogo,
                           go.GoodsDescribe,
                           db.Status,
                           db.OpenTime,
                           db.WaitOpenTime,
                           db.LuckyCode,
                           user.UserName,
                           //luckyusertimes=luckyusertimes.Times
                       };
            return new ObjectResult(FormatResult.Success(list));
        }
        ///<summary>
        ///我的中奖记录
        ///</summary>
        [HttpGet("GetLuckyList")]
        public IActionResult GetLuckyList(request re)
        {
            var list = from a in _context.DBPeriods
                       join b in _context.Goods on a.GoodsID equals b.ID
                       join c in _context.Users on a.LuckyUserID equals c.ID
                       join d in _context.DBOrderDetails on new { DBTicket=a.LuckyCode.Value, DBPeriodsID=a.ID } equals new { DBTicket=d.DBTicket, DBPeriodsID=d.DBPeriodsID }
                       join e in _context.OrderDetails on d.OrderDetailID equals e.ID
                       where a.LuckyUserID == CurrentUserID
                       select new
                       {
                           a.ID,
                           a.OverplusNum,
                           a.GoodsID,
                           b.GoodsName,
                           b.GoodsDescribe,
                           b.GoodPrice,
                           b.GoodsLogo,
                           a.NeedNum,
                           a.PeriodsCode,
                           c.UserName,
                           e.DBNum,
                           a.LuckyCode,
                           a.OpenTime
                       };
            return new ObjectResult(FormatResult.Success(list));
        }
    }
}