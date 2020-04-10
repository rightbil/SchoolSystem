using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SchoolSystem.Customize;

namespace SchoolSystem.MVC.Models
{
    public class Course
    {
       
        public int CourseId { get; set; }
        // the ff worked while Java script was enabled
        //[Remote("IsTitleExist","Course",ErrorMessage = "Title already in database")]
        // the ff works irrespective of javascript enable or disabled.
        //for this to work we need to disable the server side code in side the action method
        [SsRemoteClientServer("IsTitleExist", "Course", ErrorMessage = "Title already exists.")]

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