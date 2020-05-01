using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UlskDel.Models;
using System.Data.Entity;

namespace UlskDel.Controllers
{
    public class LKController : Controller
    {
        private OrderContext db = new OrderContext();
        // GET: LK
        public ActionResult Index()
        {
            User user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            int id = user.Id;
            return RedirectToAction("Details", "Customers", new { id });
        }
    }
}