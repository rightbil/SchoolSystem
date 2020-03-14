using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{

    public class Teacher
    {
        /*[HiddenInput(DisplayValue=false)]*/
        public int ID { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [DisplayName("Enrollment Date")]
        [Required(ErrorMessage = "Enrollment Date is required")]
        public DateTime EnrollmentDate { get; set; }
        
        [Required(ErrorMessage = "Major is required")]
        public String Major { get; set; }
        public Course courses { get; set; }
        /*public virtual ISet<Course> xcourses { get; set; }
        public virtual IList<Course> ycourses { get; set; }*/

    }
}
