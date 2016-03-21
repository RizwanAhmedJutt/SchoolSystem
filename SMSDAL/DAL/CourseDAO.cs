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
    public class CourseDAO
    {

        private readonly IDatabase gObjDatabase;
        public CourseDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetALLCourse()
        {

            DataTable dtCourseDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Course_GetALLCourse"))
                {
                    
                    dtCourseDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtCourseDetails;
        }
        public DataTable GetCourseDetailByCourseId(int CourseId)
        {
            DataTable dtCourseDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Course_GetCourseDetailByCourseId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@CourseId", DbType.Int32, CourseId);
                    dtCourseDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtCourseDetails;
        }
        public int InsertUpdateCourse(Course course)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Course_InsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, course.CourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseCode", DbType.String, course.CourseCode);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseName", DbType.String, course.CourseName);
                    gObjDatabase.AddInParameter(objDbCommand, "@ClassId", DbType.Int32, course.ClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Active", DbType.Int32, course.IsActive);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, course.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, course.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, course.ModifiedById == null ? DBNull.Value : (object)course.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, course.ModifiedDate == null ? DBNull.Value : (object)course.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@CoursenewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);



                    if (course.CourseId == 0)
                    {
                       

                        int getCourseId = Convert.ToInt32(objDbCommand.Parameters["@CoursenewId"].Value);
                        return getCourseId;
                    }
                    else if (course.CourseId > 0)
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

        public int DeleteCourse(Course course)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Course_DeleteCourse"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, course.CourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Active", DbType.Boolean, course.IsActive);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, course.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, course.ModifiedById);
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
    }
}
