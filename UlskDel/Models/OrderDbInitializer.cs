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
            db.Orders.Add(new Order { Weight = 13, Price = 220 });
            db.Orders.Add(new Order { Weight = 22, Price = 180 });
            db.Orders.Add(new Order { Weight = 4, Price = 150 });

            base.Seed(db);
        }
    }
}