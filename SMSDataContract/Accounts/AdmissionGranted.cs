using System;
using System.Collections.Generic;
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
            CategoryId = 0;
            IsGranted = false;
            AdmissionGrantedForClass = string.Empty;
            AdmissionGrantedDate = DateTime.Now;
            Remarks = string.Empty;
            CreatedById = 0;
            CreatedDate = DateTime.Now;
            ModifiedById = null;
            ModifiedDate = null;
        }


        public int AdmissionId { get; set; }
        public int StudentId { get; set; }
        public string AssessmentResult { get; set; }
        public int CategoryId { get; set; }
        public bool IsGranted { get; set; }
        public string AdmissionGrantedForClass { get; set; }
        public DateTime AdmissionGrantedDate { get; set; }
        public string Remarks { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
