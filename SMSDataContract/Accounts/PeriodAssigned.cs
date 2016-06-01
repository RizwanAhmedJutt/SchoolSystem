using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
  public  class PeriodAssigned
    {
        public PeriodAssigned()
        {
            PeriodAssignedId =0;
            CourseId = 0;
            AcadmicClassId = 0;
            CourseName = string.Empty;
            ClassName = string.Empty;
        }

        public int PeriodAssignedId { get; set; }
        public int PeriodNumber { get; set; }
        public int CourseId { get; set; }
        public int AcadmicClassId { get; set; }
        public string CourseName { get; set; }
        public string ClassName { get; set; }


    }
}
