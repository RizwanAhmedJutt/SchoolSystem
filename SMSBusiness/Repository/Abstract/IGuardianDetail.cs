using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IGuardianDetail
    {

        GuardianDetail GetGuardianInfoByStudentId(int StudentId);
        int GuardianContactAddChanges(GuardianDetail gDetail);

    }
}
