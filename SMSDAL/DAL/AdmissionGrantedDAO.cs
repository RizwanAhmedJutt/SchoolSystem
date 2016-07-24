using SMSDataContract.Accounts;
using SMSDataContract.Common;
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
 public   class AdmissionGrantedDAO
    {
        private readonly IDatabase gObjDatabase;

        public AdmissionGrantedDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public DataTable GetAdmissionGrantedInfoByStudentId(int StudentId)
        {
            DataTable dtAdmissionDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetAdmissionGrantedDetailByStudentId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@StudentId", DbType.Int32, StudentId);
                    dtAdmissionDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtAdmissionDetails;
        }
        public int InsertUpdateAdmissionGranted(AdmissionGranted admissionGranted)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_AdmissionGrantedInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@AdmissionId", DbType.Int32, admissionGranted.AdmissionId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, admissionGranted.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AssessmentResult", DbType.String,admissionGranted.AssessmentResult);
                    gObjDatabase.AddInParameter(objDbCommand, "@AdmissionGranted", DbType.Boolean,admissionGranted.IsGranted );
                    gObjDatabase.AddInParameter(objDbCommand, "@AdmissionGrantedForClass", DbType.String, admissionGranted.AdmissionGrantedForClass);
                    gObjDatabase.AddInParameter(objDbCommand, "@AdmissionGrantedDate", DbType.DateTime, admissionGranted.AdmissionGrantedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@Remarks", DbType.String, admissionGranted.Remarks);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, admissionGranted.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime,admissionGranted.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, admissionGranted.ModifiedById);
                    
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, admissionGranted.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@AdmissionNewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                   
                   
                    if (admissionGranted.AdmissionId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@AdmissionNewId"].Value);
                        return identity;
                    }
                    else if (admissionGranted.AdmissionId > 0)
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
