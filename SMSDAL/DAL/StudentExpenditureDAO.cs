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
  public  class StudentExpenditureDAO
    {

       private readonly IDatabase gObjDatabase;
       public StudentExpenditureDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

       public DataTable GetRegularExpenditure(int? StudentId, int? AcadmicClassId)
       {
           DataTable dtFeeDetails;
           try
           {

               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Fee_GetRegularExpenditure"))
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
       public DataTable GetStudentRegularExpenditureById(int StdFeeId)
       {
           DataTable dtFeeDetails;
           try
           {

               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Fee_GetStudentRegularExpenditureById"))
               {
                   gObjDatabase.AddInParameter(objCommand, "@StudentFeeId", DbType.Int32, StdFeeId);
                   dtFeeDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtFeeDetails;
       }
       public int InsertUpdateStudentRegularExpense(StudentExpenditure expenditure)
       {
           try
           {
               using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Fee_InsertUpdateStudentRegularExpenditure"))
               {
                   gObjDatabase.AddInParameter(objDbCommand, "@StdFeeId", DbType.Int32, expenditure.StdFeeId);
                   gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32,expenditure.StudentId );
                   gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32,expenditure.AcadmicClassId);
                   gObjDatabase.AddInParameter(objDbCommand, "@FeeMonth", DbType.String,expenditure.FeeMonth );
                   gObjDatabase.AddInParameter(objDbCommand, "@ClassName", DbType.String,expenditure.ClassName );
                   gObjDatabase.AddInParameter(objDbCommand, "@TuitionFee", DbType.Int32, expenditure.Tuition);
                   gObjDatabase.AddInParameter(objDbCommand, "@TransportationFee", DbType.Int32, expenditure.Transportation==null?(object)DBNull.Value:expenditure.Transportation);
                   gObjDatabase.AddInParameter(objDbCommand, "@Books", DbType.Int32, expenditure.Books==null?(object)DBNull.Value:expenditure.Books);
                   gObjDatabase.AddInParameter(objDbCommand, "@NoteBooks", DbType.Int32, expenditure.NoteBook==null?(object)DBNull.Value:expenditure.NoteBook);
                   gObjDatabase.AddInParameter(objDbCommand, "@Stationary", DbType.Int32, expenditure.Stationary==null?(object)DBNull.Value:expenditure.Stationary);
                   gObjDatabase.AddInParameter(objDbCommand, "@Uniform", DbType.Int32, expenditure.Uniform==null?(object)DBNull.Value:expenditure.Uniform);
                   gObjDatabase.AddInParameter(objDbCommand, "@Other", DbType.Int32, expenditure.Other==null?(object)DBNull.Value:expenditure.Other);
                   gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime,expenditure.CreateDate);
                   gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String,expenditure.CreateById );
                   gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, expenditure.ModifiedById == null ? DBNull.Value : (object)expenditure.ModifiedById);
                   gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, expenditure.ModifiedDate == null ? DBNull.Value : (object)expenditure.ModifiedDate);
                   gObjDatabase.AddOutParameter(objDbCommand, "@StdFeenewId", DbType.Int32, 4);
                   SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                   returnParameter.Direction = ParameterDirection.ReturnValue;
                   objDbCommand.Parameters.Add(returnParameter);
                   gObjDatabase.ExecuteNonQuery(objDbCommand);



                   if (expenditure.StdFeeId == 0)
                   {


                       int getAssignId = Convert.ToInt32(objDbCommand.Parameters["@StdFeenewId"].Value);
                       return getAssignId;
                   }
                   else if (expenditure.StdFeeId > 0)
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


    }
}
