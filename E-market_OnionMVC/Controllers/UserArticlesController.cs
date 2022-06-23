using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_market_OnionMVC.Controllers
{
    public class UserArticlesController : Controller
    {
        public IActionResult ArticleList()
        {
            return View();
        }
    }
}
