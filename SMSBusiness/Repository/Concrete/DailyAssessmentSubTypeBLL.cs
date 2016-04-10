using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class DailyAssessmentSubTypeBLL : IDailyAssessmentSubType
    {
        public List<DailyAssessmentSubType> GetAllAssessmentType()
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLDailyAssessmentSubType();
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["AssementName"].ToString();
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public DailyAssessmentSubType GetDailyAssessmentSubTypeById(int AssessmentSubTypeId)
        {
            var objgConatactsDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            DailyAssessmentSubType assessment = new DailyAssessmentSubType();
            try
            {
                stdAssessmentDetail = objgConatactsDao.GetDailyAssessmentSubTypeById(AssessmentSubTypeId);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentSubTypeId = int.Parse(item["AssessmentSubTypeId"].ToString());
                        assessment.AssessmentSubTypeName = item["AssessmentSubTypeName"].ToString();
                        assessment.ParentAssementName = item["AssementName"].ToString();



                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assessment;



        }
        public int AddChangesAssessmentSubType(DailyAssessmentSubType dAssessmentsubType)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAssessmentDao.InsertUpdateDailyAssessmentSubType(dAssessmentsubType);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }
    }
}
