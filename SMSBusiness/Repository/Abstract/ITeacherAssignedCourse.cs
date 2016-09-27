using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface ITeacherAssignedCourse
    {
      List<TeacherAssignedCourse> GetTeacherAssignedCourse();
      List<TeacherAssignedCourse> GetTeacherAssignedCourseByCourseName(string CourseName);
      TeacherAssignedCourse GetTeacherAssignedCourseById(int TSssignCId);
      int InsertUpdateAssignedCourseAddChanges(TeacherAssignedCourse teacherAssigncourese);
      List<Course> GetTeacherAssignedCourseByAcadmicClass(int TeacherId, int AcadmicClassId);

    }
}
