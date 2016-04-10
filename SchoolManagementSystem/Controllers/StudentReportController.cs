using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
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
            return View();
        }


	}
}