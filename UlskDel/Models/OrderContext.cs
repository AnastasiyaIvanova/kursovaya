using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Почта")]
        [MinLength(4)]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [MinLength(3)]
        public string Password { get; set; }
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