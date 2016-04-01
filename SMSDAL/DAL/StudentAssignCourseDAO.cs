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
  public  class StudentAssignCourseDAO
    {

       private readonly IDatabase gObjDatabase;
       public StudentAssignCourseDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
       public DataTable GetStudentAssignedCourse()
       {
           DataTable dtCStudentDetail;
           try
           {
               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Course_GetALLStudentAssignCourse"))
               {
                 
                   dtCStudentDetail = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtCStudentDetail;
       }
        public DataTable GetAssignedCourseByStudentId(int StudentId)
        {
            DataTable dtStudentDetail;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Course_GetAssignedCourseByStudentId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    dtStudentDetail = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtStudentDetail;
        }
        public int InsertUpdateStudentAssignedCourse(StudentAssignedCourse stdCourse)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Course_StudentAssignCourseInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@AssignCourseId", DbType.Int32, stdCourse.AssignCourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, stdCourse.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, stdCourse.CourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, stdCourse.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, stdCourse.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, stdCourse.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, stdCourse.ModifiedById == null ? DBNull.Value : (object)stdCourse.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, stdCourse.ModifiedDate == null ? DBNull.Value : (object)stdCourse.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@AssignedCoursenewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);



                    if (stdCourse.AssignCourseId == 0)
                    {


                        int getCourseId = Convert.ToInt32(objDbCommand.Parameters["@AssignedCoursenewId"].Value);
                        return getCourseId;
                    }
                    else if (stdCourse.AssignCourseId > 0)
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
