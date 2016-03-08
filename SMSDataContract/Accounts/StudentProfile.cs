using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentProfile
    {
        public StudentProfile()
        {
            ProfileId = 0;
            StudentId = 0;
            ImagePath = string.Empty;
        }


        public int ProfileId { get; set; }
        public int StudentId { get; set; }
        public string ImagePath { get; set; }
    }
}
