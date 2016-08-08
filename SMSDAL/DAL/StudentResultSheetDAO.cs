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
   public class StudentResultSheetDAO
    {
       private readonly IDatabase gObjDatabase;
       public StudentResultSheetDAO(IDatabase database)
       {
           gObjDatabase = database;
       }
       public DataTable GetStudentResultSheets()
       {
           DataTable dtStudentDetails;
           try
           {
               var query = "Select * From StudentResultSheet";
               using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query))
               {
                   dtStudentDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtStudentDetails;
       }
       public int InsertUpdateStudentResultSheet(StudentResultSheet srSheet)
       {
           try
           {
               using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Result_InsertUpdateStudentResultSheet"))
               {
                   gObjDatabase.AddInParameter(objDbCommand, "@StudentResultId", DbType.Int32, srSheet.StudentResultId);
                   gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, srSheet.AcadmicClassId);
                   gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, srSheet.StudentId);
                   gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, srSheet.CourseId);
                   gObjDatabase.AddInParameter(objDbCommand, "@ClassAssessmentPercentage", DbType.Decimal, srSheet.ClassAssessmentPercentage);
                   gObjDatabase.AddInParameter(objDbCommand, "@PaperPercentage", DbType.Decimal, srSheet.PaperPercentage);
                   gObjDatabase.AddInParameter(objDbCommand, "@Grade", DbType.String, srSheet.Grade);
                   gObjDatabase.AddInParameter(objDbCommand, "@Remarks", DbType.String, srSheet.Remarks );
                   gObjDatabase.AddInParameter(objDbCommand, "@PaperTerm", DbType.String, srSheet.PaperTerm);
                   gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, srSheet.CreatedById);
                   gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, srSheet.CreatedDate);
                   gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(srSheet.ModifiedById) ? DBNull.Value : (object)srSheet.ModifiedById);
                   gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, srSheet.ModifiedDate == null ? (object)DBNull.Value : srSheet.ModifiedDate);
                   gObjDatabase.AddOutParameter(objDbCommand, "@StudentResultnewId", DbType.Int32, 4);
                   SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                   returnParameter.Direction = ParameterDirection.ReturnValue;
                   objDbCommand.Parameters.Add(returnParameter);
                   gObjDatabase.ExecuteNonQuery(objDbCommand);
                   if (srSheet.StudentResultId == 0)
                   {
                       int identity = Convert.ToInt32(objDbCommand.Parameters["@StudentResultnewId"].Value);
                       return identity;
                   }
                   else 
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

           return 0;  // show Error in inserting or Updating Record
       }
       public DataTable GetStudentResultSheetById(int Id)
       {
           DataTable dtStudentDetails;
           try
           {
               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Result_GetStudentResultSheetById"))
               {
                   gObjDatabase.AddInParameter(objCommand, "@StudentResultSheetId", DbType.Int32, Id);
                   dtStudentDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtStudentDetails;
       }

       public DataTable GetGeneralStudentResultSheet(int? AcadmicClassId,int? StudentId)
       {
           DataTable dtStudentDetails;
           try
           {
               using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Result_GetGeneralResult"))
               {
                   gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                   gObjDatabase.AddInParameter(objCommand, "@StudentId ", DbType.Int32, StudentId);
                   dtStudentDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtStudentDetails;
       } 
    }
}
