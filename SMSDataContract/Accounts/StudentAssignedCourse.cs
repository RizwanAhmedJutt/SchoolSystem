using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentAssignedCourse
    {
        public StudentAssignedCourse()
        {
            AssignCourseId = 0;
            CourseId = 0;
            StudentId = 0;
            CourseName = string.Empty;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;
        }

        public int AssignCourseId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string CourseName { get; set; }
        public string StudentName { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
