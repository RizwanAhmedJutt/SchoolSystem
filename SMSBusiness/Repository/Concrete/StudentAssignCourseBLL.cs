using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class StudentAssignCourseBLL : IStudentAssignCourse
    {

        public List<StudentAssignedCourse> GetStudentAssignedCourse()
        {
            var objAssignCourseDao = new StudentAssignCourseDAO(new SqlDatabase());
            DataTable tblCourse;

            List<StudentAssignedCourse> objstdAssignCourse = new List<StudentAssignedCourse>();
            try
            {
                tblCourse = objAssignCourseDao.GetStudentAssignedCourse();
                if (tblCourse.Rows.Count > 0)
                {
                    foreach (DataRow item in tblCourse.Rows)
                    {
                        StudentAssignedCourse Assigncourse = new StudentAssignedCourse();
                        Assigncourse.AssignCourseId = Convert.ToInt32(item["CourseAssignId"]);
                        Assigncourse.CourseName = item["CourseName"].ToString();
                        Assigncourse.StudentName = item["StudentName"].ToString();
                        Assigncourse.ClassName = item["ClassName"].ToString();
                        Assigncourse.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                        Assigncourse.StudentId = item.IsNull("StudentId") ? 0 : Convert.ToInt32(item["StudentId"]);
                        Assigncourse.AcadmicClassId = item.IsNull("AcadmicClassId")?0:Convert.ToInt32(item["AcadmicClassId"]);
                        Assigncourse.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                        objstdAssignCourse.Add(Assigncourse);
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objstdAssignCourse;



        }
        public List<StudentAssignedCourse> GetStudentAssignedCourseByCourseName(string CourseName)
        {
            var objAssignCourseDao = new StudentAssignCourseDAO(new SqlDatabase());
            DataTable tblCourse;

            List<StudentAssignedCourse> objstdAssignCourse = new List<StudentAssignedCourse>();
            try
            {
                tblCourse = objAssignCourseDao.GetStudentAssignedCourseByName(CourseName);
                if (tblCourse.Rows.Count > 0)
                {
                    foreach (DataRow item in tblCourse.Rows)
                    {
                        StudentAssignedCourse Assigncourse = new StudentAssignedCourse();
                        Assigncourse.AssignCourseId = Convert.ToInt32(item["CourseAssignId"]);
                        Assigncourse.CourseName = item["CourseName"].ToString();
                        Assigncourse.StudentName = item["StudentName"].ToString();
                        Assigncourse.ClassName = item["ClassName"].ToString();
                        Assigncourse.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                        Assigncourse.StudentId = item.IsNull("StudentId") ? 0 : Convert.ToInt32(item["StudentId"]);
                        Assigncourse.AcadmicClassId = item.IsNull("AcadmicClassId") ? 0 : Convert.ToInt32(item["AcadmicClassId"]);
                        objstdAssignCourse.Add(Assigncourse);
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objstdAssignCourse;



        }
        public StudentAssignedCourse GetStudentAssignedCourseById(int StdAssignCId)
        {
            var objAssignCourseDao = new StudentAssignCourseDAO(new SqlDatabase());
            DataTable tblCourse;
            StudentAssignedCourse Assigncourse = new StudentAssignedCourse();
            try
            {
                tblCourse = objAssignCourseDao.GetStudentAssignedCourseById(StdAssignCId);
                if (tblCourse.Rows.Count > 0)
                {
                    foreach (DataRow item in tblCourse.Rows)
                    {
                        Assigncourse.AssignCourseId = Convert.ToInt32(item["CourseAssignId"]);
                        Assigncourse.CourseName = item["CourseName"].ToString();
                        Assigncourse.StudentName = item["StudentName"].ToString();
                        Assigncourse.AcadmicClassId = item.IsNull("AcadmicClassId")?0:  Convert.ToInt32(item["AcadmicClassId"]);
                        Assigncourse.StudentId = item.IsNull("StudentId") ? 0 : Convert.ToInt32(item["StudentId"]);
                        Assigncourse.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);


                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Assigncourse;



        }

        public int InsertUpdateAssignedCourseAddChanges(StudentAssignedCourse stdAssigncourese)
        {
            var objcourseDao = new StudentAssignCourseDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objcourseDao.InsertUpdateStudentAssignedCourse(stdAssigncourese);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }


    }
}
