using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Car> Cars { get; set; }
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
        //public ICollection<Order> Orders { get; set; }
        //public User()
        //{
        //    Orders = new List<Order>();
        //}
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }
        public int rating { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new List<Order>();
        }
    }

    public class Admin
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }
    }

    public class Courier
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }
        public int rating { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Courier()
        {
            Orders = new List<Order>();
        }
    }

    public class Car
    {
        public int Id { get; set; }
        public int volume { get; set; }
    }

}