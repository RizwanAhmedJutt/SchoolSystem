using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class GuardianContacts
    {
        public GuardianContacts()
        {
            GuardianContactId = 0;
            GuardianId = 0;
            FirstContact = string.Empty;
            SecondContact = string.Empty;

        }

        public int GuardianContactId { get; set; }
        public int GuardianId { get; set; }
        public string FirstContact { get; set; }
        public string SecondContact { get; set; }
    }
}
