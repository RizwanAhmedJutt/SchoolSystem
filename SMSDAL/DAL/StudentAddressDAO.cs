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
    public class StudentAddressDAO
    {

        private readonly IDatabase gObjDatabase;
        public StudentAddressDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetStudentInfoByStudentId(int StudentId)
        {
            DataTable dtStudentAddressDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetStudentAddressByStudentId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    dtStudentAddressDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtStudentAddressDetails;
        }
        public int InsertUpdateStudentAddress(StudentAddress studentAddress)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_StudentAddressInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentAddressId", DbType.Int32, studentAddress.StudentAddressId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.String, studentAddress.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@PresentAddress", DbType.String, studentAddress.PresentAddress);
                    gObjDatabase.AddInParameter(objDbCommand, "@PermanentAddress", DbType.String, studentAddress.PermanentAddress);

                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter parm = new SqlParameter("@StudentAddressNewId", SqlDbType.Int);
                    parm.Size = 4;
                    parm.Direction = ParameterDirection.Output; // This is important!
                    objDbCommand.Parameters.Add(parm);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (studentAddress.StudentAddressId == 0)
                    {
                        var identity = parm.Value;
                        return (int)identity;
                    }
                    else if (studentAddress.StudentAddressId > 0)
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
