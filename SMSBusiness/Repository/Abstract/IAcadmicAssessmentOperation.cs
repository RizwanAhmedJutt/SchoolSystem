using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IAcadmicAssessmentOperation
    {

      int AddChangesAcadmicAssessmentOperation(AcadmicAssessmentOperation dAssessmentOperation);
      List<Course> GetStudentAssessmentCourse(int StudentId, int AcadmicClassId, string Month);
      List<AcadmicAssessmentOperation> GetStudentAssessmentByCourses(int StudentId, int AcadmicClassId, string Month, StringBuilder CourseIDs);
      List<DailyAssessmentOperation> GetStudentGeneralAssessmentResult(int StudentId, int AcadmicClassId, string Month);
    }
}
