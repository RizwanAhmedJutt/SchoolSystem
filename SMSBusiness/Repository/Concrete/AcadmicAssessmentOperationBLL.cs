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
    public class AcadmicAssessmentOperationBLL : IAcadmicAssessmentOperation
    {

      public int AddChangesAcadmicAssessmentOperation(AcadmicAssessmentOperation dAssessmentOperation)
      {
          var objAssessmentDao = new AcadmicAssessmentOperationDAO(new SqlDatabase());
          int ReturnValue = 0;  // Value will be 99 in case of Update
          try
          {
              ReturnValue = objAssessmentDao.InsertUpdateAcadmicAssessmentOperation(dAssessmentOperation);
          }
          catch (Exception)
          {

              throw;
          }

          return ReturnValue;
      }


    }
}
