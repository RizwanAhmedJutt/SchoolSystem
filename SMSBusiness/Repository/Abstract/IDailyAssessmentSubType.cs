﻿using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IDailyAssessmentSubType
    {
        List<DailyAssessmentSubType> GetAllAssessmentSubType();
        List<DailyAssessmentSubType> GetALLAcadmicAssessmentSubType();
        List<DailyAssessmentSubType> GetALLTeacherGeneralAssessmentSubType();
        DailyAssessmentSubType GetDailyAssessmentSubTypeById(int AssessmentSubTypeId);
        int AddChangesAssessmentSubType(DailyAssessmentSubType dAssessmentsubType);
        DailyAssessmentSubType CheckSubAssementExist(int ParentAssessmentId, string SubAssessmentName);
        List<DailyAssessmentSubType> GetStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate);
        List<DailyAssessmentSubType> GetStudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate);
        List<DailyAssessmentSubType> GetTeacherGeneralAssessments(int? AcadmicClassId, int? TeacherId, int? CourseId, string CreateDate);
        List<DailyAssessmentSubType> GetStudentSingleGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate);
        List<DailyAssessmentType> GetStudentSingleAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate);
        List<DailyAssessmentSubType> GetTeacherSingleGeneralAssessment(int? AcadmicClassId, int? TeacherId, int? CourseId, string CreateDate);

    }
}
