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
    public class StudentResultSheetBLL
    {
        public int AddChangesStudentResultSheet(StudentResultSheet srSheet)
        {
            var objStudentResultSheetDao = new StudentResultSheetDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objStudentResultSheetDao.InsertUpdateStudentResultSheet(srSheet);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ReturnValue;
        }
        public StudentResultSheet GetStudentById(int Id)
        {
            var objStudentResultSheetDao = new StudentResultSheetDAO(new SqlDatabase());
            DataTable stdDetail;
            StudentResultSheet std = new StudentResultSheet();
            try
            {
                stdDetail = objStudentResultSheetDao.GetStudentResultSheetById(Id);

                foreach (DataRow item in stdDetail.Rows)
                {
                    std.StudentResultId = Convert.ToInt32(item["StudentResultId"]);
                    std.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"].ToString());
                    std.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                    std.CourseId = Convert.ToInt32(item["CourseId"].ToString());
                    std.ClassAssessmentPercentage = Convert.ToDouble(item["ClassAssessmentPercentage"].ToString());
                    std.PaperPercentage = Convert.ToDouble(item["PaperPercentage"].ToString());
                    std.Grade = Convert.ToChar(item["Grade"]);
                    std.Remarks = item["Remarks"].ToString();
                    std.PaperTerm = item["PaperTerm"].ToString();
                    std.CreatedById = item["CreatedById"].ToString();
                    std.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
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
