using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;

namespace ITDB.Controllers
{
    [Route("api/Goods")]
    public class GoodsController : BaseController
    {
        public GoodsController(DBContext context):base(context){
            
        }
        [HttpGet("GetGoods")]
        public IActionResult GetGoods()
        {
            var list=_context.Categories.OrderByDescending(s=>s.Sort).ToList();
            return new ObjectResult(FormatResult.Success(list));
        }
        [HttpGet("{id}",Name="GetGetGoodsByID")]
        public IActionResult GetGoodsByID(int id)
        {
            var current=_context.Categories.FirstOrDefault(a=>a.ID==id);
            var list=_context.Categories.Where(s=>s.ID==current.ParentCategoryID).Union(_context.Categories.Where(s=>s.ParentCategoryID==current.ParentCategoryID).OrderByDescending(s=>s.Sort));
            list.FirstOrDefault(s=>s.ID==current.ParentCategoryID).Name="全部";
            return new ObjectResult(FormatResult.Success(list));
        }
    }
}