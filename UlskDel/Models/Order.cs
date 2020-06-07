using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UlskDel.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Display(Name = "Отправитель")]
        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string Sender { get; set; }
        [Display(Name = "Получатель")]
        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string Receiver { get; set; }
        [Display(Name = "Адрес отправителя")]
        [Required]
        [MaxLength(200)]
        [Remote("CheckAddress", "Orders", ErrorMessage ="Неверный адрес")]
        public string Address_Sender { get; set; }
        [Display(Name = "Адрес получателя")]
        [Required]
        [MaxLength(200)]
        public string Address_Receiver { get; set; }
        [Required]
        [Display(Name = "Район отправки")]
        public Areas Area_Sender { get; set; }
        [Required]
        [Display(Name = "Район получения")]
        public Areas Area_Receiver { get; set; }
        [Required]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неккоректный номер телефона.")]
        [Display(Name = "Номер отправителя")]
        public string Phone_Sender { get; set; }
        [Required]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Неккоректный номер телефона.")]
        [Display(Name = "Номер получателя")]
        public string Phone_Receiver { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Remote("CheckDate", "Orders", ErrorMessage = "Неверная дата")]
        [Display(Name = "Дата")]
        public System.DateTime Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Время")]
        public System.DateTime Time { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
        [Display(Name = "Вес, кг")]
        [Range(0,10)]
        [Required]
        public float Weight { get; set; }
        [Display(Name = "Длина, см")]
        [Range(0,200)]
        [Required]
        public float Length { get; set; }
        [Display(Name = "Ширина, см")]
        [Range(0, 200)]
        [Required]
        public float Width { get; set; }
        [Display(Name = "Высота, см")]
        [Range(0, 200)]
        [Required]
        public float Height { get; set; }
        [Display(Name = "Негабаритный")]
        public bool Big { get; set; }
        [Display(Name = "Хрупкое")]
        public bool Fragile { get; set; }
        [Display(Name = "Оплата отправителем")]
        public bool Who_pay { get; set; }
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Нажмите кнопку Расчитать")]
        public int Price { get; set; }
        public bool Print { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? CourierId { get; set; }
        [ForeignKey("CourierId")]
        public Courier Courier { get; set; }
        
    }

    public enum Areas : byte
    {
        Железнодорожный = 1,
        Заволжский = 2,
        Засвияжский = 3,
        Ленинский = 4
    }
}