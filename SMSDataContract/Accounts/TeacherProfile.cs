using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherProfile
    {
        public TeacherProfile()
        {
            TProfileId = 0;
            TeacherId = 0;
            ImagePath = string.Empty;
        }
        public int TProfileId { get; set; }
        public int TeacherId { get; set; }
        public string ImagePath { get; set; }
    }
}
