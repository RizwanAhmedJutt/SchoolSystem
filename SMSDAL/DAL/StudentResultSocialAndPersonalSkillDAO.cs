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
    public class StudentResultSocialAndPersonalSkillDAO
    {

        private readonly IDatabase gObjDatabase;
        public StudentResultSocialAndPersonalSkillDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public int InsertUpdateStudentSocialAndPersonalSkill(StudentResultSocialAndPersonalSkill srPersonal)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Result_InsertUpdateSocialAndPersonalSkill"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@WorkSkillId", DbType.Int32, srPersonal.SocialSkillId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, srPersonal.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, srPersonal.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@SocialDescriptionId", DbType.Int32, srPersonal.SocialDescriptionId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Grade", DbType.String, srPersonal.Grad);
                    gObjDatabase.AddInParameter(objDbCommand, "@TermType", DbType.String, srPersonal.TermType);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, srPersonal.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, srPersonal.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, string.IsNullOrEmpty(srPersonal.ModifiedById) ? DBNull.Value : (object)srPersonal.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, srPersonal.ModifiedDate == null ? (object)DBNull.Value : srPersonal.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@SocialSkillnewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (srPersonal.SocialSkillId == 0)
                    {
                        int identity = Convert.ToInt32(objDbCommand.Parameters["@SocialSkillnewId"].Value);
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
        public DataTable GetStudentSocialAndPersonalSkillById(int Id)
        {
            DataTable dtStudentDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Result_GetStudentSocialSkillById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@SocialSkillId", DbType.Int32, Id);
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
