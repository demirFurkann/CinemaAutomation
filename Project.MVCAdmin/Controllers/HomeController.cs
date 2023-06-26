using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }
    }
}