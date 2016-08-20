using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList.Mvc;
using PagedList;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Rotativa.MVC;    
namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentExpenseController : Controller
    {
        //
        // GET: /StudentExpense/
        IStudentBasicExpenditure repoBasicExpense = new StudentBasicExpenditureBLL();
        IStudentRegularExpenditure repoRegExpense = new StudentRegularExpenditureBLL();
        IExport exportfiles=new ExportFileBLL();
        [HttpGet]
        public ActionResult GetStudentBasicExpenditure(int? StudentId, int? AcadmicClassId, int? page)
        {
            return View(repoBasicExpense.GetStudentBasicExpenditure(StudentId, AcadmicClassId).ToList().ToPagedList(page ?? 1, 10));
        }
        [HttpGet]
        public ActionResult GetStudentBasicExpenditureReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetStudentBasicExpenditureReport(DateTime startDate, DateTime endDate, int AcadmicClassId)
        {
            //ExportStudentBasicExpense(startDate,endDate,  AcadmicClassId);
            List<StudentBasicExpenditure> studentDetail = exportfiles.GetStudentBasicExpense(startDate, endDate, AcadmicClassId);
            return new PartialViewAsPdf("../StudentExpense/StudentBasicExpenseListExport", studentDetail.ToList())
            {

                FileName = "ExpenseDetail.pdf"
            };
            // return RedirectToAction("GetStudentBasicExpenditure");
        }
        [HttpGet]
        public ActionResult StudentBasicExpenseAddChanges(int Id)
        {
            StudentBasicExpenditure expense;

            if (Id == 0)
            {
                expense = new StudentBasicExpenditure();

                return View(expense);
            }
            else
            {

                expense = repoBasicExpense.GetBasicExpenditureById(Id);
                return View(expense);
            }
        }

        [HttpPost]
        public ActionResult StudentBasicExpenseAddChanges(int AcadmicClassId, int StudentId, StudentBasicExpenditure expense)
        {
            var userloggedId = User.Identity.GetUserId();
            expense.AcadmicClassId = AcadmicClassId;
            expense.StudentId = StudentId;
            if (expense.FeeId > 0)
            {
                expense.ModifiedById = userloggedId;

                expense.ModifiedDate = DateTime.Now;

            }
            else
            {
                expense.CreateById = userloggedId;
            }
            int getStatus = repoBasicExpense.StudentBasicExpenseAddChanges(expense);
            return RedirectToAction("GetStudentBasicExpenditure");
        }
        [HttpGet]
        public ActionResult GetRegularExpenditure(int? StudentId, int? AcadmicClassId, int? page)
        {

            return View(repoRegExpense.GetStudentRegularExpenditure(StudentId, AcadmicClassId).ToList().ToPagedList(page ?? 1, 10));
        }
        [HttpGet]
        public ActionResult GetStudentRegularExpenditureReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetStudentRegularExpenditureReport(DateTime startDate, DateTime endDate, int AcadmicClassId)
        {
            List<StudentExpenditure> studentDetail = exportfiles.GetStudentRegularExpense(startDate, endDate, AcadmicClassId);
            return new PartialViewAsPdf("../StudentExpense/StudentRegularExpenseListExport", studentDetail.ToList())
            {

                FileName = "ExpenseDetail.pdf"
            };
            //ExportStudentRegularExpense(startDate, endDate, AcadmicClassId);
            //return RedirectToAction("GetRegularExpenditure");
        }
        [HttpGet]
        public ActionResult StudentRegularExpenseAddChanges(int Id)
        {
            StudentExpenditure expense;
            if (Id == 0)
            {
                expense = new StudentExpenditure();

                return View(expense);
            }
            else
            {

                expense = repoRegExpense.GetStudentRegularExpenditureById(Id);
                return View(expense);
            }
           
        }
        [HttpPost]
        public ActionResult StudentRegularExpenseAddChanges(StudentExpenditure expense,int AcadmicClassId,int StudentId)
        {
            var userloggedId = User.Identity.GetUserId();
            expense.AcadmicClassId = AcadmicClassId;
            expense.StudentId = StudentId;
            if (expense.StdFeeId > 0)
            {
                expense.ModifiedById = userloggedId;

                expense.ModifiedDate = DateTime.Now;

            }
            else
            {
                expense.CreateById = userloggedId;
            }
            int getStatus = repoRegExpense.StudentRegularExpenseAddChanges(expense);
            return RedirectToAction("GetRegularExpenditure");
        }
        public void ExportStudentBasicExpense(DateTime startDate, DateTime endDate, int AcadmicClass)
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Students Basic Expense");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Student Name";
                    ws.Cells[rowNo, 9].Value = "Class Name";
                    ws.Cells[rowNo, 10].Value = "Admission Fee";
                    ws.Cells[rowNo, 11].Value = "Registration Fee";
                    ws.Cells[rowNo, 12].Value = "Examination Fee";
                    ws.Cells[rowNo, 13].Value = "Security Fee";
                    
                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<StudentBasicExpenditure> studentDetail = exportfiles.GetStudentBasicExpense(startDate, endDate, AcadmicClass);
                foreach (var item in studentDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.StudentName;
                    ws.Cells[rowNo, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 9].Value = item.ClassName;
                    ws.Cells[rowNo, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 10].Value = item.AdmissionFee;
                    ws.Cells[rowNo, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 11].Value = item.RegistrationFee;
                    ws.Cells[rowNo, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 12].Value = item.ExaminationFee;
                    ws.Cells[rowNo, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 13].Value = item.SecurityFee;
                  

                }
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=BasicExpense.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }
        }
        public void ExportStudentRegularExpense(DateTime startDate, DateTime endDate, int AcadmicClass)
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Students Regular Expense");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Student Name";
                    ws.Cells[rowNo, 9].Value = "Class Name";
                    ws.Cells[rowNo, 10].Value = "Uniform";
                    ws.Cells[rowNo, 11].Value = "Books";
                    ws.Cells[rowNo, 12].Value = "Note Books";
                    ws.Cells[rowNo, 13].Value = "Stationary";
                    ws.Cells[rowNo, 14].Value = "Transport Fee";
                    ws.Cells[rowNo, 15].Value = "Tution Fee";
                    ws.Cells[rowNo, 16].Value = "Others";

                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<StudentExpenditure> studentDetail = exportfiles.GetStudentRegularExpense(startDate, endDate, AcadmicClass);
                foreach (var item in studentDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.StudentName;
                    ws.Cells[rowNo, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 9].Value = item.ClassName;
                    ws.Cells[rowNo, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 10].Value = item.Uniform;
                    ws.Cells[rowNo, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 11].Value = item.Books;
                    ws.Cells[rowNo, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 12].Value = item.NoteBook;
                    ws.Cells[rowNo, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 13].Value = item.Stationary;
                    ws.Cells[rowNo, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 14].Value = item.Transportation;
                    ws.Cells[rowNo, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 15].Value = item.Tuition;
                    ws.Cells[rowNo, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 16].Value = item.Other;
                    ws.Cells[rowNo, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                }
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=RegularExpense.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }
        }
    }
}