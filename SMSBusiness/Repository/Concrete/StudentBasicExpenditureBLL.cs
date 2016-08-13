using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class StudentBasicExpenditureBLL : IStudentBasicExpenditure
    {

     public List<StudentBasicExpenditure> GetStudentBasicExpenditure(int? StudentId, int? AcadmicClassId)
     {
         var objStudentBasicExpensDao = new StudentBasicExpenditureDAO(new SqlDatabase());
         DataTable dtexpense = objStudentBasicExpensDao.GetStudentBasicExpenditure(StudentId, AcadmicClassId);
         List<StudentBasicExpenditure> expense = new List<StudentBasicExpenditure>();
         try
         {
             if (dtexpense.Rows.Count > 0)
             {
                 foreach (DataRow item in dtexpense.Rows)
                 {
                     StudentBasicExpenditure sexpense = new StudentBasicExpenditure();
                     sexpense.FeeId = Convert.ToInt32(item["FeesId"]);
                     sexpense.StudentName = item["StudentName"].ToString();
                     sexpense.ClassName = item["ClassName"].ToString();
                     sexpense.AdmissionFee = int.Parse(item["AdmissionFee"].ToString());
                     sexpense.SecurityFee = int.Parse(item["SecurityFee"].ToString());
                     sexpense.ExaminationFee = int.Parse(item["ExaminationFee"].ToString());
                     sexpense.RegistrationFee = int.Parse(item["RegistrationFee"].ToString());
                     sexpense.CreateDate = Convert.ToDateTime(item["PaidDate"]);

                     expense.Add(sexpense);
                 }


             }
         }
         catch (Exception)
         {

             throw;
         }

         return expense;
     }

     public StudentBasicExpenditure GetBasicExpenditureById(int FeeId)
     {
         var objStudentBasicExpensDao = new StudentBasicExpenditureDAO(new SqlDatabase());
         DataTable dtexpense = objStudentBasicExpensDao.GetBasicExpenditureById(FeeId);
         StudentBasicExpenditure sbExpense = new StudentBasicExpenditure();
         foreach (DataRow item in dtexpense.Rows)
         {
             sbExpense.FeeId = Convert.ToInt32(item["FeesId"]);
             sbExpense.StudentId = Convert.ToInt32(item["StudentId"]);
             sbExpense.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"]);
             sbExpense.FeeMonth = item["FeeMonth"].ToString();
             sbExpense.ClassName = item["ClassName"].ToString();
             sbExpense.AdmissionFee = Convert.ToInt32(item["AdmissionFee"]);
             sbExpense.SecurityFee = Convert.ToInt32(item["SecurityFee"]);
             sbExpense.ExaminationFee = Convert.ToInt32(item["ExaminationFee"]);
             sbExpense.RegistrationFee = Convert.ToInt32(item["RegistrationFee"]);
             sbExpense.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
             sbExpense.CreateById = item["CreatedById"].ToString();
             sbExpense.ModifiedById = item["ModifiedById"].ToString();
             sbExpense.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);


         }
         return sbExpense;   
     }
     public int StudentBasicExpenseAddChanges(StudentBasicExpenditure expense)
     {
         var objExpenseDao = new StudentBasicExpenditureDAO(new SqlDatabase());
         int ReturnValue = 0;  // Value will be 99 in case of Update
         try
         {
             ReturnValue = objExpenseDao.InsertUpdateStudentBasicExpense(expense);
         }
         catch (Exception)
         {

             throw;
         }

         return ReturnValue;
     }

     public long GetStudentBasicExpenseTotal()
     {
         var objStudentBasicExpensDao = new StudentBasicExpenditureDAO(new SqlDatabase());
         DataTable dtexpense = objStudentBasicExpensDao.GetStudentBasicExpenseTotal();
         long TotalExpense=0;
         foreach (DataRow item in dtexpense.Rows)
         {

             TotalExpense = Convert.ToInt64(item["totalBasicExpense"]);

         }
         return TotalExpense;
     }
    }
}
