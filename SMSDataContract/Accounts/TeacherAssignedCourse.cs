using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherAssignedCourse
    {
        public TeacherAssignedCourse()
        {
            TeacherAssignedCourseId = 0;
            CourseId = 0;
            TeacherId = 0;
            AcadmicClassId = 0;
            CourseName = string.Empty;
            TeacherName = string.Empty;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;
        }

        public int TeacherAssignedCourseId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public int AcadmicClassId { get; set; }
        public string CourseName { get; set; }
        public string ClassName { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string TeacherName { get; set; }
       
    }
}
