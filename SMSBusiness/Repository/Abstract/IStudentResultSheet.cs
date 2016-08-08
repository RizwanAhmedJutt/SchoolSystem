using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IStudentResultSheet
    {
        List<StudentResultSheet> GetStudentResultSheet();
        List<StudentResultSheet> GetGeneralStudentResultSheet(int? AcadmicClassId, int? StudentId);
        int AddChangesStudentResultSheet(StudentResultSheet srSheet);
        StudentResultSheet GetStudentById(int Id);

    }
}
