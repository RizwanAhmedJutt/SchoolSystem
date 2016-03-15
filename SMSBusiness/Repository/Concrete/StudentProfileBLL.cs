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
    public class StudentProfileBLL : IStudentProfile
    {
        public StudentProfile GetStudentProfileInfoByStudentId(int StudentId)
        {
            var objacadmicDetailDao = new StudentProfileDAO(new SqlDatabase());
            DataTable stdProfile;
            StudentProfile sProfile = new StudentProfile();
            try
            {
                stdProfile = objacadmicDetailDao.GetStudentInfoByStudentId(StudentId);
                if (stdProfile.Rows.Count > 0)
                {
                    foreach (DataRow item in stdProfile.Rows)
                    {
                        sProfile.StudentId = Convert.ToInt32(item["StudentId"]);
                        sProfile.ImagePath = item["ImagePath"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return sProfile;



        }

        public int StudentProfileAddChanges(StudentProfile sProfile)
        {
            var objacadmicDetailDao = new StudentProfileDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objacadmicDetailDao.InsertUpdateStudentProfile(sProfile);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }






    }
}
