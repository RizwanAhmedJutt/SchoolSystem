using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult StudentGeneralAssessment(int? AcadmicClassId, int? StudentId, DateTime CreateDate)
        {
            
            return View(repoAssessementSubType.GetStudentGeneralAssessment(AcadmicClassId, StudentId, CreateDate).ToList());
        }
        public ActionResult StudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            return View(repoAssessementSubType.GetStudentAcadmicAssessment(AcadmicClassId, StudentId, CourseId, CreateDate).ToList());
        }
	}
}