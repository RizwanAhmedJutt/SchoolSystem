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
        public DataTable GetDailyAssessmentSubTypeById(int AssessmentSubTypeId)
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand("sp_Report_GetAssessmentSubTypeById "))
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
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentSubTypeName", DbType.String, dAssessmentSubType.AssessmentSubTypeName);
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
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@AssessmentTypenewId"].Value);
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
    }
}
