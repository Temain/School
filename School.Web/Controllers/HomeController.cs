using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Staff()
        {
            return View();
        }

        public ActionResult Students()
        {
            return View();
        }

        public ActionResult Disciplines()
        {
            return View();
        }

        public ActionResult Journal()
        {
            return View();
        }
    }
}