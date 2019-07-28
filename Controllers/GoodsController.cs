using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;
using ITDB.Models.Custom;

namespace ITDB.Controllers
{
    [Route("api/Goods")]
    public class GoodsController : BaseController
    {
        public GoodsController(DBContext context):base(context){
            
        }
        [HttpGet("GetGoods")]
        public IActionResult GetGoods(request _params)
        {
            var list=_context.Goods.OrderByDescending(s=>s.CreateTime).Skip(_params.PageIndex).Take(_params.PageIndex).ToList();
            return new ObjectResult(FormatResult.Success(list));
        }
        [HttpGet("{id}",Name="GetGetGoodsByID")]
        public IActionResult GetGoodsByID(int id)
        {
            var list = _context.Goods.Find(id);
            return new ObjectResult(FormatResult.Success(list));
        }
    }
}