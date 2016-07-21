using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentAttendance
    {
        public int StudentAttendanceId { get; set; }
        public int AcadmicClassId { get; set; }
        public int StudentId { get; set; }
        public int WorkingDays { get; set; }
        public int Leaves { get; set; }
        public int Absents { get; set; }
        public double TotalPercentage { get; set; }
        public string PaperTerm { get; set; }
     
    }
}
