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
    public class StudentResultSocialDescriptionBLL : IStudentResultSocialDescription
    {

        public List<StudentResultSocialDescription> GetALLSocailDescriptions()
        {
            var objSocialDescriptionDao = new StudentResultSocialDescriptionDAO(new SqlDatabase());
            DataTable dt = objSocialDescriptionDao.GetALLSocialDescriptions();
            List<StudentResultSocialDescription> socialDescriptionList = new List<StudentResultSocialDescription>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var socialDescription = new StudentResultSocialDescription();
                    socialDescription.SocialDescriptionId = item.IsNull("SocialDescriptionId") ? 0 : Convert.ToInt32(item["SocialDescriptionId"]);
                    socialDescription.Description = item.IsNull("Description") ? string.Empty : item["Description"].ToString();
                    socialDescription.CreatedById = item.IsNull("CreatedBy") ? string.Empty : item["CreatedBy"].ToString();
                    socialDescription.CreatedDate = Convert.ToDateTime(item["CreatedDate"].ToString());
                    socialDescription.ModifiedById = item.IsNull("ModifiedBy") ? string.Empty : item["ModifiedBy"].ToString();
                    socialDescription.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"].ToString());
                    socialDescriptionList.Add(socialDescription);
                }
                return socialDescriptionList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int AddChangesSocialDescriptions(StudentResultSocialDescription socialDescription)
        {
            var objSocialDescriptionDao = new StudentResultSocialDescriptionDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objSocialDescriptionDao.AddChangessocialDescriptions(socialDescription);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ReturnValue;
        }



    }
}
