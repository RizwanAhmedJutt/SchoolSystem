using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
   public interface IStudentAttendance
    {
       List<StudentAttendance> GetStudentAttendanceSheet();

       int AddChangesStudentAttendance(StudentAttendance sAttendance);
       StudentAttendance GetStudentAttendanceById(int Id);
    }
}
