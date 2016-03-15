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
    public class TeacherDAO
    {
        private readonly IDatabase gObjDatabase;
        public TeacherDAO(IDatabase database)
        {
            gObjDatabase = database;
        }


        #region Mean Teacher create,Update, Delete and Insert Data Access Layer
        /// <summary>
        /// Get All Teacher
        /// </summary>
        /// <returns></returns>
        public DataTable GetALLTeachers()
        {
            DataTable dtTeachers;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetAllTeachers"))
                {

                    dtTeachers = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTeachers;


        }

        /// <summary>
        /// Get Teacher By Id
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        public DataTable GetTeacherById(int TeacherId)
        {
            DataTable dtStudentDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetTeacherById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@TeacherId", DbType.Int32, TeacherId);
                    dtStudentDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtStudentDetails;
        }

        /// <summary>
        /// Insert, Update Teacher
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int InsertUpdateTeacher(Teacher teacher)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_InsertUpdateTeacher"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, teacher.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String, teacher.FirstName);
                    gObjDatabase.AddInParameter(objDbCommand, "@LastName", DbType.String, teacher.LastName);
                    gObjDatabase.AddInParameter(objDbCommand, "@LastQualification", DbType.String, teacher.LastQualification);
                    gObjDatabase.AddInParameter(objDbCommand, "@CNIC", DbType.String, teacher.CNIC);
                    gObjDatabase.AddInParameter(objDbCommand, "@JoinDate", DbType.DateTime, teacher.JoinDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@LeaveDate", DbType.DateTime, teacher.LeaveDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@RefrenceName", DbType.String, teacher.RefrenceName);
                    gObjDatabase.AddInParameter(objDbCommand, "@RefrenceContact", DbType.String, teacher.RefrenceContact);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, teacher.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, teacher.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, teacher.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, teacher.ModifiedById);
                    gObjDatabase.AddOutParameter(objDbCommand, "@TeachernewId", DbType.Int32, 4);

                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);


                    if (teacher.TeacherId == 0)
                    {
                        var identity = Convert.ToInt32(objDbCommand.Parameters["@TeachernewId"].Value);
                        return (int)identity;
                    }
                    else if (teacher.TeacherId > 0)
                    {
                        if (Convert.ToInt32(returnParameter.Value) > 0)
                            return Convert.ToInt32(returnParameter.Value);
                    }

                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }

            return 0; 
        }

        /// <summary>
        /// Delete Teacher By Id
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int DeleteTeacher(Teacher teacher)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_DeleteTeacherRecord"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, teacher.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, teacher.ModifiedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.Int32, teacher.ModifiedById);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    var returnValues = returnParameter.Value;
                    if ((int)returnValues == 1)
                    {
                        return 1;  // Successfully Deleted/DeActive
                    }

                }
            }
            catch
            {
                throw;
            }

            return 0;  // show Error in inserting or Updating Record

        }


        #endregion



        #region Teacher Address Create, Update, Delete Data access Layer

        /// <summary>
        /// Get All Teacher Address 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTeacherAddress()
        {
            DataTable dtTAddress;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetAllTeacherAddress"))
                {

                    dtTAddress = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }


            return dtTAddress;
        }

        /// <summary>
        /// Get Teacher Address By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetTAddressById(int TAddressId)
        {
            DataTable dtTAddressDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetTeacherAddressById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@TAddressId", DbType.Int32, TAddressId);
                    dtTAddressDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTAddressDetails;
        }

        /// <summary>
        /// Insert Update Teacher Address
        /// </summary>
        /// <param name="tAddress"></param>
        /// <returns></returns>
        public int InsertUpdateTAddress(TeacherAddress tAddress)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_InsertUpdateTeacher"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, tAddress.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@FirstName", DbType.String, tAddress.PresentAddress);
                    gObjDatabase.AddInParameter(objDbCommand, "@LastName", DbType.String, tAddress.PermanentAddress);
                    
                    gObjDatabase.AddOutParameter(objDbCommand, "@TAddressnewId", DbType.Int32, 4);

                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);


                    if (tAddress.TAddressId == 0)
                    {
                        var identity = Convert.ToInt32(objDbCommand.Parameters["@TAddressnewId"].Value);
                        return (int)identity;
                    }
                    else if (tAddress.TAddressId > 0)
                    {
                        if (Convert.ToInt32(returnParameter.Value) > 0)
                            return Convert.ToInt32(returnParameter.Value);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// Delete Teacher Address
        /// </summary>
        /// <param name="tAddress"></param>
        /// <returns></returns>
        public int DeleteTAddress(TeacherAddress tAddress)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_DeleteTeacherAddressRecord"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, tAddress.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@PermanentAddress", DbType.DateTime, tAddress.PermanentAddress);
                    gObjDatabase.AddInParameter(objDbCommand, "@PresentAddress", DbType.Int32, tAddress.PresentAddress);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    var returnValues = returnParameter.Value;
                    if ((int)returnValues == 1)
                    {
                        return 1;  // Successfully Deleted/DeActive
                    }

                }
            }
            catch
            {
                throw;
            }

            return 0;  // show Error in inserting or Updating Record

        }

        #endregion


        #region Teacher Contacts Create, Update, Delete Data access Layer

        /// <summary>
        /// Get All Teacher Contact 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTeacherContact()
        {
            DataTable dtTContacts;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetAllTeacherContacts"))
                {

                    dtTContacts = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }


            return dtTContacts;
        }

        /// <summary>
        /// Get Teacher Contact By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetTContactsById(int TContactsId)
        {
            DataTable dtTContactsDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetTeacherContactById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@TeacherContactId", DbType.Int32, TContactsId);
                    dtTContactsDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTContactsDetails;
        }

        /// <summary>
        /// Insert Update Teacher Contact
        /// </summary>
        /// <param name="tAddress"></param>
        /// <returns></returns>
        public int InsertUpdateTContact(TeacherContact tContact)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_InsertUpdateTeacher"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, tContact.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Contact1", DbType.String, tContact.ContactFrist);
                    gObjDatabase.AddInParameter(objDbCommand, "@Contact2", DbType.String, tContact.ContactSecond);

                    gObjDatabase.AddOutParameter(objDbCommand, "@TeacherContactNewId", DbType.Int32, 4);

                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);


                    if (tContact.TeacherContactId == 0)
                    {
                        var identity = Convert.ToInt32(objDbCommand.Parameters["@TeacherContactNewId"].Value);
                        return (int)identity;
                    }
                    else if (tContact.TeacherContactId > 0)
                    {
                        if (Convert.ToInt32(returnParameter.Value) > 0)
                            return Convert.ToInt32(returnParameter.Value);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// Delete Teacher Contact
        /// </summary>
        /// <param name="tAddress"></param>
        /// <returns></returns>
        public int DeleteTContact(TeacherContact tContact)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_DeleteTeacherContactRecord"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, tContact.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@Contact1", DbType.DateTime, tContact.ContactFrist);
                    gObjDatabase.AddInParameter(objDbCommand, "@Contact2", DbType.Int32, tContact.ContactSecond);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    var returnValues = returnParameter.Value;
                    if ((int)returnValues == 1)
                    {
                        return 1;  // Successfully Deleted/DeActive
                    }

                }
            }
            catch
            {
                throw;
            }

            return 0;  // show Error in inserting or Updating Record

        }

        #endregion


        #region Teacher Profile Create, Update, Delete Data access Layer

        /// <summary>
        /// Get All Teacher Profile
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTeacherProfile()
        {
            DataTable dtTProfile;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetAllTeacherProfile"))
                {

                    dtTProfile = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }


            return dtTProfile;
        }

        /// <summary>
        /// Get Teacher Profile By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetTProfileById(int TProfileId)
        {
            DataTable dtTProfileDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetTeacherProfileById"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@TProfileId", DbType.Int32, TProfileId);
                    dtTProfileDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTProfileDetails;
        }

        /// <summary>
        /// Insert Update Teacher Profile
        /// </summary>
        /// <param name="tAddress"></param>
        /// <returns></returns>
        public int InsertUpdateTProfile(TeacherProfile tProfile)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_InsertUpdateTeacherProfile"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, tProfile.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ImagePath", DbType.String, tProfile.ImagePath);
                    //gObjDatabase.AddInParameter(objDbCommand, "@Contact2", DbType.String, tProfile.ContactSecond);

                    gObjDatabase.AddOutParameter(objDbCommand, "@TeacherProfileNewId", DbType.Int32, 4);

                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);


                    if (tProfile.TProfileId == 0)
                    {
                        var identity = Convert.ToInt32(objDbCommand.Parameters["@TeacherProfileNewId"].Value);
                        return (int)identity;
                    }
                    else if (tProfile.TProfileId > 0)
                    {
                        if (Convert.ToInt32(returnParameter.Value) > 0)
                            return Convert.ToInt32(returnParameter.Value);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return 0;
        }

        /// <summary>
        /// Delete Teacher Contact
        /// </summary>
        /// <param name="tAddress"></param>
        /// <returns></returns>
        public int DeleteTProfile(TeacherProfile tProfile)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("USP_DeleteTeacherProfileRecord"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@TeacherId", DbType.Int32, tProfile.TeacherId);
                    gObjDatabase.AddInParameter(objDbCommand, "@ImagePath", DbType.DateTime, tProfile.ImagePath);
                    //gObjDatabase.AddInParameter(objDbCommand, "@Contact2", DbType.Int32, tProfile.ContactSecond);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    var returnValues = returnParameter.Value;
                    if ((int)returnValues == 1)
                    {
                        return 1;  // Successfully Deleted/DeActive
                    }

                }
            }
            catch
            {
                throw;
            }

            return 0;  // show Error in inserting or Updating Record

        }

        #endregion
    }
}
