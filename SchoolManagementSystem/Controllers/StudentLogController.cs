using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class StudentLogController : Controller
    {
        IAssessmentCategories repoAssessmentCategory = new AssessmentCategoriesBLL();
        IDailyAssessmentType repoAssessmentType = new DailyAssessmentTypeBLL();
        IDailyAssessmentSubType repoAssessementSubType = new DailyAssessmentSubTypeBLL();
        IDailyAssessmentOperation repoOperation = new DailyAssessmentOperationBLL();
        IAcadmicAssessmentOperation repAcOperation = new AcadmicAssessmentOperationBLL();
        ITeacherAssessmentOperation repTAOperation = new TeacherAssessmentOperationBLL();
      
        public ActionResult GetStudentReport(int? StudentId, int? AcadmicClassId, string Month)
        {
            StringBuilder courseIDs = new StringBuilder();
            StudentMonthReportHelpers smrh = new StudentMonthReportHelpers();
            if (AcadmicClassId > 0)
            {

                smrh.Courses = repAcOperation.GetStudentAssessmentCourse(StudentId, AcadmicClassId, Month);
                // Get CourseId to fetch only specific courses data.
                courseIDs = repAcOperation.StudentAssessmentCourseIDs(StudentId, AcadmicClassId, Month);
                // confirm course must exist against assessment
                if (!string.IsNullOrEmpty(courseIDs.ToString()))
                {
                    smrh.AcadmicAssessment = repAcOperation.GetStudentAssessmentByCourses(StudentId, AcadmicClassId, Month, courseIDs);
                    smrh.GeneralAssessment = repAcOperation.GetStudentGeneralAssessmentResult(StudentId, AcadmicClassId, Month);
                }
                return View(smrh);
            }
            else
            {
                return View(smrh);
            }
        }
	}
}