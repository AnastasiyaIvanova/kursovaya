using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UlskDel.Models;
using Jint;

namespace UlskDel.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        OrderContext db = new OrderContext();

        public ActionResult Index()
        {
            IEnumerable<Order> orders = db.Orders;
            // передаем все объекты в динамическое свойство Orders в ViewBag
            ViewBag.Orders = orders;
            return View();
        }

        //[Authorize(Roles = "user")]
        public ActionResult About()
        {
            return View();
        }
        
        
        public ActionResult Count()
        {
            return View();
        }
    }
}