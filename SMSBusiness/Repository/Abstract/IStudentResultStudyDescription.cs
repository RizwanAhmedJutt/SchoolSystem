using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IStudentResultStudyDescription
    {

      List<StudentResultStudyDescription> GetALLStudyDescriptions();
      int AddChangesStudyDescriptions(StudentResultStudyDescription studyDescription);
    }
}
