﻿using SMSBusiness.Repository.Abstract;
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
public    class StudentAssignCourseBLL : IStudentAssignCourse
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
                    Assigncourse.StudentId = Convert.ToInt32(item["StudentId"]);
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

        public StudentAssignedCourse GetAssignedCourseByStudentId(int StudentId)
        {
            var objAssignCourseDao = new StudentAssignCourseDAO(new SqlDatabase());
            DataTable tblCourse;
            StudentAssignedCourse Assigncourse = new StudentAssignedCourse();
            try
            {
                tblCourse = objAssignCourseDao.GetAssignedCourseByStudentId(StudentId);
                if (tblCourse.Rows.Count > 0)
                {
                    foreach (DataRow item in tblCourse.Rows)
                    {
                        Assigncourse.AssignCourseId = Convert.ToInt32(item["CourseAssignId"]);
                        Assigncourse.CourseName = item["CourseName"].ToString();
                        Assigncourse.StudentName = item["StudentName"].ToString();


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
