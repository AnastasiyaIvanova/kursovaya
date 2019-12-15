using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UlskDel.Models
{
    public class OrderDbInitializer : DropCreateDatabaseAlways<OrderContext>
    {
        protected override void Seed(OrderContext db)
        {
            //db.Users.Add(new User { Id = 1, FirstName = "Moon", LastName = "Ivanova", Patronymic = "Petrovna" });
            //db.Orders.Add(new Order { Sender = "Ann", Receiver = "Peter", Date = DateTime.Now, Time = DateTime.MaxValue, UserId = 1 });
            
            Role admin = new Role { Name = "admin" };
            Role user = new Role { Name = "user" };
            Role courier = new Role { Name = "courier" };
            db.Roles.Add(admin);
            db.Roles.Add(user);

            string pwdUser = GetHash("123");
            db.Users.Add(new User
            {
                Email = "user@mail.com",
                Password = pwdUser,
                Role = user
            });
            base.Seed(db);

            string pwd = GetHash("123456");
            db.Users.Add(new User
            {
                Email = "admin@mail.com",
                Password = pwd,
                Role = admin
            });
            base.Seed(db);

            string pwd2 = GetHash("123456");
            db.Users.Add(new User
            {
                Email = "courier@mail.com",
                Password = pwd2,
                Role = courier
            });
            base.Seed(db);

            //db.Orders.Add(new Order { Sender = "SDF", Receiver = "sdf", Address_Receiver="Ульяновск", Address_Sender="Ярославль", Phone_Receiver = 98957, Phone_Sender = 986897, Date = DateTime.Now, Time = DateTime.MaxValue, Price = 1300, Status="Обрабатывается", Weight=13, Length=10, Width=3, Who_pay=false, UserId = 1 });
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}