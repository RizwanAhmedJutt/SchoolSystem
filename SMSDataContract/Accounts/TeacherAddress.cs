using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherAddress
    {
        public TeacherAddress()
        {
            TAddressId = 0;
            TeacherId = 0;
            PermanentAddress = string.Empty;
            PresentAddress = string.Empty;
        }
        public int TAddressId { get; set; }
        public int TeacherId { get; set; }
        [Display(Name="Permanent Address")]
        [Required(ErrorMessage="Enter Permanent Address")]
        public string PermanentAddress { get; set; }
        [Display(Name="Present Address")]
        [Required(ErrorMessage="Enter Present Address")]
        public string PresentAddress { get; set; }

    }
}
