using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
    public class GeneralResultSheetHelper
    {

        public GeneralResultSheetHelper()
        {
           // this.StudentAttendance = new StudentAttendance();
            this.StudentResultSheet = new List<StudentResultSheet>();
            this.StudentAttendance = new List<StudentAttendance>();
            this.SrSocialAndPersonalSkill = new List<StudentResultSocialAndPersonalSkill>();
            this.SrWorkAndStudySkill = new List<StudentResultWorkAndStudySkill>();
        }
        
        public List<StudentResultSheet> StudentResultSheet { get; set; }
        public List<StudentAttendance> StudentAttendance { get; set; }
        public List<StudentResultSocialAndPersonalSkill> SrSocialAndPersonalSkill { get; set; }
        public List<StudentResultWorkAndStudySkill> SrWorkAndStudySkill { get; set; }
        public int? StudentId { get; set; }
        public int AcadmicClassId { get; set; }
    }
}
