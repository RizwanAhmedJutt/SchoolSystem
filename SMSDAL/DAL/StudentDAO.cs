using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL
{
    public class StudentDAO
    {
         private readonly IDatabase gObjDatabase;
        public StudentDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public DataTable GetALLStudents()
        {
            DataTable dtStudents;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetALLStudents"))
                {
                    
                    dtStudents = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtStudents;


        }
        public DataTable GetStudentInfoByStudentId(int StudentId)
        {
            DataTable dtStudentDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetStudentDetailById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    dtStudentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtStudentDetails;
        }
        

        public int DeleteStudent(Student student)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_DeleteStudentRecord"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, student.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@IsActive", DbType.Int32, student.IsActive);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, student.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.Int32, student.ModifiedById);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    var returnValues = returnParameter.Value;
                    if((int)returnValues==1)
                    {
                        return 1;  // Successfully Deleted/DeActive
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
