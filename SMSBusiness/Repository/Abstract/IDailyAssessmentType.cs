﻿using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IDailyAssessmentType
    {
      List<DailyAssessmentType> GetAllAssessmentType();
      DailyAssessmentType GetDailyAssessmentById(int AssessmentTypeId);
      int AddChangeDailyAssessmentType(DailyAssessmentType dAssessmentsubType);
      DailyAssessmentType GetDailyAssessmentTypeByName(string AssessmentName, int AssessmentCategoryId);
      List<DailyAssessmentType> GetALLAssignedParentAssessments();
      List<DailyAssessmentType> GetALLAssignedParentAcadmicAssessments();
      List<DailyAssessmentType> GetALLAssignedParentTeacherAcadmicAssessments();

    }
}
