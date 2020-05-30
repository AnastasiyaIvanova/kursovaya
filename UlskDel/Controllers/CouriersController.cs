using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UlskDel.Models;

namespace UlskDel.Controllers
{
    public class CouriersController : Controller
    {
        private OrderContext db = new OrderContext();

        // GET: Couriers
        public ActionResult Index()
        {
            var couriers = db.Couriers.Include(c => c.User);
            return View(couriers.ToList());
        }

        public ActionResult Rate(int id)
        {
            Order c = db.Orders.Include(y => y.Customer).FirstOrDefault(x => x.OrderId == id);
            if (c != null)
                return PartialView(c);
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Rate(string Answer, [Bind(Include = "CustomerId")] Order order)
        {
            //Заказчик
            Customer c = db.Customers.FirstOrDefault(x => x.Id == order.CustomerId);
            c.sumVotes = c.sumVotes + Convert.ToInt32(Answer);
            c.totalVotes = c.totalVotes + 1;
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();

            User user = db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            int id = user.Id;
            return RedirectToAction("Details", "Couriers", new { id });
        }

        // GET: Couriers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Courier courier = db.Couriers.Include(t => t.Orders).FirstOrDefault(t => t.Id == id);            
            //Customer customer = db.Customers.Find(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }

        // GET: Couriers/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Couriers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ratisumVotes,totalVotesng")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                courier.time = DateTime.Now;
                db.Couriers.Add(courier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Email", courier.Id);
            return View(courier);
        }

        // GET: Couriers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = db.Couriers.Find(id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", courier.Id);
            return View(courier);
        }

        // POST: Couriers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,sumVotes,totalVotes,time,Area")] Courier courier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", courier.Id);
            return View(courier);
        }

        // GET: Couriers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courier courier = db.Couriers.Include(c => c.User).FirstOrDefault(c => c.Id ==id);
            if (courier == null)
            {
                return HttpNotFound();
            }
            return View(courier);
        }

        // POST: Couriers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courier courier = db.Couriers.Find(id);
            db.Couriers.Remove(courier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
