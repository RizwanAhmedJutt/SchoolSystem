using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherAssignedCourse
    {
        public int TeacherAssignedCourseId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }

    }
}
