using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required ( ErrorMessage="Enter Father Name")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Enter Mother Name")]
        public string MotherName { get; set; }

         [Required(ErrorMessage = "Enter Guardian Name")]
        public string GuardianName { get; set; }

         [Required(ErrorMessage = "Enter Mother Tongue")]
        public string MotherTongue { get; set; }

        [Required(ErrorMessage = "Enter Guardian Relation")]
        public string RelationWithGuardian { get; set; }
        [Required(ErrorMessage = "Enter Guardian Income")]
        public decimal GuardianMontlyIncome { get; set; }

        [Required(ErrorMessage = "Enter Guardian Qualification")]
        public string GuardianQualification { get; set; }
        [Required(ErrorMessage = "Enter Guardian Occupation")]
        public string OccupationOfGuardian { get; set; }

    }
}
