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
  public  class DailyAssessmentTypeDAO
    {

        private readonly IDatabase gObjDatabase;
        public DailyAssessmentTypeDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public int InsertUpdateDailyAssessmentType(DailyAssessmentType dAssessmentType)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Report_DailyAssessmentTypeInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentTypeId", DbType.Int32, dAssessmentType.AssessmentTypeId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentName", DbType.String, dAssessmentType.AssessmentName);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, dAssessmentType.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, dAssessmentType.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(dAssessmentType.ModifiedById)?(object)dAssessmentType.ModifiedById:dAssessmentType.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, dAssessmentType.ModifiedDate==null?(object)DBNull.Value:dAssessmentType.ModifiedDate;
                    gObjDatabase.AddOutParameter(objDbCommand, "@AssessmentTypenewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (dAssessmentType.AssessmentTypeId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@AssessmentTypenewId"].Value);
                        return identity;
                    }
                    else if (dAssessmentType.AssessmentTypeId > 0)
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
