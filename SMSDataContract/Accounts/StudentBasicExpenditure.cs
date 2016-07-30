using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Fee Month")]
        public string FeeMonth { get; set; }
        [Display(Name = "Acadmic Class")]
        public int AcadmicClassId { get; set; }
        public string ClassName { get; set; }
        [Display(Name = "Admission")]
        public int AdmissionFee { get; set; }
        [Display(Name = "Security")]
        public int SecurityFee { get; set; }
        [Display(Name = "Examination")]
        public int ExaminationFee { get; set; }
        [Display(Name = "Registration")]
        public int RegistrationFee { get; set; }
        public string CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
