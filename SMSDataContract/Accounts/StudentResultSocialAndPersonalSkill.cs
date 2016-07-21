using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentResultSocialAndPersonalSkill
    {
        public StudentResultSocialAndPersonalSkill()
        {
            SocialSkillId = 0;
            AcadmicClassId = 0;
            StudentId = 0;
            Description = string.Empty;
            TermType = string.Empty;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = null;
            ModifiedDate = null;
        }

        public int SocialSkillId { get; set; }
        public int AcadmicClassId { get; set; }
        public int StudentId { get; set; }
        public string Description { get; set; }
        public char Grad { get; set; }
        public string TermType { get; set; }

        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
