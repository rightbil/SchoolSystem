using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolSystem.MVC.Models
{
    public class CourseEnrollement
    {
        [Key]
        public int CourseEnrollemntId { get; set; }
        public int  StudentId { get; set; }
        public int  CourseId { get; set; }
    }
}
