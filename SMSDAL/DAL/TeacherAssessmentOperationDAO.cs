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


    }
}
