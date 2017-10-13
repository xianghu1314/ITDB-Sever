using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ITDB.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ITDB.Controllers
{
    [Route("api/Address")]
    [Authorize]
    public class AddressController : BaseController
    {
        public AddressController(DBContext context):base(context){
            
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var list=_context.ShippingAddress.ToList();
            return new ObjectResult(FormatResult.Success(list));
        }
        [HttpGet("GetDefault")]
        public IActionResult GetDefault()
        {
            var defaultAddress=_context.ShippingAddress.FirstOrDefault(s=>s.IfDefault);
            return new ObjectResult(FormatResult.Success(defaultAddress));
        }
        [HttpPost]
        public IActionResult Add([FromBody]ShippingAddress model)
        {
            var current=_context.ShippingAddress.Add(model);
            _context.SaveChanges();
            return new ObjectResult(FormatResult.Success("添加成功"));
        }
        [HttpPut]
        public IActionResult Update([FromBody]ShippingAddress model)
        {
            var current=_context.ShippingAddress.Find(model.ID);
            current.Address=model.Address;
            current.DetailAddress=model.DetailAddress;
            current.IfDefault=model.IfDefault;
            current.UserName=model.UserName;
            current.UserPhone=model.UserPhone;
            _context.SaveChanges();
            return new ObjectResult(FormatResult.Success("修改成功"));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var current=_context.ShippingAddress.Find(id);
            _context.ShippingAddress.Remove(current);
            _context.SaveChanges();
            return new ObjectResult(FormatResult.Success("删除成功"));
        }
    }
}