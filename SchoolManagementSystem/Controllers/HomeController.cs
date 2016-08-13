using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IdentitySample.Controllers
{
    [Authorize (Roles="Admin")]
    public class HomeController : Controller
    {
        IStudent studentRepo = new StudentBLL();
        IStudentBasicExpenditure stdBasicExpense = new StudentBasicExpenditureBLL();
        IStudentRegularExpenditure stdRegularExpense = new StudentRegularExpenditureBLL();
        public ActionResult Index()
        {
            DefaultPageHelper dph = new DefaultPageHelper();
            dph.TotalStudent =studentRepo.GetAllStudents().ToList().Where(x=>x.IsActive==true).Select(x=>x.StudentId).Count();
            dph.TotalBasicExpense = stdBasicExpense.GetStudentBasicExpenseTotal();
            dph.TotalRegularExpense = stdRegularExpense.GetStudentRegularExpenseTotal();
            dph.TotalFee = dph.TotalBasicExpense + dph.TotalRegularExpense;
            return View(dph);
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }
    }
}
