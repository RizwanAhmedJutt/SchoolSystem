using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSDataContract.Common;

namespace SMSDataContract.Accounts
{
    public class DailyAssessmentSubType
    {

        public DailyAssessmentSubType()
        {
            AssessmentSubTypeId = 0;
            AssessmentSubTypeName = string.Empty;
            CreatedById = string.Empty;
            CreateDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;

           
        }

        public int AssessmentSubTypeId { get; set; }
     
        [Required(ErrorMessage="Select Assessment Type")]
        public int AssessmentTypeId { get; set; }
        [Display(Name="Sub Assessment Name")]
        [Required(ErrorMessage="Enter Sub Assessment Name")]
        public string AssessmentSubTypeName { get; set; }
       // public int ParentAssessmentId { get; set; }
        public string ParentAssementName { get; set; }
        public List<Evaluation> evaluation { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
