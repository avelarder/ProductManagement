using ProductManagement.Core;
using ProductManagement.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementMVC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This application supports switching between data repositories in real time.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Arturo Velarde";

            return View();
        }
    }
}