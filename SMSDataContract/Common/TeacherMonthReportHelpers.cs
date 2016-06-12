using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
  public  class TeacherMonthReportHelpers
    {
        public TeacherMonthReportHelpers()
        {
            this.Courses = new List<Course>();
            this.TeacherAssessment = new List<TeacherAssessmentOperation>();
          
        }
        public List<Course> Courses { get; set; }
        public List<TeacherAssessmentOperation> TeacherAssessment { get; set; }
       

    }
}
