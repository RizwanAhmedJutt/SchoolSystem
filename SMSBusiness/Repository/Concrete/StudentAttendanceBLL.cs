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
    public class StudentAttendanceBLL : IStudentAttendance
    {
        public List<StudentAttendance> GetStudentAttendanceSheet()
        {
            var objStudentAttendanceDao = new StudentAttendanceDAO(new SqlDatabase());
            DataTable stdDetail;
            List<StudentAttendance> stdAttendanceSheet = new List<StudentAttendance>();
            try
            {
                stdDetail = objStudentAttendanceDao.GetStudentAttendanceSheets();

                foreach (DataRow item in stdDetail.Rows)
                {
                    StudentAttendance std = new StudentAttendance();
                    std.StudentAttendanceId = Convert.ToInt32(item["StudentAttendanceId"]);
                    std.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"].ToString());
                    std.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                    std.WorkingDays = Convert.ToInt32(item["WorkingDays"].ToString());
                    std.Leaves = Convert.ToInt32(item["Leaves"].ToString());
                    std.Absents = Convert.ToInt32(item["Absentees"].ToString());
                    std.TotalPercentage = Convert.ToDouble(item["TotalPercentage"].ToString());
                    std.PaperTerm =(item["PaperTerm"].ToString().Trim());
                    stdAttendanceSheet.Add(std);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return stdAttendanceSheet;
        }
        public int AddChangesStudentAttendance(StudentAttendance sAttendance)
        {
            var objAttendanceDao = new StudentAttendanceDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAttendanceDao.InsertUpdateStudentAttendance(sAttendance);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return ReturnValue;
        }

        public StudentAttendance GetStudentAttendanceById(int Id)
        {
            var objStudentAttendanceSheetDao = new StudentAttendanceDAO(new SqlDatabase());
            DataTable stdDetail;
            StudentAttendance std = new StudentAttendance();
            try
            {
                stdDetail = objStudentAttendanceSheetDao.GetStudentAttendanceSheetById(Id);
                foreach (DataRow item in stdDetail.Rows)
                {
                    std.StudentAttendanceId = Convert.ToInt32(item["StudentAttendanceId"]);
                    std.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"].ToString());
                    std.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                    std.WorkingDays = Convert.ToInt32(item["WorkingDays"].ToString());
                    std.Leaves = Convert.ToInt32(item["Leaves"].ToString());
                    std.Absents = Convert.ToInt32(item["Absentees"].ToString());
                    std.TotalPercentage = Convert.ToChar(item["TotalPercentage"]);
                    std.PaperTerm = item["PaperTerm"].ToString();
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
