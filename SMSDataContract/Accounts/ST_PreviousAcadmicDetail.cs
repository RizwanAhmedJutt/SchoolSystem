using System;
using System.Collections.Generic;
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
        public string SchoolName { get; set; }
        public int AcadmicClassId { get; set; }
        public bool PreviousExamPassed { get; set; }
        public string Session { get; set; }
        public decimal MarksObtained { get; set; }
        public decimal TotalMark { get; set; }
        public char? Grade { get; set; }
        public string MediumOfInstruction { get; set; }
        //this is only to use as query string
        public int GuardianId { get; set; }

    }
}
