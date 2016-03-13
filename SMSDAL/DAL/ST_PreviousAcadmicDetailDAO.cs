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
    public class ST_PreviousAcadmicDetailDAO
    {

        private readonly IDatabase gObjDatabase;
        public ST_PreviousAcadmicDetailDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetStudentPreviousInfoByStudentId(int StudentId)
        {
            DataTable dtPrevioustDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetStudentPreviousRecordByStdId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    dtPrevioustDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtPrevioustDetails;
        }
        public int InsertUpdateStudentPreviousInfo(ST_PreviousAcadmicDetail previousDetial)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_StudentPreviousRecordInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@PAcadmicID", DbType.Int32, previousDetial.PAcadmicId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, previousDetial.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@SchoolName", DbType.String, previousDetial.SchoolName);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.String, previousDetial.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@PreviousExamPassed", DbType.Boolean, previousDetial.PreviousExamPassed);
                    gObjDatabase.AddInParameter(objDbCommand, "@Session", DbType.String, previousDetial.Session);
                    gObjDatabase.AddInParameter(objDbCommand, "@MarksObtained", DbType.Decimal, previousDetial.MarksObtained);
                    gObjDatabase.AddInParameter(objDbCommand, "@TotalMark", DbType.Decimal, previousDetial.TotalMark);
                    gObjDatabase.AddInParameter(objDbCommand, "@Grade", DbType.String, previousDetial.Grade);
                    gObjDatabase.AddInParameter(objDbCommand, "@MediumOfInstruction", DbType.String, previousDetial.MediumOfInstruction);
                    gObjDatabase.AddOutParameter(objDbCommand, "@PAcadmicNewID", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                   
                 
                   
                    if (previousDetial.PAcadmicId == 0)
                    {
                        // identity will be return studentid 
                        int getStudentId = Convert.ToInt32(objDbCommand.Parameters["@PAcadmicNewID"].Value);
                        return getStudentId;
                    }
                    else if (previousDetial.PAcadmicId > 0)
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




    }
}
