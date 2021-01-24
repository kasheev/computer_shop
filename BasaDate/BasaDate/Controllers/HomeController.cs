using BasaDate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BasaDate.Controllers
{
    
    public class HomeController : Controller
    {
        private CourseAPI courseAPI = new CourseAPI();

        public ActionResult Index()
        {
       
            string Dollar = courseAPI.GetParser().Valute.USD.Value;
            string Euro = courseAPI.GetParser().Valute.EUR.Value;
            ViewData["dollar"] = Dollar;
            ViewData["euro"] = Euro;
            //ViewData["dollar"] = dollar;
            //ViewData["euro"] = eur;
           
            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}