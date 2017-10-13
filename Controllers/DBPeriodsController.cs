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
        [HttpGet("GetNewList")]
        public IActionResult GetNewList(request re)
        {
            var list = from db in _context.DBPeriods
                       join go in _context.Goods on db.GoodsID equals go.ID
                       join u in _context.User on db.LuckyUserID equals u.ID into u1
                       from user in u1.DefaultIfEmpty()

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
    }
}