using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UlskDel.Models;

namespace UlskDel.Controllers
{
    public class AccountController : Controller
    {
        OrderContext db = new OrderContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string pwd = GetHash(model.Password);
                // поиск пользователя в бд
                User user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == pwd);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, bool isCourier = false)
        {
            if (ModelState.IsValid)
            {
                string pwd = GetHash(model.Password);
                User user = db.Users.FirstOrDefault(u => u.Email == model.Name && u.Password == pwd);
                int role = 1;
                if (isCourier)
                {
                    role = 3;
                }

                if (user == null)
                {
                    User x = db.Users.Add(new User { Email = model.Name, Password = pwd, RoleId = role });
                    if (isCourier)
                    {
                        db.Couriers.Add(new Courier { Id = x.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now });
                    } else db.Customers.Add(new Customer { Id = x.Id, sumVotes = 0, totalVotes = 0 });
                    db.SaveChanges();
                    user = db.Users.Where(u => u.Email == model.Name && u.Password == pwd).FirstOrDefault();
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
            }
            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}