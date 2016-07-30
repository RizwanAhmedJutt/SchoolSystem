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
    public class StudentResultSocialAndPersonalSkillBLL : IStudentResultSocialAndPersonalSkill
    {

        public List<StudentResultSocialAndPersonalSkill> GetStudentSocialAndPersonalSkill()
        {
            var objStudentStudentResultSocialAndPersonalSkillDao = new StudentResultSocialAndPersonalSkillDAO(new SqlDatabase());
            DataTable stdDetail;
            List<StudentResultSocialAndPersonalSkill> stdSocialAndPersonalSkill = new List<StudentResultSocialAndPersonalSkill>();
            try
            {
                stdDetail = objStudentStudentResultSocialAndPersonalSkillDao.GetStudentSocialAndPersonalSkill();

                foreach (DataRow item in stdDetail.Rows)
                {
                    StudentResultSocialAndPersonalSkill std = new StudentResultSocialAndPersonalSkill();
                    std.SocialSkillId = Convert.ToInt32(item["SocialSkillId"]);
                    std.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"].ToString());
                    std.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                    std.SocialDescriptionId = Convert.ToInt32(item["SocialDescriptionId"].ToString());
                    std.Grad = Convert.ToChar(item["Grade"].ToString().Trim());
                    std.TermType = item["TermType"].ToString();
                    std.CreatedById = item["CreatedById"].ToString();
                    std.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                    std.ModifiedById = item.IsNull("ModifiedById") ? null : (item["ModifiedById"].ToString());
                    std.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);
                    stdSocialAndPersonalSkill.Add(std);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return stdSocialAndPersonalSkill;
        }

        public int AddChangesStudentResultSocialAndPersonalSkill(StudentResultSocialAndPersonalSkill srSocial)
        {
            var objStudentResultSocialDao = new StudentResultSocialAndPersonalSkillDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objStudentResultSocialDao.InsertUpdateStudentSocialAndPersonalSkill(srSocial);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ReturnValue;
        }

        public StudentResultSocialAndPersonalSkill GetStudentSocialAndPersonalSkillById(int Id)
        {
            var objStudentResultSocialAndPersonalSkillDao = new StudentResultSocialAndPersonalSkillDAO(new SqlDatabase());
            DataTable stdDetail;
            StudentResultSocialAndPersonalSkill std = new StudentResultSocialAndPersonalSkill();
            try
            {
                stdDetail = objStudentResultSocialAndPersonalSkillDao.GetStudentSocialAndPersonalSkillById(Id);
                foreach (DataRow item in stdDetail.Rows)
                {
                    std.SocialSkillId = Convert.ToInt32(item["SocialSkillId"]);
                    std.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"].ToString());
                    std.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                    std.SocialDescriptionId = Convert.ToInt32(item["SocialDescriptionId"].ToString());
                    std.Grad = Convert.ToChar(item["Grade"].ToString());
                    std.TermType = item["TermType"].ToString();
                    std.CreatedById = item["CreatedById"].ToString();
                    std.CreatedDate = Convert.ToDateTime(item["CreatedDate"].ToString());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return std;



        }

    }
}
