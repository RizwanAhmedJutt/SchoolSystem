﻿using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IStudentRegularExpenditure
    {

        List<StudentExpenditure> GetStudentRegularExpenditure(int? StudentId, int? AcadmicClassId);
        StudentExpenditure GetStudentRegularExpenditureById(int StudentFeeId);
        int StudentRegularExpenseAddChanges(StudentExpenditure expense);
        long GetStudentRegularExpenseTotal();
    }
}
