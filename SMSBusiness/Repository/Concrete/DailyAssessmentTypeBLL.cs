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
    public class DailyAssessmentTypeBLL : IDailyAssessmentType
    {

       public int AddChangeDailyAssessmentType(DailyAssessmentType dAssessmentsubType)
       {
           var objAssessmentDao = new DailyAssessmentTypeDAO(new SqlDatabase());
           int ReturnValue = 0;  // Value will be 99 in case of Update
           try
           {
               ReturnValue = objAssessmentDao.InsertUpdateDailyAssessmentType(dAssessmentsubType);
           }
           catch (Exception)
           {

               throw;
           }

           return ReturnValue;
       }


    }
}
