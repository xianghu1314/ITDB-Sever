using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITDB.Models;
using ITDB.Models.Custom;
using Microsoft.AspNetCore.Mvc;

namespace ITDB.Controllers
{
    public class BaseController : Controller
    {
        protected readonly DBContext _context;
        public BaseController(DBContext context)
        {
            _context = context;
        }
        public int CurrentUserID
        {
            get
            {
                return Convert.ToInt32(User.Identity.Name);
            }
        }
        public response FormatResult{
            get
            {
                return new response();
            }
        }
    }
}