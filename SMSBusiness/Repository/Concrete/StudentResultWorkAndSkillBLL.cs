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
                   std.Description = item["Description"].ToString();
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
