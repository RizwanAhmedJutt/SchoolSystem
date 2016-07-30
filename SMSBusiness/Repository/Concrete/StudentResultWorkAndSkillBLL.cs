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
    public class StudentResultWorkAndSkillBLL : IStudentResultWorkAndSkill
    {
        public List<StudentResultWorkAndStudySkill> GetStudentWorkAndStudySkill()
        {
            var objStudentResultWorkAndSkillDAODao = new StudentResultWorkAndSkillDAO(new SqlDatabase());
            DataTable stdDetail;
            List<StudentResultWorkAndStudySkill> stdworkAndStudySkill = new List<StudentResultWorkAndStudySkill>();
            try
            {
                stdDetail = objStudentResultWorkAndSkillDAODao.GetStudentWorkAndStudySkill();

                foreach (DataRow item in stdDetail.Rows)
                {
                    StudentResultWorkAndStudySkill std = new StudentResultWorkAndStudySkill();
                    std.WorkSkillId = Convert.ToInt32(item["WorkSikllid"]);
                    std.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"].ToString());
                    std.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                    std.StudyDescriptionId = Convert.ToInt32(item["StudyDescriptionId"].ToString());
                    std.Grade = Convert.ToChar(item["Grade"].ToString().Trim());
                    std.TermType = item["TermType"].ToString();
                    std.CreatedById = item["CreatedById"].ToString();
                    std.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                    std.ModifiedById = item.IsNull("ModifiedById") ? null : (item["ModifiedById"].ToString());
                    std.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);
                    stdworkAndStudySkill.Add(std);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return stdworkAndStudySkill;
        }

        public int AddChangesStudentWorkAndStudySkill(StudentResultWorkAndStudySkill srWorkAndstudy)
        {
            var objWorkAndStudyDao = new StudentResultWorkAndSkillDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objWorkAndStudyDao.InsertUpdateStudentWorkAndStudySkill(srWorkAndstudy);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ReturnValue;
        }
        public StudentResultWorkAndStudySkill GetStudentWorkAndStudySkillById(int Id)
       {
           var objStudentWorkAndStudySkillDao = new StudentResultWorkAndSkillDAO(new SqlDatabase());
           DataTable stdDetail;
           StudentResultWorkAndStudySkill std = new StudentResultWorkAndStudySkill();
           try
           {
               stdDetail = objStudentWorkAndStudySkillDao.GetStudentWorkAndStudyById(Id);
               foreach (DataRow item in stdDetail.Rows)
               {
                   std.WorkSkillId = Convert.ToInt32(item["WorkSikllid"]);
                   std.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"].ToString());
                   std.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                   std.StudyDescriptionId =Convert.ToInt32( item["Description"].ToString());
                   std.Grade = Convert.ToChar(item["Grade"].ToString());
                   std.TermType =item["TermType"].ToString();
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
