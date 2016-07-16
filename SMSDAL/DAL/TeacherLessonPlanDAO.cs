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
    public class TeacherLessonPlanDAO
    {

        private readonly IDatabase gObjDatabase;
        public TeacherLessonPlanDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public int AddChangesLessonPlan(TeacherLessonPlan LessonPlan)
        {

            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_TeacherLesson_InsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherLessonPlanId", DbType.Int32, LessonPlan.TeacherLessonPlanId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, LessonPlan.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, LessonPlan.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, LessonPlan.CourseId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Lesson", DbType.String, LessonPlan.Lesson);
                    gObjDatabase.AddInParameter(objDbCommand, "@Topic", DbType.String, LessonPlan.Topic);
                    gObjDatabase.AddInParameter(objDbCommand, "@SubTopic", DbType.String, LessonPlan.SubTopic);
                    gObjDatabase.AddInParameter(objDbCommand, "@Objective", DbType.String, LessonPlan.Objective);
                    gObjDatabase.AddInParameter(objDbCommand, "@OutComes", DbType.String, LessonPlan.OutComes);
                    gObjDatabase.AddInParameter(objDbCommand, "@TeachingMethodology", DbType.String, LessonPlan.TeachingMethodology);
                    gObjDatabase.AddInParameter(objDbCommand, "@ResourceRequired", DbType.String, LessonPlan.ResourceRequired);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, LessonPlan.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, LessonPlan.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(LessonPlan.ModifiedById) ? DBNull.Value : (object)LessonPlan.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, LessonPlan.ModifiedDate == null ? (object)DBNull.Value : LessonPlan.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@TeacherLessonPlannewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (LessonPlan.TeacherLessonPlanId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@TeacherLessonPlannewId"].Value);
                        return identity;
                    }
                    else if (LessonPlan.TeacherLessonPlanId > 0)
                    {
                        var UpdateValue = returnParameter.Value;
                        return (int)UpdateValue;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 0;  // show Error in inserting or Updating Record

        }

        public DataTable GetTeacherLessons(int? AcadmicClassId, int? TeacherId, int? CourseId)
        {
            DataTable LessonPlan;


            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_TeacherLesson_GetTeacherLessons"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@TeacherId", DbType.Int32, TeacherId);
                    gObjDatabase.AddInParameter(objCommand, "@CourseId", DbType.Int32, CourseId);

                    LessonPlan = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LessonPlan;
        }
        public DataTable GetTeacherLessonPlan(int LessonPlanId)
        {
            DataTable LessonPlan;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_TeacherLesson_GetTeacherLessonById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@LessonPlanId", DbType.Int32, LessonPlanId);
                    LessonPlan = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return LessonPlan;
        }
    }
}
