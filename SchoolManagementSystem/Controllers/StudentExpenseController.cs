﻿using SMSBusiness.Repository.Abstract;
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
namespace SchoolManagementSystem.Controllers
{
     [Authorize(Roles = "Admin")]
    public class StudentExpenseController : Controller
    {
        //
        // GET: /StudentExpense/
        IStudentBasicExpenditure repoBasicExpense = new StudentBasicExpenditureBLL();
        [HttpGet]
        public ActionResult GetStudentBasicExpenditure(int? StudentId, int? AcadmicClassId,int?page)
        {
            return View(repoBasicExpense.GetStudentBasicExpenditure(StudentId,AcadmicClassId).ToList().ToPagedList(page ?? 1, 10));
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
        public ActionResult StudentBasicExpenseAddChanges(int AcadmicClassId,int StudentId,StudentBasicExpenditure expense)
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
           // int getStatus = repoBasicExpense.StudentBasicExpenseAddChanges(expense);
            return RedirectToAction("GetStudentBasicExpenditure");
        }
	}
}