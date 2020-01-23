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
            // получаем из бд все объекты Book
            IEnumerable<Order> orders = db.Orders;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Orders = orders;
            // возвращаем представление
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Count([Bind(Include = "Address_Sender,Address_Receiver,Weight,Length,Width,Height")] Order order)
        {
            string receiver = order.Address_Receiver;
            return receiver;
        }
    }
}