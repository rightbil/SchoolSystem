using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Display(Name = "Credit")]
        [Range(0,4,ErrorMessage = "Credits are 0 to 4")]
        public int Credits { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]

        public double Price { get; set; }


    }
}