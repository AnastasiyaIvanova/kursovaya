using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class MyDateTimeAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                DateTime date;
                DateTime.TryParse(value.ToString(), out date);
                if (date > DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
    }
}