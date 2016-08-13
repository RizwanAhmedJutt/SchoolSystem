using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentResultWorkAndStudySkill
    {
        public StudentResultWorkAndStudySkill()
        {
            WorkSkillId = 0;
            AcadmicClassId = 0;
            StudentId = 0;
            StudyDescriptionId = 0;
            TermType = string.Empty;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = null;
            ModifiedDate = null;
            Grade = 'A';
        }

        public int WorkSkillId { get; set; }
        public int AcadmicClassId { get; set; }
        public int StudentId { get; set; }
        public int StudyDescriptionId { get; set; }
        public string Description { get; set; }
        public char Grade { get; set; }
        public string TermType { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
