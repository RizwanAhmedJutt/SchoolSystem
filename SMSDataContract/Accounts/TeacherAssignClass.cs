using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherAssignClass
    {
        public TeacherAssignClass()
        {
            TeacherAssignId = 0;
            TeacherId = 0;
            TeacherName = string.Empty;
            ClassName = string.Empty;
        }
        public int TeacherAssignId { get; set; }
        public int TeacherId { get; set; }
        public int AcadmicClassId { get; set; }
        public string TeacherName { get; set; }
        public string ClassName { get; set; }

    }
}
