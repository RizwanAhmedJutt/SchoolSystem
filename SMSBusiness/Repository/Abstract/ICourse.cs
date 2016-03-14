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
      Course GetCourseDetailByCourseId(int CourseId);
      int CourseAddChanges(Course courese);



    }
}
