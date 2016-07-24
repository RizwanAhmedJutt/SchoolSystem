using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSDataContract.Accounts;

namespace SMSDataContract.Common
{
    public class ResultSheetHelper
    {

        public List<StudentAssignedCourse> AssignedCourses { get; set; }
        public List<StudentResultSheet> StudentResultSheet { get; set; }
        public StudentAttendance StudentAttendance { get; set; }
        public List<StudentResultSocialAndPersonalSkill> SrSocialAndPersonalSkill { get; set; }
        public List<StudentResultWorkAndStudySkill> SrWorkAndStudySkill { get; set; }
        public string PaperTerm { get; set; }

    }
}
