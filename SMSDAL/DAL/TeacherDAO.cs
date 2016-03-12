using SMSDataContract.Common;
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
    public class TeacherDAO
    {
        private readonly IDatabase gObjDatabase;
        public TeacherDAO(IDatabase database)
        {
            gObjDatabase = database;
        }


        #region Mean Teacher create,Update, Delete and Insert Data Access Layer
        /// <summary>
        /// Get All Teacher
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLTeachers()
        {
            DataTable dtTeachers;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetAllTeachers"))
                {

                    dtTeachers = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTeachers;


        }

        /// <summary>
        /// Get Teacher By Id
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public DataTable GetTeacherById(int TeacherId)
        {
            DataTable dtStudentDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetTeacherById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@TeacherId", DbType.Int32, TeacherId);
                    dtStudentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtStudentDetails;
        }

        /// <summary>
        /// Insert, Update Teacher
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int InsertUpdateTeacher(Teacher teacher)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_InsertUpdateTeacher"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, teacher.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String, teacher.FirstName);
                    gObjDatabase.AddInParameter(objDbCommand, "@LastName", DbType.String, teacher.LastName);
                    gObjDatabase.AddInParameter(objDbCommand, "@LastQualification", DbType.String, teacher.LastQualification);
                    gObjDatabase.AddInParameter(objDbCommand, "@CNIC", DbType.String, teacher.CNIC);
                    gObjDatabase.AddInParameter(objDbCommand, "@ClassId", DbType.DateTime, teacher.JoinDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@NoOfSibling", DbType.DateTime, teacher.LeaveDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@NoOfSIblingCurrentSchool", DbType.String, teacher.RefrenceName);
                    gObjDatabase.AddInParameter(objDbCommand, "@RollNumber", DbType.String, teacher.RefrenceContact);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, teacher.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, teacher.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, teacher.ModifiedDate == null ? DBNull.Value : (object)teacher.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(teacher.ModifiedById) ? DBNull.Value : (object)teacher.ModifiedById);
                    gObjDatabase.AddOutParameter(objDbCommand, "@TeachernewId", DbType.Int32, 4);

                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);


                    if (teacher.TeacherId == 0)
                    {
                        var identity = Convert.ToInt32(objDbCommand.Parameters["@TeachernewId"].Value);
                        return (int)identity;
                    }
                    else if (teacher.TeacherId > 0)
                    {
                        if (Convert.ToInt32(returnParameter.Value) > 0)
                            return Convert.ToInt32(returnParameter.Value);
                    }

                }
            }
            catch
            {
                throw;
            }

            return 0; 
        }

        /// <summary>
        /// Delete Teacher By Id
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int DeleteStudent(Teacher teacher)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_DeleteTeacherRecord"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, teacher.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, teacher.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.Int32, teacher.ModifiedById);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    var returnValues = returnParameter.Value;
                    if ((int)returnValues == 1)
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


        #endregion
    }
}
