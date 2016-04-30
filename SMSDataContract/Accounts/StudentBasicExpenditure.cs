using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentBasicExpenditure
    {
        public StudentBasicExpenditure()
        {
            FeeId = 0;
            StudentId = 0;
            StudentName = string.Empty;
            FeeMonth = string.Empty;
            AcadmicClassId = 0;
            ClassName = string.Empty;
            AdmissionFee = 0;
            SecurityFee = 0;
            ExaminationFee = 0;
            RegistrationFee = 0;
            CreateById = string.Empty;
            CreateDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;
        }

        public int FeeId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string FeeMonth { get; set; }
        public int AcadmicClassId { get; set; }
        public string ClassName { get; set; }
        public int AdmissionFee { get; set; }
        public int SecurityFee { get; set; }
        public int ExaminationFee { get; set; }
        public int RegistrationFee { get; set; }
        public string CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
