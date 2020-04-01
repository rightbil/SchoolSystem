using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolSystem.DbContext;
using SchoolSystem.MVC.Models.Controllers;


namespace SchoolSystem.MVC.Models.Tests.Controllers
{
    [TestClass]
    public class TeachersControllerTest
    {
        private SchoolDbContext db = new SchoolDbContext();

        public void Can_Paginate()
        {
            // Arrange
           // Moke<Instructor> mock= new Moke<Instructor>();
        }

        public void Index()
        {
          // Arrange
          InstructorController controller = new InstructorController();

          // Act
          ViewResult result = controller.Index() as ViewResult;

          // Assert
          Assert.IsNotNull(result);
        }

        public void Details(int id)
        {
        }

        public void Create()
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create([Bind(Include = "TeacherId,LastName,FirstName,Major,Department, Course")]
            Instructor postInstructor)
        {
        }

        public void Edit(int id)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Edit([Bind(Include = "TeacherId,LastName,FirstName,HireDate,Major,Department,Course")]
            Instructor instructor)
        {
        }

        public void Delete(int? id)
        {

        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(int id)
        {

        }


        public void findByLastName(string searchLastName)
        {


        }

    }
}


