using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class OrderDbInitializer : DropCreateDatabaseIfModelChanges<OrderContext>
    {
        protected override void Seed(OrderContext db)
        {
            //db.Users.Add(new User { Id = 1, FirstName = "Moon", LastName = "Ivanova", Patronymic = "Petrovna" });
            //db.Orders.Add(new Order { Sender = "Ann", Receiver = "Peter", Date = DateTime.Now, Time = DateTime.MaxValue, UserId = 1 });
            //db.Orders.Add(new Order { Sender = "SDF", Receiver = "sdf", Date = DateTime.Now, Time = DateTime.MaxValue, UserId = 1 });
            Role admin = new Role { Name = "admin" };
            Role user = new Role { Name = "user" };
            db.Roles.Add(admin);
            db.Roles.Add(user);
            db.Users.Add(new User
            {
                Email = "somemail@gmail.com",
                Password = "123456",
                Role = admin
            });
            base.Seed(db);
        }
    }
}