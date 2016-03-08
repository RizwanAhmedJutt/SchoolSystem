using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public string LastQualification { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LeaveDate { get; set; }
        public string RefrenceName { get; set; }
        public string RefrenceContact { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
