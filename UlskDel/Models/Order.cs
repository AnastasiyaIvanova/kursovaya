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
        [Display(Name = "Отправитель")]
        public string Sender { get; set; }
        [Display(Name = "Получатель")]
        public string Receiver { get; set; }
        [Display(Name = "Адрес отправителя")]
        public string Address_Sender { get; set; }
        [Display(Name = "Адрес получателя")]
        public string Address_Receiver { get; set; }
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Номер отправителя")]
        public string Phone_Sender { get; set; }
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Invalid Phone Number.")]
        [Display(Name = "Номер получателя")]
        public string Phone_Receiver { get; set; }
        [DataType(DataType.Date)]
        //[MyDateTime]
        [Display(Name = "Дата")]
        public System.DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Время")]
        public System.DateTime Time { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }
        [Display(Name = "Вес")]
        public float Weight { get; set; }
        [Display(Name = "Длина")]
        public float Length { get; set; }
        [Display(Name = "Ширина")]
        public float Width { get; set; }
        [Display(Name = "Высота")]
        public float Height { get; set; }
        [Display(Name = "Оплата отправителем")]
        public bool Who_pay { get; set; }
        [Display(Name = "Цена")]
        public int Price { get; set; }
        public bool Print { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}