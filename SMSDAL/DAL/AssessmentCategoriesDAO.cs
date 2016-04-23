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
   public class AssessmentCategoriesDAO
    {
        private readonly IDatabase gObjDatabase;
        public AssessmentCategoriesDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetALLAssessmentCategories()
        {
            DataTable dtAssessmentDetails;
            try
            {
                var query = "Select * from AssessmentCategories";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query))
                {

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
    
        public int InsertUpdateAssessmentCategories(AssessmentCategories dAssessmentCategory)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Report_AssessmentCategoryInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentCategoryId", DbType.Int32, dAssessmentCategory.AssessmentCategoryId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentName", DbType.String, dAssessmentCategory.AssessmentName);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, dAssessmentCategory.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, dAssessmentCategory.CreateDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(dAssessmentCategory.ModifiedById) ? (object)dAssessmentCategory.ModifiedById : dAssessmentCategory.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, dAssessmentCategory.ModifiedDate == null ? (object)DBNull.Value : dAssessmentCategory.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@AssessmentCategorynewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (dAssessmentCategory.AssessmentCategoryId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@AssessmentCategorynewId"].Value);
                        return identity;
                    }
                    else if (dAssessmentCategory.AssessmentCategoryId > 0)
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


        public DataTable GetAssessmentCategoryById(int AssessmentCategoryId)
        {
            DataTable dtAssessmentDetails;
            try
            {
                var query = "Select * from AssessmentCategories Where AssessmentCategoryId=" + AssessmentCategoryId;
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query))
                {

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }
        public DataTable GetAssessmentCategoryByName(string AssessmentName)
        {
            DataTable dtAssessmentDetails;
            try
            {
                var query = "Select * from AssessmentCategories Where AssessmentName='" + AssessmentName + "'";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query))
                {

                    dtAssessmentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAssessmentDetails;
        }



    }
}
