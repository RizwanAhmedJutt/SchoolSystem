using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentBasicExpenditure
    {
        public int FeeId { get; set; }
        public int StudentId { get; set; }
        public DateTime FeeMonth { get; set; }
        public string ClassName { get; set; }
        public float AdmissionFee { get; set; }
        public float SecurityFee { get; set; }
        public float ExaminationFee { get; set; }
        public float RegistrationFee { get; set; }
        public int CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
