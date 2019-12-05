using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Address_Sender { get; set; }
        public string Address_Receiver { get; set; }
        public int Phone_Sender { get; set; }
        public int Phone_Receiver { get; set; }
        [DataType(DataType.Date)]
        public System.DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public System.DateTime Time { get; set; }
        public string Status { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public bool Who_pay { get; set; }
        // цена
        public int Price { get; set; }
        public int UserId { get; set; }
    }
}