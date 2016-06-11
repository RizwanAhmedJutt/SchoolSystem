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
    public class AcadmicAssessmentOperationDAO
    {
        private readonly IDatabase gObjDatabase;
        public AcadmicAssessmentOperationDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public int InsertUpdateAcadmicAssessmentOperation(AcadmicAssessmentOperation dAssessmentOpertion)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Report_InsertUpdateAcadmicAssessmentOperation"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicAssessmentOperationId", DbType.Int32, dAssessmentOpertion.AcadmicAssessmentOperationId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, dAssessmentOpertion.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, dAssessmentOpertion.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, dAssessmentOpertion.CourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ParentAssessmentId", DbType.Int32, dAssessmentOpertion.ParentAssessmentId);
                   // gObjDatabase.AddInParameter(objDbCommand, "@AssessmentSubTypeId", DbType.Int32, dAssessmentOpertion.AssessmentSubTypeId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentFormat", DbType.Boolean, dAssessmentOpertion.AssessmentFormat);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssementStatus", DbType.String, dAssessmentOpertion.AssementStatus);
                    gObjDatabase.AddInParameter(objDbCommand, "@AverageConsequence", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.AverageConsequence) ? (object)dAssessmentOpertion.AverageConsequence : dAssessmentOpertion.AverageConsequence);
                    gObjDatabase.AddInParameter(objDbCommand, "@WorseConsequence", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.WorseConsequence)?(object)dAssessmentOpertion.WorseConsequence:dAssessmentOpertion.WorseConsequence);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, dAssessmentOpertion.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, dAssessmentOpertion.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.ModifiedById)?DBNull.Value:(object)dAssessmentOpertion.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, dAssessmentOpertion.ModifiedDate==null?(object)DBNull.Value:dAssessmentOpertion.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@AcadmicAssessmentOperationnewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (dAssessmentOpertion.AcadmicAssessmentOperationId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@AcadmicAssessmentOperationnewId"].Value);
                        return identity;
                    }
                    else if (dAssessmentOpertion.AcadmicAssessmentOperationId > 0)
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
        /// <summary>
        /// Get All Course which has been selected for evaluation.
        /// </summary>
        /// <returns></returns>
        public DataTable GetStudentAssessmentCourse(int? StudentId, int? AcadmicClassId, string Month)
        {
            DataTable course;
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select distinct c.CourseId,");
            query.AppendLine("c.CourseName");
            query.AppendLine("from AcadmicAssessmentOperation op");
            query.AppendLine("Left Join Courses c on c.CourseId=op.CourseId");
            query.AppendLine("WHERE  op.StudentId= "+StudentId);
            query.AppendLine("AND    op.AcadmicClassId= " + AcadmicClassId);
            query.AppendLine("AND    DATENAME(MONTH,op.CreatedDate)='"+Month+"'");
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query.ToString()))
                {

                    course = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return course;
        } 

        public DataTable GetStudentAssessmentByCourses(int? StudentId, int? AcadmicClassId, string Month,StringBuilder CourseIDs)
        {
            DataTable course;
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select c.CourseName,");
            query.AppendLine("daType.AssementName,");
            query.AppendLine("Max(op.AssementStatus) as AssementStatus,");
            query.AppendLine("MAX(op.AverageConsequence) as AverageConsequence,");
            query.AppendLine("MAX(op.WorseConsequenec) as WorseConsequenec");
            query.AppendLine("from AcadmicAssessmentOperation op");
            query.AppendLine("Left Join DailyAssementType daType on daType.AssessmentTypeId=op.ParentAssessmentId");
            query.AppendLine("Left Join Courses c on c.CourseId=op.CourseId");
            query.AppendLine("WHERE  op.StudentId=" + StudentId);
            query.AppendLine("AND    op.AcadmicClassId=" +AcadmicClassId);
            query.AppendLine("AND    DATENAME(MONTH,op.CreatedDate)='" + Month +"'");
            query.AppendLine("AND    op.CourseId in (" + CourseIDs.Replace(",", "", CourseIDs.ToString().LastIndexOf(","), 1) + ")");
            query.AppendLine("Group  by daType.AssementName,c.CourseName");
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

        public DataTable GetStudentGeneralAssessmentResult(int? StudentId, int? AcadmicClassId, string Month)
        {

            DataTable course;
            StringBuilder query = new StringBuilder();
            query.AppendLine("Select daType.AssessmentTypeId,");
            query.AppendLine("daType.AssementName,");
            query.AppendLine("Sum(Convert(int,op.AssementStatus)) as AssementStatus,");
            query.AppendLine("MAX(op.WorseConsequence) as WorseConsequence");
            query.AppendLine("from DailyAssementOperation op");
            query.AppendLine("Left   Join DailyAssementType daType on op.ParentAssessmentId=daType.AssessmentTypeId");
            query.AppendLine("WHERE  op.StudentId= " + StudentId);
            query.AppendLine("AND    op.AcadmicClassId= " + AcadmicClassId);
            query.AppendLine("AND    DATENAME(MONTH,op.CreatedDate)= '"+Month+"'");
            query.AppendLine("Group by  daType.AssessmentTypeId,daType.AssementName");
            query.AppendLine("order by daType.AssessmentTypeId");
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
