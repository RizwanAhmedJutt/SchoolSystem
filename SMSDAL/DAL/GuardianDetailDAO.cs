﻿using SMSDataContract.Accounts;
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
    public class GuardianDetailDAO
    {
        private readonly IDatabase gObjDatabase;
        public GuardianDetailDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public DataTable GetGuardianInfoByStudentId(int GuardianId)
        {
            DataTable dtGuardianDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_std_GetGuardianDetailByGuardianId"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@GuardianId", DbType.Int32, GuardianId);
                    dtGuardianDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtGuardianDetails;
        }
        public int InsertUpdateGuardian(GuardianDetail guardianDetail)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_std_GuardianInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentGuardianId", DbType.Int32, guardianDetail.StudentGuardianId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StudentId", DbType.Int32, guardianDetail.StudentId);
                    gObjDatabase.AddInParameter(objDbCommand, "@FathertName", DbType.String, guardianDetail.FatherName);
                    gObjDatabase.AddInParameter(objDbCommand, "@MotherName", DbType.String, guardianDetail.MotherName);
                    gObjDatabase.AddInParameter(objDbCommand, "@GuardianName", DbType.String, guardianDetail.GuardianName);
                    gObjDatabase.AddInParameter(objDbCommand, "@MotherTongue", DbType.String, guardianDetail.MotherName);
                    gObjDatabase.AddInParameter(objDbCommand, "@OccupationOfGuardian", DbType.String, guardianDetail.OccupationOfGuardian);
                    gObjDatabase.AddInParameter(objDbCommand, "@RelationWithGuardian", DbType.String, guardianDetail.RelationWithGuardian);
                    gObjDatabase.AddInParameter(objDbCommand, "@GuardianMonthlyIncome", DbType.Int32, guardianDetail.GuardianMontlyIncome);
                    gObjDatabase.AddInParameter(objDbCommand, "@GuardianQualification", DbType.String, guardianDetail.GuardianQualification);

                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter parm = new SqlParameter("@GuardianNewId", SqlDbType.Int);
                    parm.Size = 4;
                    parm.Direction = ParameterDirection.Output; // This is important!
                    objDbCommand.Parameters.Add(parm);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    if (guardianDetail.StudentGuardianId == 0)
                    {
                        var identity = parm.Value;
                        return (int)identity;
                    }
                    else if (guardianDetail.StudentGuardianId > 0)
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
