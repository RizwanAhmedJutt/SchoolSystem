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
    public class StudentResultSocialDescriptionDAO
    {
         private readonly IDatabase gObjDatabase;
         public StudentResultSocialDescriptionDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetALLSocialDescriptions()
        {
            DataTable dtDescriptionDetails;
            try
            {
                var getQuery = "Select * from StudentResultSocialDescription";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getQuery))
                {

                    dtDescriptionDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtDescriptionDetails;
        }
        public int AddChangessocialDescriptions(StudentResultSocialDescription socialDescription)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Result_InsertUpdateSocialDescription"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@socialDescriptionId", DbType.Int32, socialDescription.SocialDescriptionId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Description", DbType.String, socialDescription.Description);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, socialDescription.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, socialDescription.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, socialDescription.ModifiedDate==null?null:socialDescription.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(socialDescription.ModifiedById) ? null : socialDescription.ModifiedById);
                    gObjDatabase.AddOutParameter(objDbCommand, "@SocialDescriptionnewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (socialDescription.SocialDescriptionId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@SocialDescriptionnewId"].Value);
                        return identity;
                    }
                    else 
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

            // return 0;  // show Error in inserting or Updating Record


        }

    }
}
