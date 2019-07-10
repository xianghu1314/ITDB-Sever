using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;

namespace ITDB.Controllers
{
    [Route("api/Config")]
    public class ConfigController : BaseController
    {
        public ConfigController(DBContext context):base(context){
            
        }
        [HttpGet("getCategories")]
        public IActionResult getCategories()
        {
            var list=_context.ConfigureCategories.Where(s=>s.IfShow).OrderByDescending(s=>s.Sort).ToList();
            return new ObjectResult(FormatResult.Success(list));
        }
        
    }
}