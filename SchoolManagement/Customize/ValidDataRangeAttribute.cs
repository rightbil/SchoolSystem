using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolSystem.Customize
{
    public class ValidDateRangeAttribute:RangeAttribute
    {
        public ValidDateRangeAttribute(string minimumValue)
            :base (typeof(DateTime),minimumValue, DateTime.Now.ToShortDateString())
        {
            
        }
    }
}