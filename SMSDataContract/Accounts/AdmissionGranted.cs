using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class AdmissionGranted
    {

        public AdmissionGranted()
        {
            AdmissionId = 0;
            StudentId = 0;
            AssessmentResult = string.Empty;
            GuardianId = 0;
            IsGranted = false;
            AdmissionGrantedForClass = string.Empty;
            AdmissionGrantedDate = DateTime.Now;
            Remarks = string.Empty;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = null;
            ModifiedDate = null;
        }


        public int AdmissionId { get; set; }
        public int StudentId { get; set; }
        [Display(Name="Assessment Result")]
        [Required(ErrorMessage="Enter assessment Result")]
        public string AssessmentResult { get; set; }
        public int GuardianId { get; set; }
        
        public bool IsGranted { get; set; }
        [Display(Name="Admission For Class")]
        [Required(ErrorMessage="Enter Addmission Granted for class")]
        public string AdmissionGrantedForClass { get; set; }
        [Display(Name="Admission Granted Date")]
        public DateTime AdmissionGrantedDate { get; set; }
        [Required(ErrorMessage="Enter Remarks")]
        [Display(Name="Final Remarks")]
        public string Remarks { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
