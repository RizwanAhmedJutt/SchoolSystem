using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class GuardianContacts
    {
        public GuardianContacts()
        {
            GuardianContactId = 0;
            GuardianId = 0;
            FirstContact = string.Empty;
            SecondContact = string.Empty;
            StudentId = 0;
        }

        public int GuardianContactId { get; set; }
        public int GuardianId { get; set; }
        [Required( ErrorMessage="Enter Contact")]
        [Display(Name="First Contact")]
        public string FirstContact { get; set; }
        [Required(ErrorMessage ="Enter Contact")]
        [Display(Name = "Second Contact")]
        public string SecondContact { get; set; }
        public int StudentId { get; set; }
    }
}
