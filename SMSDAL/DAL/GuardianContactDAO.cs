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
    public class GuardianContactDAO
    {
        private readonly IDatabase gObjDatabase;
        public GuardianContactDAO(IDatabase database)
        {
            gObjDatabase = database;
        }


        public DataTable GetGuardianContactInfoByGuardianId(int GuardianId)
        {
            DataTable dtGuardianContactDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetGuardianContactByGuardianId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@GuardianId", DbType.Int32, GuardianId);
                    dtGuardianContactDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtGuardianContactDetails;
        }
        public int InsertUpdateGuardianContact(GuardianContacts guardianContact)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_GuardianContactInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@GuardianContactId", DbType.Int32, guardianContact.GuardianContactId);
                    gObjDatabase.AddInParameter(objDbCommand, "@GuardianId", DbType.Int32, guardianContact.GuardianId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Contact1", DbType.String, guardianContact.FirstContact);
                    gObjDatabase.AddInParameter(objDbCommand, "@Contact2", DbType.String, guardianContact.SecondContact);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter parm = new SqlParameter("@GuardianNewContactId", SqlDbType.Int);
                    parm.Size = 4;
                    parm.Direction = ParameterDirection.Output; // This is important!
                    objDbCommand.Parameters.Add(parm);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (guardianContact.GuardianContactId == 0)
                    {
                        var identity = parm.Value;
                        return (int)identity;
                    }
                    else if (guardianContact.GuardianContactId > 0)
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
