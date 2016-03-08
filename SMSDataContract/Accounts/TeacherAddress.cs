using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherAddress
    {
        public int TAddressId { get; set; }
        public int TeacherId { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }

    }
}
