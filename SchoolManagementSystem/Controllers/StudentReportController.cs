using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class StudentReportController : Controller
    {
        IDailyAssessmentType repoAssessmentType = new DailyAssessmentTypeBLL();
        IDailyAssessmentSubType repoAssessementSubType = new DailyAssessmentSubTypeBLL();
        // GET: /StudentReport/
        public ActionResult GetALLAssessment()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddChangesAssessmentType(int Id)
        {
            DailyAssessmentType dAssessType = new DailyAssessmentType();

            return View(dAssessType);
        }
        [HttpPost]
        public ActionResult AddChangesAssessmentType(DailyAssessmentType dAssesstype)
        {
            return View();
        }

        public ActionResult AddChangesDailyAssessmentReport()
        {
           List<DailyAssessmentType> type = repoAssessmentType.GetAllAssessmentType();
           List<DailyAssessmentSubType> subtype= repoAssessementSubType.GetAllAssessmentSubType();
           dynamic mymodel = new ExpandoObject();
           mymodel.AssessmentType = type;
           mymodel.AssessmentSubType = subtype;
           return View(mymodel);
        }
        public ActionResult AddChangesAssessmentReport()
        {
          
          
            DailyAssessmentHelper myModel = new DailyAssessmentHelper();
            myModel.ParentAssessments = repoAssessmentType.GetAllAssessmentType();
            myModel.ChildAssessments = repoAssessementSubType.GetAllAssessmentSubType();
            return View(myModel);
        }

	}
}