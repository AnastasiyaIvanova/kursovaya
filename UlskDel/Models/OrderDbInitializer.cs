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
            db.SaveChanges();

            Customer cust = new Customer { Id = first.Id, sumVotes = 0, totalVotes = 0 };
            db.Customers.Add(cust);

            string pwd = GetHash("123456");
            User second = db.Users.Add(new User
            {
                Email = "admin@mail.com",
                Password = pwd,
                Role = admin
            });
            base.Seed(db);

            string pwd2 = GetHash("123456");
            User cour1 = db.Users.Add(new User
            {
                Email = "courier@mail.com",
                Password = pwd2,
                Role = courier
            });
            base.Seed(db);

            User cour2 = db.Users.Add(new User
            {
                Email = "courier2@mail.com",
                Password = pwd2,
                Role = courier
            });
            base.Seed(db);
            User cour3 = db.Users.Add(new User
            {
                Email = "courier3@mail.com",
                Password = pwd2,
                Role = courier
            });
            base.Seed(db);
            User cour4 = db.Users.Add(new User
            {
                Email = "courier4@mail.com",
                Password = pwd2,
                Role = courier
            });
            base.Seed(db);
            User cour5 = db.Users.Add(new User
            {
                Email = "courier5@mail.com",
                Password = pwd2,
                Role = courier
            });
            base.Seed(db);
            db.SaveChanges();

            var cour = new List<Courier>
            {
                new Courier {Id = cour1.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now, Area = Areas.Ленинский, Name = "Первый" },
                new Courier {Id = cour2.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now, Area = Areas.Железнодорожный, Name = "Второй" },
                new Courier {Id = cour3.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now, Area = Areas.Заволжский, Name = "Третий" },
                new Courier {Id = cour4.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now, Area = Areas.Засвияжский, Name = "Четвертый" },
                new Courier {Id = cour5.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now, Area = Areas.Засвияжский, oversize = true, Name = "Пятый" }
            };
            cour.ForEach(s => db.Couriers.Add(s));
            db.SaveChanges();
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}