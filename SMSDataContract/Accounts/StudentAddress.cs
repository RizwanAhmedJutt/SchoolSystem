using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentAddress
    {
        public StudentAddress()
        {
            StudentAddressId = 0;
            StudentId = 0;
            PermanentAddress = string.Empty;
            PresentAddress = string.Empty;

        }

        public int StudentAddressId { get; set; }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Enter Permanent Address")]
        public string PermanentAddress { get; set; }
        [Required(ErrorMessage = "Enter Present Address")]
        public string PresentAddress { get; set; }
        public int CityId { get; set; }
    }
}
