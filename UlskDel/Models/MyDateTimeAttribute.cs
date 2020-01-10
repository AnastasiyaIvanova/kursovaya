using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UlskDel.Models
{
    public class MyDateTimeAttribute : RangeAttribute
    {
        public MyDateTimeAttribute()
          : base(typeof(DateTime),
                  DateTime.Now.AddDays(1).ToShortDateString(),
                  DateTime.Now.AddYears(1).ToShortDateString())
        { }
    }
}