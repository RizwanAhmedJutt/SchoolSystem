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
    public class StudentAttendanceDAO
    {

        private readonly IDatabase gObjDatabase;
        public StudentAttendanceDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetStudentAttendanceSheets()
        {
            DataTable dtAttendanceDetails;
            try
            {
                var query = "Select * From StudentAttendanceSheet";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query))
                {
                    dtAttendanceDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAttendanceDetails;
        }
        public int InsertUpdateStudentAttendance(StudentAttendance sAttendance)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Result_InsertUpdateStudentAttendance"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentAttendanceId", DbType.Int32, sAttendance.StudentAttendanceId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, sAttendance.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, sAttendance.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@WorkingDays", DbType.String, sAttendance.WorkingDays);
                    gObjDatabase.AddInParameter(objDbCommand, "@Leaves", DbType.Int32, sAttendance.Leaves);
                    gObjDatabase.AddInParameter(objDbCommand, "@Absentes", DbType.Int32, sAttendance.Absents);
                    gObjDatabase.AddInParameter(objDbCommand, "@TotalPercentage", DbType.Decimal, sAttendance.TotalPercentage);
                    gObjDatabase.AddInParameter(objDbCommand, "@PaperTerm", DbType.String, sAttendance.PaperTerm);
                    gObjDatabase.AddOutParameter(objDbCommand, "@AttendancenewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (sAttendance.StudentAttendanceId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@AttendancenewId"].Value);
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
        public DataTable GetStudentAttendanceSheetById(int Id)
        {
            DataTable dtStudentDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Result_GetStudentAttendanceSheetById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@StudentAttendanceId", DbType.Int32, Id);
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
