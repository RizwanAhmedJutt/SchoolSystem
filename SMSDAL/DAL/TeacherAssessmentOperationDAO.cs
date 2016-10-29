using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL.DAL
{
    public class TeacherAssessmentOperationDAO
    {
        private readonly IDatabase gObjDatabase;
        public TeacherAssessmentOperationDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public int InsertUpdateTeacherAssessmentOperation(TeacherAssessmentOperation dAssessmentOpertion)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Report_InsertUpdateTeacherAssessmentOperation"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherAssessmentOperationId", DbType.Int32, dAssessmentOpertion.TeacherAssessmentOperationId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, dAssessmentOpertion.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, dAssessmentOpertion.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, dAssessmentOpertion.CourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ParentAssessmentId", DbType.Int32, dAssessmentOpertion.ParentAssessmentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentFormat", DbType.Boolean, dAssessmentOpertion.AssessmentFormat);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssementStatus", DbType.String, dAssessmentOpertion.AssementStatus);
                    gObjDatabase.AddInParameter(objDbCommand, "@AverageConsequence", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.AverageConsequence) ? (object)dAssessmentOpertion.AverageConsequence : dAssessmentOpertion.AverageConsequence);
                    gObjDatabase.AddInParameter(objDbCommand, "@WorseConsequence", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.WorseConsequence) ? (object)dAssessmentOpertion.WorseConsequence : dAssessmentOpertion.WorseConsequence);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, dAssessmentOpertion.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, dAssessmentOpertion.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.ModifiedById) ? DBNull.Value : (object)dAssessmentOpertion.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, dAssessmentOpertion.ModifiedDate == null ? (object)DBNull.Value : dAssessmentOpertion.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@TeacherAssessmentOperationnewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (dAssessmentOpertion.TeacherAssessmentOperationId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@TeacherAssessmentOperationnewId"].Value);
                        return identity;
                    }
                    else if (dAssessmentOpertion.TeacherAssessmentOperationId > 0)
                    {
                        var UpdateValue = returnParameter.Value;
                        return (int)UpdateValue;
                    }

                }
            }
            catch
            {
                throw;
            }

            return 0;  // show Error in inserting or Updating Record
        }
        public DataTable GetTeacherAssessmentCourse(int? TeacherId, int? AcadmicClassId, string Month)
        {
            DataTable course;
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select distinct c.CourseId,");
            query.AppendLine("c.CourseName");
            query.AppendLine("from TeacherAssessmentOperation op");
            query.AppendLine("Left   Join Courses c on c.CourseId=op.CourseId");
            query.AppendLine("WHERE  op.TeacherId= " + TeacherId);
            query.AppendLine("AND    op.AcadmicClassId= " + AcadmicClassId);
            query.AppendLine("AND    DATENAME(MONTH,op.CreatedDate) + ' ' + DateName( Year, op.CreatedDate )='" + Month + "'");

            try
            {
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query.ToString()))
                {

                    course = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return course;
        }
        public DataTable GetTeacherMonthAssessmentResult( int? AcadmicClassId,int? TeacherId, StringBuilder CourseIDs, string Month)
        {

            DataTable assessment;
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select c.CourseName,");
            query.AppendLine("daType.AssementName,");
            query.AppendLine("Max(op.AssementStatus) as AssementStatus,");
            query.AppendLine("MAX(op.AverageConsequence) as AverageConsequence,");
            query.AppendLine("MAX(op.WorseConsequenec) as WorseConsequenec");
            query.AppendLine("from TeacherAssessmentOperation op");
            query.AppendLine("Left   Join DailyAssementType daType on op.ParentAssessmentId=daType.AssessmentTypeId");
            query.AppendLine("Left Join Courses c on c.CourseId=op.CourseId");
            query.AppendLine("AND    op.AcadmicClassId= " + AcadmicClassId);
            query.AppendLine("WHERE    op.TeacherId= " + TeacherId);
            query.AppendLine("AND    op.CourseId in (" + CourseIDs.Replace(",", "", CourseIDs.ToString().LastIndexOf(","), 1) + ")");
            query.AppendLine("AND    DATENAME(MONTH,op.CreatedDate) + ' ' + DateName( Year, op.CreatedDate )='" + Month + "'");
            query.AppendLine("Group by  c.CourseName,daType.AssementName");
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query.ToString()))
                {

                    assessment = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return assessment;
        }
        public DataTable GetTeacherMonthAssessmentResultByClass(string AcadmicClassIds, int? TeacherId,string Month)
        {

            DataTable assessment;
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select c.CourseName,");
            query.AppendLine("daType.AssementName,");
            query.AppendLine("op.AcadmicClassId,");
            query.AppendLine("op.AssementStatus as AssementStatus,");
            query.AppendLine("op.AverageConsequence as AverageConsequence,");
            query.AppendLine("op.WorseConsequenec as WorseConsequenec");
            query.AppendLine("from TeacherAssessmentOperation op");
            query.AppendLine("Left   Join DailyAssementType daType on op.ParentAssessmentId=daType.AssessmentTypeId");
            query.AppendLine("Left Join Courses c on c.CourseId=op.CourseId");
            query.AppendLine("WHERE    op.TeacherId= " + TeacherId);
            query.AppendLine("AND      op.AcadmicClassId in (" + AcadmicClassIds + ")");
            query.AppendLine("AND    DATENAME(MONTH,op.CreatedDate) + ' ' + DateName( Year, op.CreatedDate )='" + Month + "'");
            //query.AppendLine("Group by  c.CourseName,daType.AssementName");
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query.ToString()))
                {

                    assessment = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return assessment;
        }

        public DataTable GetTeacherAssessmentCourseByClass(int? TeacherId, string AcadmicClassId, string Month)
        {
            DataTable course;
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select distinct c.CourseId,");
            query.AppendLine("c.CourseName,");
            query.AppendLine("c.ClassId,");
            query.AppendLine("ac.ClassName");
            query.AppendLine("from TeacherAssessmentOperation op");
            query.AppendLine("Left   Join Courses c on c.CourseId=op.CourseId");
            query.AppendLine("Left   Join AcadmicClass ac on ac.AcadmicClassId=c.ClassId");
            query.AppendLine("WHERE  op.TeacherId= " + TeacherId);
            query.AppendLine("AND    op.AcadmicClassId in ( " + AcadmicClassId +" )");
            query.AppendLine("AND    DATENAME(MONTH,op.CreatedDate) + ' ' + DateName( Year, op.CreatedDate )='" + Month + "'");
            query.AppendLine("order by c.ClassId");

            try
            {
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query.ToString()))
                {

                    course = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return course;
        }
    }
}
