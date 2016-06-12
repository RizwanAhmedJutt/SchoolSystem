using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface ITeacherAssessmentOperation
    {
        int AddChangesTeacherAssessmentOperation(TeacherAssessmentOperation dAssessmentOperation);

        List<Course> GetTeacherAssessmentCourse(int? TeacherId, int? AcadmicClassId, string Month);
        StringBuilder TeacherAssessmentCourseIDs(int? TeacherId, int? AcadmicClassId, string Month);
        List<TeacherAssessmentOperation> GetTeacherMonthAssessmentResult(int? AcadmicClassId, int? TeacherId, StringBuilder CourseIDs, string Month);
    }
}
