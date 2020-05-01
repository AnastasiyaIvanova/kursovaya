using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UlskDel.Models
{
    public class OrderDbInitializer : DropCreateDatabaseIfModelChanges<OrderContext>

    {
        protected override void Seed(OrderContext db)
        {
            Role admin = new Role { Name = "admin" };
            Role customer = new Role { Name = "customer" };
            Role courier = new Role { Name = "courier" };
            db.Roles.Add(admin);
            db.Roles.Add(customer);
            db.Roles.Add(courier);

            string pwdUser = GetHash("123");
            User first = db.Users.Add(new User
            {
                Email = "user@mail.com",
                Password = pwdUser,
                Role = customer
            });
            base.Seed(db);

            //db.Customers.Add(new Customer
            //{
            //    Id = first.Id,
            //    rating = 0
            //});
            //base.Seed(db);

            string pwd = GetHash("123456");
            User second = db.Users.Add(new User
            {
                Email = "admin@mail.com",
                Password = pwd,
                Role = admin
            });
            base.Seed(db);

            //db.Admins.Add(new Admin
            //{
            //    Id = second.Id
            //});
            //base.Seed(db);

            string pwd2 = GetHash("123456");
            User third = db.Users.Add(new User
            {
                Email = "courier@mail.com",
                Password = pwd2,
                Role = courier
            });
            base.Seed(db);

            //db.Couriers.Add(new Courier
            //{
            //    Id = third.Id,
            //    rating = 0
            //});
            //base.Seed(db);
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}