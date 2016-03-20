using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
    public class Teacher
    {
        public Teacher()
        {
            TeacherId = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            LastQualification = string.Empty;
            CNIC = string.Empty;
            JoinDate = DateTime.Now;
            LeaveDate = DateTime.Now;
            RefrenceName = string.Empty;
            RefrenceContact = string.Empty;
            Active = false;
            ModifiedById =null;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedDate =null ;
        }
        public int TeacherId { get; set; }
        [Display(Name="First Name")]
        [Required(ErrorMessage="Enter First Name")]
        public string FirstName { get; set; }
        [Display(Name="Last Name")]
        [Required(ErrorMessage="Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage="Enter CNIC")]
        public string CNIC { get; set; }
        [Display(Name="Last Qualification")]
        [Required(ErrorMessage="Enter Last Qualification")]
        public string LastQualification { get; set; }
        [Display(Name="Join Date")]
        [Required(ErrorMessage="Enter Join Date")]
        public DateTime JoinDate { get; set; }
        [Display(Name="Leave Date")]
        
        public DateTime LeaveDate { get; set; }
        [Display(Name="Refrence Name")]
        public string RefrenceName { get; set; }
        [Display(Name="Refrence Contact")]
        public string RefrenceContact { get; set; }
        public string TeacherName { get; set; }
        public bool Active { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
