using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL.DAL
{
  public  class StudentBasicExpenditureDAO
    {

       private readonly IDatabase gObjDatabase;
       public StudentBasicExpenditureDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

       public DataTable GetStudentBasicExpenditure(int? StudentId, int? AcadmicClassId)
       {
           DataTable dtFeeDetails;
           try
           {
               
               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Fee_GetStudentBasicExpenditure"))
               {
                   gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId==null?(object)DBNull.Value:StudentId);
                   gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId==null?(object)DBNull.Value:AcadmicClassId);

                   dtFeeDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtFeeDetails;
       }
       public DataTable GetBasicExpenditureById(int FeeId)
       {
           DataTable dtFeeDetails;
           try
           {

               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Fee_GetBasicExpenditureById"))
               {
                   gObjDatabase.AddInParameter(objCommand, "@FeeId", DbType.Int32, FeeId);
                   dtFeeDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtFeeDetails;
       }
       public int InsertUpdateStudentBasicExpense(StudentBasicExpenditure expenditure)
       {
           try
           {
               using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Fee_InsertUpdateStudentBasicExpenditure"))
               {
                   gObjDatabase.AddInParameter(objDbCommand, "@FeesId", DbType.Int32, expenditure.FeeId);
                   gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32,expenditure.StudentId );
                   gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32,expenditure.AcadmicClassId);
                   gObjDatabase.AddInParameter(objDbCommand, "@FeeMonth", DbType.String,expenditure.FeeMonth );
                   gObjDatabase.AddInParameter(objDbCommand, "@ClassName", DbType.String,expenditure.ClassName );
                   gObjDatabase.AddInParameter(objDbCommand, "@AdmissionFee", DbType.Int32,expenditure.AdmissionFee );
                   gObjDatabase.AddInParameter(objDbCommand, "@SecurityFee", DbType.Int32,expenditure.SecurityFee );
                   gObjDatabase.AddInParameter(objDbCommand, "@ExaminationFee",DbType.Int32,expenditure.ExaminationFee);
                   gObjDatabase.AddInParameter(objDbCommand, "@RegistrationFee",DbType.Int32,expenditure.RegistrationFee);
                   gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime,expenditure.CreateDate);
                   gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String,expenditure.CreateById );
                   gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, expenditure.ModifiedById == null ? DBNull.Value : (object)expenditure.ModifiedById);
                   gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, expenditure.ModifiedDate == null ? DBNull.Value : (object)expenditure.ModifiedDate);
                   gObjDatabase.AddOutParameter(objDbCommand,"@FeesnewId", DbType.Int32, 4);
                   SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                   returnParameter.Direction = ParameterDirection.ReturnValue;
                   objDbCommand.Parameters.Add(returnParameter);
                   gObjDatabase.ExecuteNonQuery(objDbCommand);



                   if (expenditure.FeeId == 0)
                   {


                       int getAssignId = Convert.ToInt32(objDbCommand.Parameters["@FeesnewId"].Value);
                       return getAssignId;
                   }
                   else if (expenditure.FeeId > 0)
                   {
                       var UpdateValue = returnParameter.Value;
                       return (int)UpdateValue;
                   }

               }
           }
           catch
           {
               throw;
           }



           return 0;// show Error in inserting or Updating Record
       }
       public DataTable GetStudentBasicExpenseTotal()
       {
           DataTable dtFeeDetails;
           try
           {

               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Fee_GetGrandTotalBasicExpense"))
               {
                   dtFeeDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtFeeDetails;
       }

    }
}
