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
    public class StudentRegularExpenditureBLL : IStudentRegularExpenditure
    {

        public List<StudentExpenditure> GetStudentRegularExpenditure(int StudentId, int AcadmicClassId)
        {
            var objStudentExpensDao = new StudentExpenditureDAO(new SqlDatabase());
            DataTable dtexpense = objStudentExpensDao.GetRegularExpenditure(StudentId, AcadmicClassId);
            List<StudentExpenditure> expense = new List<StudentExpenditure>();
            try
            {
                if (dtexpense.Rows.Count > 0)
                {
                    foreach (DataRow item in dtexpense.Rows)
                    {
                        StudentExpenditure sexpense = new StudentExpenditure();
                        sexpense.StdFeeId = Convert.ToInt32(item["StdFeeId"]);
                        sexpense.StudentId = Convert.ToInt32(item["StudentId"]);
                        sexpense.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"]);
                        sexpense.StudentName = item["StudentName"].ToString();
                        sexpense.FeeMonth = item["FeeMonth"].ToString();
                        sexpense.ClassName = item["ClassName"].ToString();
                        sexpense.Tuition = item.IsNull("TuitionFee") ? 0 : int.Parse(item["TuitionFee"].ToString());
                        sexpense.Transportation = item.IsNull("TransportationFee") ? 0 : int.Parse(item["TransportationFee"].ToString());
                        sexpense.Books = item.IsNull("Books") ? 0 : int.Parse(item["Books"].ToString());
                        sexpense.Stationary = item.IsNull("Stationary") ? 0 : int.Parse(item["Stationary"].ToString());
                        sexpense.Uniform = item.IsNull("Uniform") ? 0 : Convert.ToInt32(item["Uniform"]);
                        sexpense.Other = item.IsNull("Other") ? 0 : Convert.ToInt32(item["Other"]);
                        sexpense.CreateDate = Convert.ToDateTime(item["CreatedDate"]);

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

        public StudentExpenditure GetStudentRegularExpenditureById(int StudentFeeId)
        {
            var objStudentRegularExpensDao = new StudentExpenditureDAO(new SqlDatabase());
            DataTable dtexpense = objStudentRegularExpensDao.GetStudentRegularExpenditureById(StudentFeeId);
            StudentExpenditure sbExpense = new StudentExpenditure();
            foreach (DataRow item in dtexpense.Rows)
            {
                sbExpense.StdFeeId = Convert.ToInt32(item["StdFeeId"]);
                sbExpense.StudentId = Convert.ToInt32(item["StudentId"]);
                sbExpense.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"]);
                sbExpense.FeeMonth = item["FeeMonth"].ToString();
                sbExpense.ClassName = item["ClassName"].ToString();
                sbExpense.Tuition = item.IsNull("TuitionFee") ? 0 : Convert.ToInt32(item["TuitionFee"]);
                sbExpense.Transportation = item.IsNull("TransportationFee") ? 0 : Convert.ToInt32(item["TransportationFee"]);
                sbExpense.Books = item.IsNull("Books") ? 0 : Convert.ToInt32(item["Books"]);
                sbExpense.Stationary = item.IsNull("Stationary") ? 0 : Convert.ToInt32(item["Stationary"]);
                sbExpense.Uniform = item.IsNull("Uniform") ? 0 : Convert.ToInt32(item["Uniform"]);
                sbExpense.Other = item.IsNull("Other") ? 0 : Convert.ToInt32(item["Other"]);
                sbExpense.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                sbExpense.CreateById = item["CreatedById"].ToString();
                sbExpense.ModifiedById = item.IsNull("ModifiedById") ? null : item["ModifiedById"].ToString();
                sbExpense.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);


            }
            return sbExpense;
        }
        public int StudentRegularExpenseAddChanges(StudentExpenditure expense)
        {
            var objExpenseDao = new StudentExpenditureDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objExpenseDao.InsertUpdateStudentRegularExpense(expense);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }
    }
}
