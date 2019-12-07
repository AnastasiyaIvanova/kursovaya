using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        //public List<Order> Orders { get; set; }
        public ICollection<Order> Orders { get; set; }
        public User()
        {
            Orders = new List<Order>();
        }
    }
}