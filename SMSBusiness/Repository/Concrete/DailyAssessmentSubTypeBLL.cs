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
        public List<DailyAssessmentSubType> GetAllAssessmentSubType()
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
                    AssessmentDetails.AssessmentSubTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["AssementName"].ToString();
                    AssessmentDetails.AssessmentTypeId=Convert.ToInt32(dr["AssessmentTypeId"]);
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
                        assessment.AssessmentTypeId = Convert.ToInt32(item["AssessmentTypeId"]);
                        assessment.ParentAssementName = item["AssementName"].ToString();
                        assessment.CreatedById = item["CreatedById"].ToString();
                        assessment.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                        assessment.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);
                        assessment.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : item["ModifiedById"].ToString();


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
        public DailyAssessmentSubType CheckSubAssementExist(int ParentAssessmentId, string SubAssessmentName)
        {
            var objgConatactsDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            DailyAssessmentSubType assessment = new DailyAssessmentSubType();
            try
            {
                stdAssessmentDetail = objgConatactsDao.CheckSubAssementExist(ParentAssessmentId, SubAssessmentName);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentSubTypeId = int.Parse(item["AssessmentSubTypeId"].ToString());
                        assessment.AssessmentSubTypeName = item["AssessmentSubTypeName"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assessment;


        }
    }
}
