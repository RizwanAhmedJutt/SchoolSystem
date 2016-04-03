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

      public List<TeacherAssignedCourse> GetTeacherAssignedCourse()
        {
            var objAssignTeacherDao = new TeacherAssignedCouresDAO(new SqlDatabase());
            DataTable tblCourse;
            List<TeacherAssignedCourse> objTeacherAssign = new List<TeacherAssignedCourse>();
            try
            {
                tblCourse = objAssignTeacherDao.GetTeacherAssignedCourse();
                if (tblCourse.Rows.Count > 0)
                {
                    foreach (DataRow item in tblCourse.Rows)
                    {
                        TeacherAssignedCourse Assigncourse = new TeacherAssignedCourse();
                        Assigncourse.TeacherAssignedCourseId= Convert.ToInt32(item["TeacherAssignedCourseID"]);
                        Assigncourse.CourseName = item["CourseName"].ToString();
                        Assigncourse.TeacherName = item["TeacherName"].ToString();
                        Assigncourse.ClassName = item["ClassName"].ToString();
                        Assigncourse.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                        Assigncourse.ClassId = Convert.ToInt32(item["ClassId"]);
                        objTeacherAssign.Add(Assigncourse);
                      
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objTeacherAssign;



        }
      public List<TeacherAssignedCourse> GetTeacherAssignedCourseByCourseName(string CourseName)
      {
          var objAssignTeacherDao = new TeacherAssignedCouresDAO(new SqlDatabase());
          DataTable tblCourse;
          List<TeacherAssignedCourse> objTeacherAssign = new List<TeacherAssignedCourse>();
          try
          {
              tblCourse = objAssignTeacherDao.GetTeacherAssignedCourseByCourseName(CourseName);
              if (tblCourse.Rows.Count > 0)
              {
                  foreach (DataRow item in tblCourse.Rows)
                  {
                      TeacherAssignedCourse Assigncourse = new TeacherAssignedCourse();
                      Assigncourse.TeacherAssignedCourseId = Convert.ToInt32(item["TeacherAssignedCourseID"]);
                      Assigncourse.CourseName = item["CourseName"].ToString();
                      Assigncourse.TeacherName = item["TeacherName"].ToString();
                      Assigncourse.ClassName = item["ClassName"].ToString();
                      Assigncourse.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                      Assigncourse.ClassId = Convert.ToInt32(item["ClassId"]);
                      objTeacherAssign.Add(Assigncourse);

                  }
              }


          }
          catch (Exception ex)
          {

              throw ex;
          }
          return objTeacherAssign;



      } 
      public TeacherAssignedCourse GetTeacherAssignedCourseById(int TSssignCId)
      {
          var objAssignCourseDao = new TeacherAssignedCouresDAO(new SqlDatabase());
          DataTable tblCourse;
          TeacherAssignedCourse Assigncourse = new TeacherAssignedCourse();
          try
          {
              tblCourse = objAssignCourseDao.GetTeacherAssignedCourseById(TSssignCId);
              if (tblCourse.Rows.Count > 0)
              {
                  foreach (DataRow item in tblCourse.Rows)
                  {
                      Assigncourse.TeacherAssignedCourseId = Convert.ToInt32(item["TeacherAssignedCourseID"]);
                      Assigncourse.CourseName = item["CourseName"].ToString();
                      Assigncourse.TeacherName = item["TeacherName"].ToString();
                      Assigncourse.ClassId = Convert.ToInt32(item["ClassId"]);
                      Assigncourse.TeacherId = Convert.ToInt32(item["TeacherId"]);
                      Assigncourse.CourseId = Convert.ToInt32(item["CourseId"]);

                  }
              }


          }
          catch (Exception ex)
          {

              throw ex;
          }
          return Assigncourse;



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
