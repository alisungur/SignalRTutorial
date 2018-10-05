using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalROrnek.Controllers
{
    public class HomeController : Controller
    {
        SignalRContext context = new SignalRContext();        

        public ActionResult Index()
        {
            
            return View();           
        }

        
        public ActionResult VeriGetir()
        {
            var model= context.SignalRTest.OrderByDescending(z => z.Id).FirstOrDefault();
            return Json(model,JsonRequestBehavior.AllowGet); 
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}