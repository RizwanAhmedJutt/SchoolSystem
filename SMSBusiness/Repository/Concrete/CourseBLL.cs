using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class CourseBLL : ICourse
    {
        public List<Course> GetALLCourse()
        {
            var objCourseDao = new CourseDAO(new SqlDatabase());
            DataTable tblCourse;
            tblCourse = objCourseDao.GetALLCourse();
            List<Course> objCourseList = new List<Course>();
            try
            {

                foreach (DataRow dr in tblCourse.Rows)
                {
                    Course course = new Course();
                    course.CourseId = Convert.ToInt32(dr["CourseId"]);
                    course.CourseCode = dr["CourseCode"].ToString();
                    course.CourseName = dr["CourseName"].ToString();
                    course.ClassName = dr["ClassName"].ToString();
                    course.IsActive = Convert.ToBoolean(dr["active"]);
                    course.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);

                    objCourseList.Add(course);
                }

                return objCourseList;

            }
            catch
            {

                throw;
            }


        }
        public Course GetCourseDetailByCourseId(int CourseId)
        {
            var objCourseDao = new CourseDAO(new SqlDatabase());
            DataTable tblCourse;
            Course course = new Course();
            try
            {
                tblCourse = objCourseDao.GetCourseDetailByCourseId(CourseId);
                if (tblCourse.Rows.Count > 0)
                {
                    foreach (DataRow item in tblCourse.Rows)
                    {
                        course.CourseId = Convert.ToInt32(item["CourseId"]);
                        course.CourseName = item["CourseName"].ToString();
                        course.CourseCode = item["CourseCode"].ToString();
                        course.ClassId = Convert.ToInt32(item["ClassId"]);
                        course.ClassName = item["ClassName"].ToString();
                        course.CreatedById = item["CreatedById"].ToString();
                        course.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                        course.ModifiedById = item["ModifiedById"].ToString();
                        // course.ModifiedDate =string.IsNullOrEmpty(item["ModifiedDate"].ToString())? null: Convert.ToDateTime(item["ModifiedDate"]);
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return course;



        }

        public int CourseAddChanges(Course courese)
        {
            var objcourseDao = new CourseDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objcourseDao.InsertUpdateCourse(courese);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

        public int DeleteCourse(Course c)
        {
            var objcourseDao = new CourseDAO(new SqlDatabase());
            int ReturnValue = 0;
            try
            {
                ReturnValue = objcourseDao.DeleteCourse(c);


            }
            catch (Exception)
            {

                throw;
            }


            return ReturnValue;
        }


    }
}
