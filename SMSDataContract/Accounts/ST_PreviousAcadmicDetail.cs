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
        public string SchoolName { get; set; }
        public int AcadmicClassId { get; set; }
        [Display(Name="Previous Exam Passed")]
        public bool PreviousExamPassed { get; set; }
       
        public string Session { get; set; }
        [Display(Name="Obtained Marks")]
        public decimal MarksObtained { get; set; }
        [Display(Name="Total Marks")]
        public decimal TotalMark { get; set; }
        public string Grade { get; set; }
        public string MediumOfInstruction { get; set; }
        //this is only to use as query string
        public int GuardianId { get; set; }

    }
}
