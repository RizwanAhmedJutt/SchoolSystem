using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
public  interface IStudentAssignCourse
    {
    StudentAssignedCourse GetAssignedCourseByStudentId(int StudentId);
    int InsertUpdateAssignedCourseAddChanges(StudentAssignedCourse stdAssigncourese);


    }
}
