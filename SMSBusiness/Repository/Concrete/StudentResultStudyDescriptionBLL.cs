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
    public class StudentResultStudyDescriptionBLL : IStudentResultStudyDescription
    {
   
        public List<StudentResultStudyDescription> GetALLStudyDescriptions()
        {
            var objStudyDescriptionDao = new StudentResultStudyDescriptionDAO(new SqlDatabase());
            DataTable dt = objStudyDescriptionDao.GetALLStudyDescriptions();
            List<StudentResultStudyDescription> studyDescriptionList = new List<StudentResultStudyDescription>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var studyDescription = new StudentResultStudyDescription();
                    studyDescription.StudyDescriptionId = item.IsNull("StudyDescriptionId") ? 0 : Convert.ToInt32(item["StudyDescriptionId"]);
                    studyDescription.Description = item.IsNull("Description") ? string.Empty : item["Description"].ToString();
                    studyDescription.CreatedById = item.IsNull("CreatedById") ? string.Empty : item["CreatedById"].ToString();
                    studyDescription.CreatedDate = Convert.ToDateTime(item["CreatedDate"].ToString());
                    studyDescription.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : item["ModifiedById"].ToString();
                    studyDescription.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"].ToString());
                    studyDescriptionList.Add(studyDescription);
                }
                return studyDescriptionList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int AddChangesStudyDescriptions(StudentResultStudyDescription studyDescription)
        {
            var objStudyDescriptionDao = new StudentResultStudyDescriptionDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objStudyDescriptionDao.AddChangesStudyDescriptions(studyDescription);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ReturnValue;
        }


    }
}
