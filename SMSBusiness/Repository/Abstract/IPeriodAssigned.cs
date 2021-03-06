﻿using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public interface IPeriodAssigned
    {

        List<PeriodAssigned> GetALLAssignedPeriods();
        PeriodAssigned GetAssignedPeriodById(int PeriodAssignedId);
        int AddChangesPeriods(PeriodAssigned periodAssigned);
        List<PeriodAssigned> PeroidAssignedByAcadmicClass(string AcadmicClassName);
        List<PeriodAssigned> PeroidAssignedByCourse(string CourseName);
        PeriodAssigned CheckAlreadyPeriodAssigned(int PeriodNumber, int AcadmicClassId, int CourseId);
    }
}
