using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class GuardianDetail 
    {
        public GuardianDetail()
        {
            StudentGuardianId = 0;
            StudentId = 0;
            FatherName = string.Empty;
            MotherName = string.Empty;
            GuardianName = string.Empty;
            MotherTongue = string.Empty;
            RelationWithGuardian = string.Empty;
            GuardianMontlyIncome = 0;
            GuardianQualification = string.Empty;
            OccupationOfGuardian = string.Empty;
        }

        public int StudentGuardianId { get; set; }
        public int StudentId { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GuardianName { get; set; }
        public string MotherTongue { get; set; }
      
        public string RelationWithGuardian { get; set; }
        public decimal GuardianMontlyIncome { get; set; }
        public string GuardianQualification { get; set; }
        public string OccupationOfGuardian { get; set; }

    }
}
