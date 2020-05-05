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
        //DropCreateDatabaseIfModelChanges<OrderContext>

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
            db.SaveChanges();

            var cour = new List<Courier>
            {
                new Courier {Id = cour1.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now },
                new Courier {Id = cour2.Id, sumVotes = 0, totalVotes = 0, time = DateTime.Now }
            };
            cour.ForEach(s => db.Couriers.Add(s));
            db.SaveChanges();

            var car = new List<Car>
            {
                new Car { volume = 1.5f, Id = cour[0].Id},
                new Car { volume = 7.5f, Id = cour[1].Id }
                
                //new Car { volume = 9, },
                //new Car { volume = 15, },
                //new Car { volume = 19.5f, }
            };
            car.ForEach(s => db.Cars.Add(s));
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