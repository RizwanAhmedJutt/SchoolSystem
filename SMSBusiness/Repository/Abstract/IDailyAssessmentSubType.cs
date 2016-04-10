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
     List<DailyAssessmentSubType> GetAllAssessmentType();
     DailyAssessmentSubType GetDailyAssessmentSubTypeById(int AssessmentSubTypeId);
     int AddChangesAssessmentSubType(DailyAssessmentSubType dAssessmentsubType);

    }
}
