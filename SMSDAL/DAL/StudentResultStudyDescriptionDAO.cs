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
    public class StudentResultStudyDescriptionDAO
    {
        private readonly IDatabase gObjDatabase;
        public StudentResultStudyDescriptionDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetALLStudyDescriptions()
        {
            DataTable dtDescriptionDetails;
            try
            {
                var getQuery = "Select * from StudentResultStudyDescription";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getQuery))
                {

                    dtDescriptionDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtDescriptionDetails;
        }
        public int AddChangesStudyDescriptions(StudentResultStudyDescription studyDescription)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Result_InsertUpdateStudyDescription"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@StudyDescriptionId", DbType.Int32, studyDescription.StudyDescriptionId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Description", DbType.String, studyDescription.Description);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, studyDescription.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, studyDescription.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, studyDescription.ModifiedDate==null?null:studyDescription.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(studyDescription.ModifiedById) ? null : studyDescription.ModifiedById);
                    gObjDatabase.AddOutParameter(objDbCommand, "@StudyDescriptionnewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (studyDescription.StudyDescriptionId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@StudyDescriptionnewId"].Value);
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

            // return 0;  // show Error in inserting or Updating Record


        }
    }
}
