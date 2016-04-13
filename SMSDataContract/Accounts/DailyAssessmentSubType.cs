using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Good = false;
            VeryGood = false;
            Average = false;
            Worse = false;
        }

        public int AssessmentSubTypeId { get; set; }
     
        [Required(ErrorMessage="Select Assessment Type")]
        public int AssessmentTypeId { get; set; }
        [Display(Name="Sub Assessment Name")]
        [Required(ErrorMessage="Enter Sub Assessment Name")]
        public string AssessmentSubTypeName { get; set; }
       // public int ParentAssessmentId { get; set; }
        public string ParentAssementName { get; set; }
        [Display(Name="V.Good")]
        public bool VeryGood { get; set; }

        public bool Good { get; set; }
        [Display(Name="AVG")]
        public bool Average { get; set; }

        public bool Worse { get; set; }
        public string WorseConcequence { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
