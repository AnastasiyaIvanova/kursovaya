﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UlskDel.Models;

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

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(User user)
        {
            user.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Users.Add(user);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + user.Person + ", за покупку!";
        }
    }
}