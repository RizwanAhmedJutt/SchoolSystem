using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface ICourse
    {
      List<Course> GetALLCourse();
      List<Course> GetALLCourseByAcadmicClassId(int AcadmicClassId);
      Course GetCourseDetailByCourseId(int CourseId);
      int CourseAddChanges(Course courese);
      int DeleteCourse(Course c);



    }
}
