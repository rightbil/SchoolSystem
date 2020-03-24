using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{

    public class ChangeOrder
    {
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public Int16? ApprovedByID { get; set; }
        public Byte ApprovalStatusID { get; set; }
        public Int16 AssignedToID { get; set; }
        public Int16 CreatedByID { get; set; }
        public Byte CurrentStatusID { get; set; }
        public DateTime? DateApproved { get; set; }
        public DateTime? EndDate { get; set; }
        public Byte ImpactID { get; set; }
        public Byte PriorityID { get; set; }
        public DateTime? StartDate { get; set; }
        public Byte TypeID { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string ReasonForChange { get; set; }

        [ForeignKey("ApprovedByID")]
        public User ApprovedBy { get; set; }

        [ForeignKey("ApprovalStatusID")]
        public ChangeApprovalStatus ApprovalStatus { get; set; }

        [ForeignKey("AssignedToID")] public User AssignedTo { get; set; }

        [ForeignKey("CreatedByID")] public User CreatedBy { get; set; }

        [ForeignKey("ImpactID")] public ChangeImpact Impact { get; set; }

        [ForeignKey("PriorityID")] public ChangePriority Priority { get; set; }

        [ForeignKey("TypeID")] public ChangeType ChangeType { get; set; }

        [ForeignKey("CurrentStatusID")] public ChangeStatus CurrentStatus { get; set; }
    */
    }
}
