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
    public class AssessmentCategoriesBLL : IAssessmentCategories
    {

       public List<AssessmentCategories> GetALLAssessmentCategories()
        {
            var objAssessmentDao = new AssessmentCategoriesDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLAssessmentCategories();
            List<AssessmentCategories> objAssementList = new List<AssessmentCategories>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new AssessmentCategories();
                    AssessmentDetails.AssessmentCategoryId = Convert.ToInt32(dr["AssessmentCategoryId"]);
                    AssessmentDetails.AssessmentName = dr["AssessmentName"].ToString();
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }

       public AssessmentCategories GetAssessmentCategoryById(int AssessmentCategoryId)
        {
            var objgConatactsDao = new AssessmentCategoriesDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            AssessmentCategories assessment = new AssessmentCategories();
            try
            {
                stdAssessmentDetail = objgConatactsDao.GetAssessmentCategoryById(AssessmentCategoryId);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentCategoryId = int.Parse(item["AssessmentCategoryId"].ToString());
                        assessment.AssessmentName = item["AssessmentName"].ToString();
                        assessment.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                        assessment.CreatedById = item["CreatedById"].ToString();
                        assessment.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : item["ModifiedById"].ToString();
                        assessment.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);


                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assessment;



        }

       public int AddChangeAssessmentCategories(AssessmentCategories dAssessmentCategory)
        {
            var objAssessmentDao = new AssessmentCategoriesDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAssessmentDao.InsertUpdateAssessmentCategories(dAssessmentCategory);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

       public AssessmentCategories GetAssessmentCategoryByName(string AssessmentName)
        {

            var objgConatactsDao = new AssessmentCategoriesDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            AssessmentCategories assessment = new AssessmentCategories();
            try
            {
                stdAssessmentDetail = objgConatactsDao.GetAssessmentCategoryByName(AssessmentName);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentCategoryId = int.Parse(item["AssessmentCategoryId"].ToString());
                        assessment.AssessmentName = item["AssessmentName"].ToString();
                        assessment.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                        assessment.CreatedById = item["CreatedById"].ToString();
                        assessment.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : item["ModifiedById"].ToString();
                        assessment.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);


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
