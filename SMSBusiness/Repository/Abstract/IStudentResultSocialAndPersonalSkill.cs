using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IStudentResultSocialAndPersonalSkill
    {
      List<StudentResultSocialAndPersonalSkill> GetStudentSocialAndPersonalSkill();
      int AddChangesStudentResultSocialAndPersonalSkill(StudentResultSocialAndPersonalSkill srSocial);
      StudentResultSocialAndPersonalSkill GetStudentSocialAndPersonalSkillById(int Id);
    }
}
