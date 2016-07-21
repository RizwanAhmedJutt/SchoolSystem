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
    public class StudentResultWorkAndSkillDAO
    {
        private readonly IDatabase gObjDatabase;
        public StudentResultWorkAndSkillDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public int InsertUpdateStudentWorkAndStudySkill(StudentResultWorkAndStudySkill srWork)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Result_InsertUpdateWorkAndStudySkill"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@WorkSkillId", DbType.Int32, srWork.WorkSkillId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, srWork.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, srWork.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Description", DbType.String, srWork.Description);
                    gObjDatabase.AddInParameter(objDbCommand, "@Grade", DbType.String,srWork.Grade);
                    gObjDatabase.AddInParameter(objDbCommand, "@TermType", DbType.String, srWork.TermType);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, srWork.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, srWork.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(srWork.ModifiedById) ? DBNull.Value : (object)srWork.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, srWork.ModifiedDate == null ? (object)DBNull.Value : srWork.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@WorkSkillnewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (srWork.WorkSkillId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@WorkSkillnewId"].Value);
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
        public DataTable GetStudentWorkAndStudyById(int Id)
        {
            DataTable dtStudentDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Result_GetWorkSkillById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@WorkSkillId", DbType.Int32, Id);
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
