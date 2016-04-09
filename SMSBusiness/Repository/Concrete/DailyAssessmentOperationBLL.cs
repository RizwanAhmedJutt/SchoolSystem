using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class DailyAssessmentOperationBLL : IDailyAssessmentOperation
    {

      public int AddChangesAssessmentOperation(DailyAssessmentOperation dAssessmentOperation)
      {
          var objAssessmentDao = new DailyAssessmentOperationDAO(new SqlDatabase());
          int ReturnValue = 0;  // Value will be 99 in case of Update
          try
          {
              ReturnValue = objAssessmentDao.InsertUpdateDailyAssessmentOperation(dAssessmentOperation);
          }
          catch (Exception)
          {

              throw;
          }

          return ReturnValue;
      }


    }
}
