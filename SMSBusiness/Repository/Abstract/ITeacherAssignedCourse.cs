using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface ITeacherAssignedCourse
    {
      TeacherAssignedCourse GetAssignedCourseByTeacherId(int TeacherId);
      int InsertUpdateAssignedCourseAddChanges(TeacherAssignedCourse teacherAssigncourese);


    }
}
