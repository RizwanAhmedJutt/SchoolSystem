using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IStudentBasicExpenditure
    {

        List<StudentBasicExpenditure> GetStudentBasicExpenditure(int? StudentId, int? AcadmicClassId);
        StudentBasicExpenditure GetBasicExpenditureById(int FeeId);
        int StudentBasicExpenseAddChanges(StudentBasicExpenditure expense);
       long GetStudentBasicExpenseTotal();
    }
}
