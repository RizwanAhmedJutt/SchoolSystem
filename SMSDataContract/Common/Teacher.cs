using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
    public class Teacher
    {
        public Teacher()
        {
            TeacherId = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            LastQualification = string.Empty;
            CNIC = string.Empty;
            JoinDate = DateTime.Now;
            LeaveDate = DateTime.Now;
            RefrenceName = string.Empty;
            RefrenceContact = string.Empty;
            ModifiedById = string.Empty;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public string LastQualification { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public string RefrenceName { get; set; }
        public string RefrenceContact { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
