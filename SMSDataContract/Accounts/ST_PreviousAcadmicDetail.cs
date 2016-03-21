using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class ST_PreviousAcadmicDetail
    {
        public ST_PreviousAcadmicDetail()
        {
            PAcadmicId = 0;
            StudentId=0;
            SchoolName = string.Empty;
            AcadmicClassId = 0;
            PreviousExamPassed = false;
            Session = string.Empty;
            MarksObtained = 0;
            TotalMark = 0;
            Grade=null;
            MediumOfInstruction = string.Empty;

        }

        public int PAcadmicId { get; set; }
        public int StudentId { get; set; }
        [Display(Name="School Name")]
        [Required(ErrorMessage="Enter School Name")]
        public string SchoolName { get; set; }
        [Display(Name="Acadmic Class")]
        public int AcadmicClassId { get; set; }
        [Display(Name="Previous Exam Passed")]
        [Required(ErrorMessage = "Enter Previous Exam pass?")]
        public bool PreviousExamPassed { get; set; }
       
        public string Session { get; set; }
        [Display(Name="Obtained Marks")]
        [Required(ErrorMessage="Enter Obtained marks")]
        public decimal MarksObtained { get; set; }
        [Display(Name="Total Marks")]
        [Required(ErrorMessage="Enter Total Marks")]
        public decimal TotalMark { get; set; }
        [Required(ErrorMessage="Enter Grade Like (A,B...F)")]
        public string Grade { get; set; }
        [Required(ErrorMessage="Language of communiction")]
        [Display(Name = "Medium Of Instruction")]
        public string MediumOfInstruction { get; set; }
        //this is only to use as query string
        public int GuardianId { get; set; }

    }
}
