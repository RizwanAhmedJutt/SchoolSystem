using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentResultSheet
    {
        public int StudentResultId { get; set; }
        public int AcadmicClassId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public float ClassAssessmentPercentage { get; set; }
        public float PaperPercentage { get; set; }
        public char Grade { get; set; }
        public string Remarks { get; set; }
        public string PaperTerm { get; set; }
    }
}
