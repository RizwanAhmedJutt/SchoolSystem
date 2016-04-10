using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class StudentReportController : Controller
    {
        //
        // GET: /StudentReport/
        public ActionResult GetALLAssessment()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddChangesAssessmentType(int Id)
        {
            
            return View();
        }


	}
}