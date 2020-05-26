using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SchoolSystem.Customize;

namespace SchoolSystem.MVC.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        /// <summary>
        ///[Remote("IsTitleExist","Course",ErrorMessage = "Title already in database")]// this works while Java script enabled
        ///for this to work disable the server side code in the action method b/c we can also validate data in the action methods, this works irrespective of Javascript 
        /// </summary>

        [DisplayName("Name"),StringLength(20) ,Required(ErrorMessage = "Name is required")]
        //this works while creating but it has bug while editing with out changing the Course Title.
        //[RemoteClientServer("IsTitleExist", "Course", ErrorMessage = "Title already exists.")]

        public string Title { get; set; }
        [Range(0,4,ErrorMessage = "Invalid credit! Credits are 0 to 4")]
        public int Credit { get; set; }
        [DataType(DataType.Currency), Required(ErrorMessage = "Price is required")]
        [Range(0.0,9999.00)]
        public double Price { get; set; }
        /*public int DepartmentId { get; set; }
        public string Department { get; set; }
    */
    }
}