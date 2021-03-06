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
        int DeleteStudentGeneralAssessment(int AcadmicClassId, int StudentId, DateTime CreateDate);
        int DeleteStudentAcadmicAssessment(int AcadmicClassId, int StudentId, int CourseId, DateTime CreateDate);
        int DeleteTeacherAssessment(int AcadmicClassId, int TeacherId, int CourseId, DateTime CreateDate);
        DailyAssessmentSubType CheckSubAssementExist(int ParentAssessmentId, string SubAssessmentName);
        List<DailyAssessmentSubType> GetStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, DateTime StartDate,DateTime EndDate);
        List<DailyAssessmentSubType> GetStudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId,DateTime StartDate,DateTime EndDate);
        List<DailyAssessmentType> GetTeacherAcadmicAssessments(int? AcadmicClassId, int? TeacherId, int? CourseId, DateTime StartDate,DateTime EndDate);
        List<DailyAssessmentSubType> GetStudentSingleGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate);
        List<DailyAssessmentType> GetStudentSingleAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate);
        List<DailyAssessmentType> GetTeacherSingleAcadmicAssessment(int? AcadmicClassId, int? TeacherId, int? CourseId, string CreateDate);
        DateTime GetLastReportCreatedDate(int? AcadmicClassId, int? StudentId);

    }
}
