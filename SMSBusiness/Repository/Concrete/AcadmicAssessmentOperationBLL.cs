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
    public class AcadmicAssessmentOperationBLL : IAcadmicAssessmentOperation
    {

        public int AddChangesAcadmicAssessmentOperation(AcadmicAssessmentOperation dAssessmentOperation)
        {
            var objAssessmentDao = new AcadmicAssessmentOperationDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAssessmentDao.InsertUpdateAcadmicAssessmentOperation(dAssessmentOperation);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

        public List<Course> GetStudentAssessmentCourse(int? StudentId, int? AcadmicClassId, string Month)
        {
            var objAssessmentDao = new AcadmicAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetStudentAssessmentCourse(StudentId, AcadmicClassId, Month);
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

        public StringBuilder StudentAssessmentCourseIDs(int? StudentId, int? AcadmicClassId, string Month)
        {
            var objAssessmentDao = new AcadmicAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetStudentAssessmentCourse(StudentId, AcadmicClassId, Month);
            StringBuilder CourseIDs = new StringBuilder();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var c = new Course();
                    c.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                    CourseIDs.AppendLine(c.CourseId.ToString() +",");
                }
                return CourseIDs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AcadmicAssessmentOperation> GetStudentAssessmentByCourses(int? StudentId, int? AcadmicClassId, string Month, StringBuilder CourseIDs)
        {
            var objAssessmentDao = new AcadmicAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetStudentAssessmentByCourses(StudentId, AcadmicClassId, Month,CourseIDs);
            List<AcadmicAssessmentOperation> AcadmicAssessment = new List<AcadmicAssessmentOperation>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var op = new AcadmicAssessmentOperation();
                    op.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    op.AssessmentName = item.IsNull("AssementName") ? string.Empty : item["AssementName"].ToString();
                    op.AverageConsequence= item.IsNull("AverageConsequence") ? string.Empty : item["AverageConsequence"].ToString();
                    op.WorseConsequence = item.IsNull("WorseConsequenec") ? string.Empty : item["WorseConsequenec"].ToString();
                    op.AssementStatus = item.IsNull("AssementStatus") ? string.Empty : item["AssementStatus"].ToString();
                    
                    AcadmicAssessment.Add(op);
                }
                return AcadmicAssessment;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        } 


        public List<DailyAssessmentOperation> GetStudentGeneralAssessmentResult(int? StudentId, int? AcadmicClassId, string Month)
        {
            var objAssessmentDao = new AcadmicAssessmentOperationDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetStudentGeneralAssessmentResult(StudentId, AcadmicClassId, Month);
            List<DailyAssessmentOperation> GeneralAssessment = new List<DailyAssessmentOperation>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var op = new DailyAssessmentOperation();
                    op.ParentAssessmentId = item.IsNull("AssessmentTypeId") ? 0 : Convert.ToInt32(item["AssessmentTypeId"]);
                    op.AssessmentName = item.IsNull("AssementName") ? string.Empty : item["AssementName"].ToString();
                    op.AssementStatus = item.IsNull("AssementStatus") ? string.Empty : item["AssementStatus"].ToString();
                    op.WorseConsequence = item.IsNull("WorseConsequence") ? string.Empty : item["WorseConsequence"].ToString();
                    GeneralAssessment.Add(op);
                }
                return GeneralAssessment;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
