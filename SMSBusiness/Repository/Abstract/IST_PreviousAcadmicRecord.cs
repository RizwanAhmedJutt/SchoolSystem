using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
 public   interface IST_PreviousAcadmicRecord
    {

     ST_PreviousAcadmicDetail GetPreviousAcadmicInfoByStudentId(int StudentId);
     int PreviousAcadmicDetailAddChanges(ST_PreviousAcadmicDetail PreviousDetail);

    }
}
