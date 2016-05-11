using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSDataContract.Common;

namespace SMSDataContract.Accounts
{
   public class StudentDetail
    {
        public Student Student { get; set; }
        public StudentAddress StudentAddress { get; set; }
        public GuardianContacts GuardianContact { get; set; }
        public GuardianDetail GuardianDetail { get; set; }
        public City Cities { get; set; }
        public AdmissionGranted AdmissionGranted { get; set; }
        public AcadmicClass AcadmicClass { get; set; }

    }
}
