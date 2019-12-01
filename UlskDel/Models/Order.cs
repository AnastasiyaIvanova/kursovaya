using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public float Weight { get; set; }
        public string Address { get; set; }
        // цена
        public int Price { get; set; }
    }
}