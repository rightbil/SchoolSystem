using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.DbModels.Model
{
  
        [Table("tblDepartment")]
        public class Department
        {
            public int DepartmentId { get; set; }
            [DisplayName("Department Name"), Required, StringLength(10)]
            public string DeptName { get; set; }
            public int Capacity { get; set; }

    }
    }

