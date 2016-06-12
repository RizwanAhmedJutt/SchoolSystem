using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
   public interface ITeacherLesson
    {

        int AddChangesLessonPlan(TeacherLessonPlan LessonPlan);
        List<TeacherLessonPlan> GetTeacherLessons(int? AcadmicClassId, int? TeacherId, int? CourseId);
    }
}
