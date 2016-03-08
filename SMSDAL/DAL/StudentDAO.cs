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
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("usp_GetAllStudent"))
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
        public int InsertUpdateStudent(Student student)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_InsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, student.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String, student.FirstName);
                    gObjDatabase.AddInParameter(objDbCommand, "@LastName", DbType.String, student.LastName);
                    gObjDatabase.AddInParameter(objDbCommand, "@DateOfBirth", DbType.String, student.DOB);
                    gObjDatabase.AddInParameter(objDbCommand, "@Religion", DbType.String, student.Religion);
                    gObjDatabase.AddInParameter(objDbCommand, "@ClassId", DbType.Int32, student.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@NoOfSibling", DbType.Int32, student.NoOfSibling);
                    gObjDatabase.AddInParameter(objDbCommand, "@NoOfSIblingCurrentSchool", DbType.Int32, student.NoOfSiblingCurrentSchool);
                    gObjDatabase.AddInParameter(objDbCommand, "@RollNumber", DbType.Int32, student.RollNumber);
                    gObjDatabase.AddInParameter(objDbCommand, "@IsActive", DbType.Int32, student.IsActive);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, student.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, student.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, student.ModifiedDate == null? DBNull.Value:(object) student.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(student.ModifiedById)?(object)DBNull.Value:student.ModifiedById);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter parm = new SqlParameter("@StudentnewId", SqlDbType.Int);
                    parm.Size = 4;
                    parm.Direction = ParameterDirection.Output; // This is important!
                    objDbCommand.Parameters.Add(parm);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (student.StudentId == 0)
                    {
                        var identity = parm.Value;
                        return (int)identity;
                    }
                    else if (student.StudentId > 0)
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
