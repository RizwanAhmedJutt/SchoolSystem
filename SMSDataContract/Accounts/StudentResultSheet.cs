using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentResultSheet
    {
        public StudentResultSheet()
        {
            StudentResultId = 0;
            AcadmicClassId = 0;
            StudentId = 0;
            CourseId = 0;
            ClassAssessmentPercentage = 0.0;
            PaperPercentage = 0.0;
            Grade = string.Empty;
            Remarks=string.Empty;
            PaperTerm = string.Empty;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = null;
            ModifiedDate = null;
        }
        public int StudentResultId { get; set; }
        public int AcadmicClassId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        [Display(Name="Class Assessment %")]
        public double ClassAssessmentPercentage { get; set; }
        [Display(Name = "Paper %")]
        public double PaperPercentage { get; set; }
        public string Grade { get; set; }
        public string Remarks { get; set; }
        public string PaperTerm { get; set; }
       
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
