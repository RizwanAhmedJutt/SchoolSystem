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
    public class TeacherAssignedCouresDAO
    {
        private readonly IDatabase gObjDatabase;
        public TeacherAssignedCouresDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetTeacherAssignedCourse()
        {
            DataTable dtTeacherDetail;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Course_GetALLTeacherAssignCourse"))
                {
                    
                    dtTeacherDetail = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTeacherDetail;
        }
        public DataTable GetTeacherAssignedCourseByCourseName(string CourseName)
        {
            DataTable dtTeacherDetail;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Course_GetALLTeacherAssignCourseByCourseName"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@CourseName", DbType.String, CourseName);
                    dtTeacherDetail = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTeacherDetail;
        }
        public DataTable GetTeacherAssignedCourseById(int TSssignCId)
        {
            DataTable dtTeacherDetail;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Course_GetTeacherAssignedCourseById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@TAssignCId", DbType.Int32, TSssignCId);
                    dtTeacherDetail = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTeacherDetail;
        }
        public int InsertUpdateAssignedCourse(TeacherAssignedCourse teacherCourse)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Course_TeacherAssignCourseInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherAssignedCourseId", DbType.Int32, teacherCourse.TeacherAssignedCourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, teacherCourse.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, teacherCourse.CourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ClassId", DbType.Int32, teacherCourse.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, teacherCourse.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, teacherCourse.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, teacherCourse.ModifiedById == null ? DBNull.Value : (object)teacherCourse.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, teacherCourse.ModifiedDate == null ? DBNull.Value : (object)teacherCourse.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@AssignedCoursenewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);



                    if (teacherCourse.TeacherAssignedCourseId == 0)
                    {


                        int getCourseId = Convert.ToInt32(objDbCommand.Parameters["@AssignedCoursenewId"].Value);
                        return getCourseId;
                    }
                    else if (teacherCourse.TeacherAssignedCourseId > 0)
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
