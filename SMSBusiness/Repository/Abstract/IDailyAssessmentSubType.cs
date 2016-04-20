using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
 public   interface IDailyAssessmentSubType
    {
     List<DailyAssessmentSubType> GetAllAssessmentSubType();
     DailyAssessmentSubType GetDailyAssessmentSubTypeById(int AssessmentSubTypeId);
     int AddChangesAssessmentSubType(DailyAssessmentSubType dAssessmentsubType);
     DailyAssessmentSubType CheckSubAssementExist(int ParentAssessmentId, string SubAssessmentName);

    }
}
