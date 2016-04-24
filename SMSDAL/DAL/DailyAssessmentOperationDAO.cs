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
    public class DailyAssessmentOperationDAO
    {
        private readonly IDatabase gObjDatabase;
        public DailyAssessmentOperationDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public int InsertUpdateDailyAssessmentOperation(DailyAssessmentOperation dAssessmentOpertion)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Report_InsertUpdateDailyAssessmentOperation"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@DailyAssessmentOpertationId", DbType.Int32, dAssessmentOpertion.DailyAssessmentOpertationId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, dAssessmentOpertion.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, dAssessmentOpertion.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ParentAssessmentId", DbType.Int32, dAssessmentOpertion.ParentAssessmentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentSubTypeId", DbType.Int32, dAssessmentOpertion.AssessmentSubTypeId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentFormat", DbType.Boolean, dAssessmentOpertion.AssessmentFormat);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssementStatus", DbType.String, dAssessmentOpertion.AssementStatus);
                    gObjDatabase.AddInParameter(objDbCommand, "@WorseConsequence", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.WorseConsequence)?(object)dAssessmentOpertion.WorseConsequence:dAssessmentOpertion.WorseConsequence);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, dAssessmentOpertion.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, dAssessmentOpertion.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(dAssessmentOpertion.ModifiedById)?DBNull.Value:(object)dAssessmentOpertion.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, dAssessmentOpertion.ModifiedDate==null?(object)DBNull.Value:dAssessmentOpertion.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@DailyAssessmentOpertationnewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (dAssessmentOpertion.DailyAssessmentOpertationId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@DailyAssessmentOpertationnewId"].Value);
                        return identity;
                    }
                    else if (dAssessmentOpertion.DailyAssessmentOpertationId > 0)
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
