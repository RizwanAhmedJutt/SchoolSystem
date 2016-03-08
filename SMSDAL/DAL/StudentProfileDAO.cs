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
  public  class StudentProfileDAO
    {
        private readonly IDatabase gObjDatabase;

        public StudentProfileDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetStudentInfoByStudentId(int StudentId)
        {
            DataTable dtStudentProfileDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetStudentProfileDetailByStudentId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    dtStudentProfileDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtStudentProfileDetails;
        }
        public int InsertUpdateStudentProfile(StudentProfile studentProfile)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_StudentProfileInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@ProfileId", DbType.Int32, studentProfile.ProfileId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.String, studentProfile.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ImagePath", DbType.String, studentProfile.ImagePath);
                   
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter parm = new SqlParameter("@StudentAddressNewId", SqlDbType.Int);
                    parm.Size = 4;
                    parm.Direction = ParameterDirection.Output; // This is important!
                    objDbCommand.Parameters.Add(parm);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (studentProfile.ProfileId == 0)
                    {
                        var identity = parm.Value;
                        return (int)identity;
                    }
                    else if (studentProfile.ProfileId > 0)
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
