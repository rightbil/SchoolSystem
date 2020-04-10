using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace SchoolSystem.Customize
{
    public class SsCurrentDateAttribute : ValidationAttribute

    {
        public override bool IsValid(object value)
        {
            return (DateTime)value <= DateTime.Now ? true : false;
        }
    }
}
