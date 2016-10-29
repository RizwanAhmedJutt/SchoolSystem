using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class TeacherAssessmentOperationBLL : ITeacherAssessmentOperation
    {

        public int AddChangesTeacherAssessmentOperation(TeacherAssessmentOperation dAssessmentOperation)
        {
            var objAssessmentDao = new TeacherAssessmentOperationDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAssessmentDao.InsertUpdateTeacherAssessmentOperation(dAssessmentOperation);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }
        public List<Course> GetTeacherAssessmentCourse(int? TeacherId, int? AcadmicClassId, string Month)
        {
            var objAssessmentDao = new TeacherAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetTeacherAssessmentCourse(TeacherId, AcadmicClassId, Month);
            List<Course> Course = new List<Course>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var c = new Course();
                    c.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                    c.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    Course.Add(c);
                }
                return Course;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public StringBuilder TeacherAssessmentCourseIDs(int? TeacherId, int? AcadmicClassId, string Month)
        {
            var objAssessmentDao = new TeacherAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetTeacherAssessmentCourse(TeacherId, AcadmicClassId, Month);
            StringBuilder CourseIDs = new StringBuilder();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var c = new Course();
                    c.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                    CourseIDs.AppendLine(c.CourseId.ToString() + ",");
                }
                return CourseIDs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Course> GetTeacherAssessmentCourse(int? TeacherId, string AcadmicClassId, string Month)
        {
            var objAssessmentDao = new TeacherAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetTeacherAssessmentCourseByClass(TeacherId, AcadmicClassId, Month);
            List<Course> Course = new List<Course>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var c = new Course();
                    c.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                    c.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    c.ClassId = item.IsNull("ClassId") ? 0 : Convert.ToInt32(item["ClassId"]);
                    c.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    Course.Add(c);
                }
                return Course;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<TeacherAssessmentOperation> GetTeacherMonthAssessmentResult(int? AcadmicClassId, int? TeacherId, StringBuilder CourseIDs, string Month)
        {
            var objAssessmentDao = new TeacherAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetTeacherMonthAssessmentResult(AcadmicClassId, TeacherId, CourseIDs, Month);
            List<TeacherAssessmentOperation> AcadmicAssessment = new List<TeacherAssessmentOperation>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var op = new TeacherAssessmentOperation();
                    op.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    op.AssessmentName = item.IsNull("AssementName") ? string.Empty : item["AssementName"].ToString();
                    op.AverageConsequence = item.IsNull("AverageConsequence") ? string.Empty : item["AverageConsequence"].ToString();
                    op.WorseConsequence = item.IsNull("WorseConsequenec") ? string.Empty : item["WorseConsequenec"].ToString();
                    //  op.AssementStatus = item.IsNull("AssementStatus") ? string.Empty : item["AssementStatus"].ToString();
                    op.AssessmentTotal = item.IsNull("AssementStatus") ? 0 : Convert.ToInt32(item["AssementStatus"]);
                    AcadmicAssessment.Add(op);
                }
                return AcadmicAssessment;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<TeacherAssessmentOperation> GetTeacherMonthAssessmentResultByClass(string AcadmicClassIds, int? TeacherId, string Month)
        {
            var objAssessmentDao = new TeacherAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetTeacherMonthAssessmentResultByClass(AcadmicClassIds, TeacherId, Month);
            List<TeacherAssessmentOperation> AcadmicAssessment = new List<TeacherAssessmentOperation>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var op = new TeacherAssessmentOperation();
                    op.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    op.AssessmentName = item.IsNull("AssementName") ? string.Empty : item["AssementName"].ToString();
                    op.AverageConsequence = item.IsNull("AverageConsequence") ? string.Empty : item["AverageConsequence"].ToString();
                    op.WorseConsequence = item.IsNull("WorseConsequenec") ? string.Empty : item["WorseConsequenec"].ToString();
                    //  op.AssementStatus = item.IsNull("AssementStatus") ? string.Empty : item["AssementStatus"].ToString();
                    op.AssessmentTotal = item.IsNull("AssementStatus") ? 0 : Convert.ToInt32(item["AssementStatus"]);
                    AcadmicAssessment.Add(op);
                }
                return AcadmicAssessment;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
