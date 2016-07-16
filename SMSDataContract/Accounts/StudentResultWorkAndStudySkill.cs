using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentResultWorkAndStudySkill
    {

        public int WorkSkillId { get; set; }
        public int AcadmicClassId { get; set; }
        public int StudentId { get; set; }
        public string Description { get; set; }
        public char Grade { get; set; }
        public string TermType { get; set; }
    }
}
