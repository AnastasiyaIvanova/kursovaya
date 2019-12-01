using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Person { get; set; }
        // адрес покупателя
        public string Address { get; set; }
        // ID книги
        public int OrderId { get; set; }
        // дата покупки
        public DateTime Date { get; set; }
    }
}