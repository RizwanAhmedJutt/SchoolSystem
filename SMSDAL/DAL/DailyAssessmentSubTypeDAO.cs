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
    public class DailyAssessmentSubTypeDAO
    {


        private readonly IDatabase gObjDatabase;
        public DailyAssessmentSubTypeDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetALLDailyAssessmentSubType()
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand("sp_Report_GetALLDailyAssessmentSubType "))
                {

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;

        }
        public DataTable GetALLAcadmicAssessmentSubType()
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand("sp_report_GetALLAcadmicAssessmentSubType"))
                {

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;

        }
        public DataTable GetALLTeacherGeneralAssessmentSubType()
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand("sp_report_GetALLTeacherGeneralAssessmentSubType"))
                {

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;

        }
        public DataTable GetDailyAssessmentSubTypeById(int AssessmentSubTypeId)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_GetAssessmentSubTypeById "))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AssessmentSubTypeId", DbType.Int32, AssessmentSubTypeId);

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
        public int InsertUpdateDailyAssessmentSubType(DailyAssessmentSubType dAssessmentSubType)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Report_DailyAssessmentSubTypeInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentSubTypeId", DbType.Int32, dAssessmentSubType.AssessmentSubTypeId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentTypeId", DbType.Int32, dAssessmentSubType.AssessmentTypeId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentSubTypeName", DbType.String, dAssessmentSubType.AssessmentSubTypeName);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentFormat", DbType.Boolean, dAssessmentSubType.AssessmentFormat);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, dAssessmentSubType.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, dAssessmentSubType.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(dAssessmentSubType.ModifiedById) ? (object)dAssessmentSubType.ModifiedById : dAssessmentSubType.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, dAssessmentSubType.ModifiedDate == null ? (object)DBNull.Value : dAssessmentSubType.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@AssessmentSubTypenewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (dAssessmentSubType.AssessmentSubTypeId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@AssessmentSubTypenewId"].Value);
                        return identity;
                    }
                    else if (dAssessmentSubType.AssessmentSubTypeId > 0)
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
        public int DeleteStudentGeneralAssessment(int AcadmicClassId, int StudentId, DateTime CreateDate)
        {
            int DeleteStatus = 0;
            try
            {
                var query = "DElETE From DailyAssementOperation" +
                " WHERE DailyAssementOperation.StudentId=" + StudentId + " AND DailyAssementOperation.AcadmicClassId=" + AcadmicClassId
               + " AND CONVERT(date, DailyAssementOperation.CreatedDate)=" +"'"+CreateDate +"'";
                using (DbCommand objDbCommand = gObjDatabase.GetSqlStringCommand(query))
                {
                    DeleteStatus = gObjDatabase.ExecuteNonQuery(objDbCommand);

                }
                return DeleteStatus;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int DeleteStudentAcadmicAssessment(int AcadmicClassId, int StudentId, int CourseId, string CreateDate)
        {
            int DeleteStatus = 0;
            try
            {
                var query = "Delete From AcadmicAssessmentOperation" +
                " WHERE AcadmicAssessmentOperation.StudentId=" + StudentId +
                " AND AcadmicAssessmentOperation.AcadmicClassId=" + AcadmicClassId +
                " AND CONVERT(date, AcadmicAssessmentOperation.CreatedDate)=" +"'"+CreateDate +"'" +
                " AND CourseId=" + CourseId;
                using (DbCommand objDbCommand = gObjDatabase.GetSqlStringCommand(query))
                {
                    DeleteStatus = gObjDatabase.ExecuteNonQuery(objDbCommand);

                }
                return DeleteStatus;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int DeleteTeacherAssessment(int AcadmicClassId, int TeacherId, int CourseId, string CreateDate)
        {
            int DeleteStatus = 0;
            try
            {
                var query = " DELETE FROM TeacherAssessmentOperation" +
                    " WHERE TeacherAssessmentOperation.AcadmicClassId=" + AcadmicClassId +
                    " AND TeacherAssessmentOperation.TeacherId=" + TeacherId +
                    " AND TeacherAssessmentOperation.CourseId=" + CourseId +
                    " AND  CONVERT(date, TeacherAssessmentOperation.CreatedDate)=" + "'"+CreateDate+"'";
                using (DbCommand objDbCommand = gObjDatabase.GetSqlStringCommand(query))
                {
                    DeleteStatus = gObjDatabase.ExecuteNonQuery(objDbCommand);
                }
                return DeleteStatus;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable CheckSubAssementExist(int ParentAssessmentId, string SubAssessmentName)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_CheckSubAssessmentType"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@ParentAssessmentId", DbType.Int32, ParentAssessmentId);
                    gObjDatabase.AddInParameter(objCommand, "@AssessmentName", DbType.String, SubAssessmentName);

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
        public DataTable GetStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, DateTime CreateDate)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_GetStudentGeneralAssessment"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    gObjDatabase.AddInParameter(objCommand, "@CreatDate", DbType.DateTime, CreateDate);
                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }

        public DataTable GetStudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_GetStudentAcadmicAssessment"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    gObjDatabase.AddInParameter(objCommand, "@CourseId", DbType.Int32, CourseId);
                    gObjDatabase.AddInParameter(objCommand, "@CreatDate", DbType.Date, CreateDate);
                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
        public DataTable GetTeacherAcadmicAssessments(int? AcadmicClassId, int? TeacherId, int? CourseId, DateTime CreateDate)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_GetTeacherAcadmicAssessments"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@TeacherId", DbType.Int32, TeacherId);
                    gObjDatabase.AddInParameter(objCommand, "@CourseId", DbType.Int32, CourseId);
                    gObjDatabase.AddInParameter(objCommand, "@CreatDate", DbType.Date, CreateDate);
                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }

        public DataTable GetStudentSingleGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_GetStudentSingleGeneralAssessment"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    gObjDatabase.AddInParameter(objCommand, "@CreatDate", DbType.Date, CreateDate);
                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
        public DataTable GetStudentSingleAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_GetStudentSingleAcadmicAssessment"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    gObjDatabase.AddInParameter(objCommand, "@CourseId", DbType.Int32, CourseId);
                    gObjDatabase.AddInParameter(objCommand, "@CreatDate", DbType.Date, CreateDate);
                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
        public DataTable GetTeacherSingleAcadmicAssessment(int? AcadmicClassId, int? TeacherId, int? CourseId, string CreateDate)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Report_GetTeacherSingleAcadmicAssessment"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@CourseId", DbType.Int32, CourseId);
                    gObjDatabase.AddInParameter(objCommand, "@TeacherId", DbType.Int32, TeacherId);
                    gObjDatabase.AddInParameter(objCommand, "@CreatDate", DbType.Date, CreateDate);
                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }

        public DataTable GetLastReportCreatedDate(int? AcadmicClassId, int? StudentId)
        {
            DataTable dtDate;
            try
            {
                var query = "Select top 1 CreatedDate From DailyAssementOperation Where  StudentId=" + StudentId + " AND   AcadmicClassId=" + AcadmicClassId;
                using (DbCommand objDbCommand = gObjDatabase.GetSqlStringCommand(query))
                {
                    dtDate = gObjDatabase.GetDataTable(objDbCommand);

                }
                return dtDate;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
