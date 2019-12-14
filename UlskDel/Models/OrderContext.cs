using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("DefaultConnection")
        { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
        public User()
        {
            Orders = new List<Order>();
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}