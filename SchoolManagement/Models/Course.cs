using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.MVC.Models
{
    public class Course
    {
       
        public int CourseId { get; set; }
       
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Title { get; set; }

        [Display(Name = "Credit")]
        [Range(0,4,ErrorMessage = "Credits are 0 to 4")]
        public int Credit { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }


    }
}