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
        [Display(Name="Father Name")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Enter Mother Name")]
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }

         [Required(ErrorMessage = "Enter Guardian Name")]
         [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }

         [Required(ErrorMessage = "Enter Mother Tongue")]
         [Display(Name = "Mother Tongue")]
        public string MotherTongue { get; set; }

        [Required(ErrorMessage = "Enter Guardian Relation")]
        [Display(Name = "Relation with Guardian")]
        public string RelationWithGuardian { get; set; }
        [Required(ErrorMessage = "Enter Guardian Income")]
        [Display(Name = "Guardian Monthly Income")]
        public decimal GuardianMontlyIncome { get; set; }

        [Required(ErrorMessage = "Enter Guardian Qualification")]
        [Display(Name = "Guardian Qualification")]
        public string GuardianQualification { get; set; }
        [Required(ErrorMessage = "Enter Guardian Occupation")]
        [Display(Name = "Occupation of Guardian")]
        public string OccupationOfGuardian { get; set; }

    }
}
