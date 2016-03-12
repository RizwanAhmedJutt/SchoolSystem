using SMSBusiness.Repository.Abstract;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class TeacherRepositry : ITeacherRepositry
    {
        #region
        public List<Teacher> GetAllTeacher()
        {

            List<Teacher> teacherList= new List<Teacher>();


            return teacherList;
        }

        #endregion
    }
}
