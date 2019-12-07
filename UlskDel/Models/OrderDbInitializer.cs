using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class OrderDbInitializer : DropCreateDatabaseAlways<OrderContext>
    {
        protected override void Seed(OrderContext db)
        {
            //db.Orders.Add(new Order { Sender="Ann", Receiver="Peter", Date=DateTime.Now, Time=DateTime.MaxValue});
            //db.Users.Add(new User { FirstName = "Moon", LastName = "Ivanova", Id = 1 });
            db.Users.Add(new User { Id = 1, FirstName = "Moon", LastName = "Ivanova", Patronymic = "Petrovna" });
            db.Orders.Add(new Order { Sender = "Ann", Receiver = "Peter", Date = DateTime.Now, Time = DateTime.MaxValue, UserId = 1 });
            db.Orders.Add(new Order { Sender = "SDF", Receiver = "sdf", Date = DateTime.Now, Time = DateTime.MaxValue, UserId = 1 });
            base.Seed(db);
        }
    }
}