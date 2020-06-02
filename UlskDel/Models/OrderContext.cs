using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }

    public class Role
    {
        public int Id { get; set; }
        [Display(Name = "Роль")]
        public string Name { get; set; }
    }

    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public User User { get; set; }
        public int sumVotes { get; set; }
        public int totalVotes { get; set; }
        [Display(Name = "Рейтинг")]
        public double rating
        {
            get
            {
                 int[] votesRange = { 1, 2, 3, 4, 5 };
                 if (sumVotes > 0 && totalVotes > 0) {
                        float z = 1.64485f;
                        int vMin = votesRange.Min();
                        float vWidth = votesRange.Max() - vMin;
                        float phat = (sumVotes - totalVotes * vMin) / vWidth / totalVotes;
                        double rating = (phat + z * z / (2 * totalVotes) - z* Math.Sqrt((phat* (1 - phat) + z * z / (4 * totalVotes)) / totalVotes)) / (1 + z * z / totalVotes);
                        return Math.Round(rating * vWidth + vMin, 6);
                    }
                 return 0;
            }
        }
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
        public int sumVotes { get; set; }
        public int totalVotes { get; set; }
        [Display(Name = "Рейтинг")]
        public double rating
        {
            get
            {
                int[] votesRange = { 1, 2, 3, 4, 5 };
                if (sumVotes > 0 && totalVotes > 0)
                {
                    float z = 1.64485f;
                    int vMin = votesRange.Min();
                    float vWidth = votesRange.Max() - vMin;
                    float phat = (sumVotes - totalVotes * vMin) / vWidth / totalVotes;
                    double rating = (phat + z * z / (2 * totalVotes) - z * Math.Sqrt((phat * (1 - phat) + z * z / (4 * totalVotes)) / totalVotes)) / (1 + z * z / totalVotes);
                    return Math.Round(rating * vWidth + vMin, 6);
                }
                return 0;
            }
        }
        [Display(Name = "Время")]
        public DateTime time { get; set; }
        [Display(Name = "Газель")]
        public bool oversize { get; set; }
        [Display(Name = "Район")]
        public Areas Area { get; set; }
        [Required]
        [Remote("IsExist", "Courier", ErrorMessage = "URL exist!")]        
        public ICollection<Order> Orders { get; set; }
        public Courier()
        {
            Orders = new List<Order>();
        }
    }

}