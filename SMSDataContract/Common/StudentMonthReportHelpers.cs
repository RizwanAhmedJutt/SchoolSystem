using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
  public  class StudentMonthReportHelpers
    {
        public List<Course> Courses { get; set; }
        public List<AcadmicAssessmentOperation> AcadmicAssessment { get; set; }
        public List<DailyAssessmentOperation> GeneralAssessment { get; set; }

    }
}
