using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
   public interface IStudentResultWorkAndSkill
    {
        List<StudentResultWorkAndStudySkill> GetStudentWorkAndStudySkill();
        int AddChangesStudentWorkAndStudySkill(StudentResultWorkAndStudySkill srWorkAndstudy);
        StudentResultWorkAndStudySkill GetStudentWorkAndStudySkillById(int Id);
    }
}
