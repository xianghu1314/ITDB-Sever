using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ITDB.Models.Custom;

namespace ITDB.Controllers
{
    [Route("api/DBOrderDetail")]
    public class DBOrderDetailController : BaseController
    {
        public DBOrderDetailController(DBContext context) : base(context)
        {

        }
        [HttpGet("GetPurchaseHistory")]
        [Authorize]
        public IActionResult GetPurchaseHistory(request r)
        {
            try
            {
                var periods = (from a in _context.DBOrderDetail
                               where a.UserID == CurrentUserID
                               group a by new { DBPeriodsID = a.DBPeriodsID, UserID = a.UserID } into g
                               select new
                               {
                                   g.Key.DBPeriodsID,
                                   g.Key.UserID,
                                   Times = g.Count(),
                                   tickets = g.Select(s => s.DBTicket.ToString().PadLeft(8,'0')).ToList()
                               }).ToList();

                var list = from a in periods
                           join c in _context.DBPeriods on a.DBPeriodsID equals c.ID
                           join d in _context.Goods on c.GoodsID equals d.ID
                        //    join b in _context.OrderDetail on a.DBPeriodsID equals b.DBPeriodsID
                        //    join e in _context.Order on b.OrderID equals e.ID

                           join u in _context.User on c.LuckyUserID equals u.ID into us
                           from user in us.DefaultIfEmpty()
                           join n in (from a in _context.DBOrderDetail
                                      group a by new { DBPeriodsID = a.DBPeriodsID, UserID = a.UserID } into g
                                      select new
                                      {
                                          g.Key.DBPeriodsID,
                                          g.Key.UserID,
                                          Times = g.Count(),
                                          tickets = g.Select(s => s.DBTicket)
                                      }) on new { DBPeriodsID = a.DBPeriodsID, LuckyUserID = user?.ID } equals new { DBPeriodsID = n.DBPeriodsID, LuckyUserID = n?.UserID } into n2
                           from luckyusertimes in n2.DefaultIfEmpty()
                           //where e.UserID == a.UserID
                           //orderby e.SubmitTime descending
                           select new
                           {
                               a.DBPeriodsID,
                               c.GoodsID,
                               d.GoodsLogo,
                               d.GoodsName,
                               d.GoodsDescribe,
                               c.PeriodsCode,
                               a.Times,
                               c.NeedNum,
                               c.OverplusNum,
                               a.tickets,
                               user?.UserName,
                               c.LuckyCode,
                               luckyusertimes = luckyusertimes?.Times,
                               c.OpenTime
                           };
                return new ObjectResult(FormatResult.Success(list));
            }
            catch (System.Exception e)
            {

                return new ObjectResult(FormatResult.Failure(e.Message));
            }
        }
    }
}