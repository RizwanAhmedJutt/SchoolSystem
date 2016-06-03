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
 public   class PeriodAssignedDAO
    {


        private readonly IDatabase gObjDatabase;
        public PeriodAssignedDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetALLAssignedPeriods()
        {
            DataTable dtPeriodDetails;
            try
            {
               
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Period_GetALLAssignedPeriods"))
                {

                    dtPeriodDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtPeriodDetails;
        } 

        public DataTable PeriodAssignedByAcadmicClass(string AcadmicClassName)
        {

            DataTable dtPeriodDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Period_PeroidAssignedByAcadmicClass"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassName", DbType.String, AcadmicClassName);
                    dtPeriodDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtPeriodDetails;
        }
        public DataTable PeriodAssignedByCourse(string  CourseName)
        {

            DataTable dtPeriodDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Period_PeriodAssignedByCourse"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@CourseName", DbType.String, CourseName);
                    dtPeriodDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtPeriodDetails;
        }

        public int AddChangesPeriods(PeriodAssigned PAssigned)
        {
            try
            {

                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Period_InsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@PeriodAssignedId", DbType.Int32, PAssigned.PeriodAssignedId);
                    gObjDatabase.AddInParameter(objDbCommand, "@PeriodId", DbType.Int32, PAssigned.PeriodNumber);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, PAssigned.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@CourseId", DbType.Int32, PAssigned.CourseId);
                    gObjDatabase.AddOutParameter(objDbCommand,"@PeriodAssignednewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (PAssigned.PeriodAssignedId== 0)
                    {
                        int getPeriodId = Convert.ToInt32(objDbCommand.Parameters["@PeriodAssignednewId"].Value);

                        return getPeriodId;
                    }
                    else if (PAssigned.PeriodAssignedId > 0)
                    {
                        var UpdateValue = returnParameter.Value;
                        return (int)UpdateValue;
                    }

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return 0;  // show Error in inserting or Updating Record
        }
        public DataTable GetAssignedPeriodById(int PeriodAssignedId)
        {

            DataTable dtPeriodDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Period_GetAssignedPeriodById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@PeriodAssignedId", DbType.Int32, PeriodAssignedId);
                    dtPeriodDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtPeriodDetails;

        }
       
        public DataTable CheckalreadyPeriodAssigned(int PeriodNumber,int AcadmicClassId,int CourseId)
        {
            DataTable dtPeriodDetails;
            try
            {

                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Period_IsPeriodAssigned"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@PeriodNumber", DbType.Int32, PeriodNumber);
                    gObjDatabase.AddInParameter(objCommand, "@AcadmicClassId", DbType.Int32, AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@CourseId", DbType.Int32, CourseId);
                    dtPeriodDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtPeriodDetails;
        }
    }
}
