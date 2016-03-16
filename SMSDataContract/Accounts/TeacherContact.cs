using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherContact
    {
        public TeacherContact()
        {
            TeacherContactId = 0;
            TeacherId = 0;
            ContactFrist = string.Empty;
            ContactSecond = string.Empty;
        }
        public int TeacherContactId { get; set; }
        public int TeacherId { get; set; }
        public string ContactFrist { get; set; }
        public string ContactSecond { get; set; }
    }
}
