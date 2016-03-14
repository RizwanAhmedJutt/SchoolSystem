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
  public  class TeacherAssignCourseBLL:ITeacherAssignedCourse
    {

      public TeacherAssignedCourse GetAssignedCourseByTeacherId(int TeacherId)
        {
            var objAssignCourseDao = new TeacherAssignedCouresDAO(new SqlDatabase());
            DataTable tblCourse;
            TeacherAssignedCourse Assigncourse = new TeacherAssignedCourse();
            try
            {
                tblCourse = objAssignCourseDao.GetAssignedCourseByTeacherId(TeacherId);
                if (tblCourse.Rows.Count > 0)
                {
                    foreach (DataRow item in tblCourse.Rows)
                    {
                        Assigncourse.TeacherAssignedCourseId= Convert.ToInt32(item["TeacherAssignedCourseID"]);
                        Assigncourse.CourseName = item["CourseName"].ToString();
                        Assigncourse.TeacherName = item["TeacherName"].ToString();
                        
                      
                    }
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return course;



        }

      public int InsertUpdateAssignedCourseAddChanges(TeacherAssignedCourse teacherAssigncourese)
        {
            var objcourseDao = new TeacherAssignedCouresDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objcourseDao.InsertUpdateAssignedCourse(teacherAssigncourese);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }



    }
}
