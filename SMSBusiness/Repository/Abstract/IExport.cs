using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IExport
    {
      List<StudentDetail> GetStudentReport(DateTime startDate, DateTime endDate, int AcadmicClassId);
      List<Teacher> GetTeacherReport();
      List<TeacherAssignClass> GetALLTeachersAssignedClass();
      List<Course> GetALLCourse();
      List<TeacherAssignedCourse> GetALLTeacherAssignCourse();
      List<StudentAssignedCourse> GetALLStudentAssignCourse();
      List<StudentBasicExpenditure> GetStudentBasicExpense(DateTime startDate, DateTime endDate, int AcadmicClassId);
      List<StudentExpenditure> GetStudentRegularExpense(DateTime startDate, DateTime endDate, int AcadmicClassId);
    }
}
