using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolSystem.Customize
{
    public class SsDateRangeAttribute:RangeAttribute
    {
        public SsDateRangeAttribute(string minimumValue)
            :base (typeof(DateTime),minimumValue, DateTime.Now.ToShortDateString())
        {
            
        }
    }
}