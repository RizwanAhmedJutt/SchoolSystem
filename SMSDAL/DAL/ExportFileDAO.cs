using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL.DAL
{
    public class ExportFileDAO
    {
        private readonly IDatabase gObjDatabase;
        public ExportFileDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetStudentReport()
        {
            DataTable dtAssessmentDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Export_StudentReport"))
                {

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
        public DataTable GetALLTeachers()
        {
            DataTable dtClassDetails;
            try
            {
                var getTeachers = "Select t.FirstName+''+t.LastName as TeacherName,t.CNIC ,t.LastQualification,t.JoinDate ,t.RefrenceName,t.RefrenceContact,t.LeaveDate from Teacher t WHERE t.Active=1";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getTeachers))
                {

                    dtClassDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtClassDetails;
        }
        public DataTable GetALLTeachersAssignedClass()
        {
            DataTable dtClassDetails;
            try
            {
                var getTeachersClass = "Select t.FirstName+''+t.LastName as TeacherName, ac.ClassName FROM TeacherAssignedClass tAC JOin Teacher t on t.TeacherId=tAC.TeacherId JOin AcadmicClass ac on ac.AcadmicClassId=tAC.ClassId WHERE t.Active=1";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getTeachersClass))
                {

                    dtClassDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtClassDetails;
        }
        public DataTable GetALLCourse()
        {
            DataTable dtClassDetails;
            try
            {
                var getCourses = "Select c.CourseCode, c.CourseName , ac.ClassName   from Courses c Join AcadmicClass ac on ac.AcadmicClassId=c.ClassId WHERE c.Active=1";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getCourses))
                {

                    dtClassDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtClassDetails;
        }
        public DataTable GetALLTeacherAssignCourse()
        {
            DataTable dtClassDetails;
            try
            {
                var getTAssignCourse = "Select t.FirstName+''+t.LastName as teachername,c.CourseName ,ac.ClassName from TeacherAssignedCourse tassignCourse Join Courses c on c.CourseId=tassignCourse.CourseId Join Teacher t on t.TeacherId=tassignCourse.TeacherId Join AcadmicClass ac on ac.AcadmicClassId=tassignCourse.ClassId WHERE t.Active=1 and c.Active=1";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getTAssignCourse))
                {

                    dtClassDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtClassDetails;
        }
        public DataTable GetALLStudentAssignCourse()
        {
            DataTable dtClassDetails;
            try
            {
                var getStdAssignCourse = "Select std.FirstName+''+std.LastName as StudentName,c.CourseName , ac.ClassName from Std_AssignedCourse stdAC Join Student std on std.StudentId=stdAC.StudentId Join Courses c on c.CourseId=stdAC.CourseId join AcadmicClass ac on ac.AcadmicClassId=stdAC.AcadmicClassId WHERE c.Active=1 AND std.IsActive=1";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getStdAssignCourse))
                {

                    dtClassDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtClassDetails;
        }
    }
}
