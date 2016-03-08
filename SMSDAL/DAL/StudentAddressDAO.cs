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
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, studentAddress.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@PresentAddress", DbType.String, studentAddress.PresentAddress);
                    gObjDatabase.AddInParameter(objDbCommand, "@PermanentAddress", DbType.String, studentAddress.PermanentAddress);
                    gObjDatabase.AddInParameter(objDbCommand, "@CityId", DbType.Int32, studentAddress.CityId);
                    gObjDatabase.AddOutParameter(objDbCommand, "@StudentAddressNewId", DbType.Int32, 4);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);

                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                   
                    if (studentAddress.StudentAddressId == 0)
                    {
                        var identity = Convert.ToInt32(objDbCommand.Parameters["@StudentId"].Value);

                        int getStudentId = Convert.ToInt32(objDbCommand.Parameters["@StudentId"].Value);
                        return getStudentId;
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
