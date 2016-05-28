using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class TeacherRepositry : ITeacherRepositry
    {
        #region Add, Edit, Update And Delete Teacher 
        public List<Teacher> GetAllTeachers()
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            var dtTeachers = new DataTable();
            dtTeachers = objTeacherDao.GetALLTeachers();
            List<Teacher> objTeacherList = new List<Teacher>();

            try
            {

                foreach (DataRow dr in dtTeachers.Rows)
                {
                    var TeacherDetails = new Teacher();
                    TeacherDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    TeacherDetails.FirstName = dr.IsNull("FirstName") ? string.Empty : Convert.ToString(dr["FirstName"]);
                    TeacherDetails.LastName = dr.IsNull("LastName") ? string.Empty : Convert.ToString(dr["LastName"]);
                    TeacherDetails.CNIC = dr.IsNull("CNIC") ? string.Empty : Convert.ToString(dr["CNIC"]);
                    TeacherDetails.LastQualification = dr.IsNull("LastQualification") ? String.Empty : Convert.ToString( dr["LastQualification"]);
                    TeacherDetails.JoinDate = dr.IsNull("JoinDate") ? DateTime.Now : Convert.ToDateTime(dr["JoinDate"]);
                    TeacherDetails.LeaveDate = dr.IsNull("LeaveDate") ?(DateTime?) null: Convert.ToDateTime(dr["LeaveDate"]);
                    TeacherDetails.RefrenceName = dr.IsNull("RefrenceName") ? string.Empty : Convert.ToString(dr["RefrenceName"]);
                    TeacherDetails.RefrenceContact = dr.IsNull("RefrenceContact") ? string.Empty : Convert.ToString(dr["RefrenceContact"]);
                    TeacherDetails.Active = Convert.ToBoolean(dr["Active"]);
                    //TeacherDetails.CreatedById = dr.IsNull("CreatedById") ? string.Empty : Convert.ToString(dr["CreatedById"]);
                    //TeacherDetails.CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"]);
                    //TeacherDetails.ModifiedById = dr.IsNull("ModifiedById") ? string.Empty : Convert.ToString(dr["ModifiedById"]);
                    //TeacherDetails.ModifiedDate = dr.IsNull("ModifiedDate") ?DateTime.Now : Convert.ToDateTime(dr["ModifiedById"]);
                    objTeacherList.Add(TeacherDetails);
                }

                return objTeacherList;

            }
            catch
            {

                throw;
            }
        }
        public List<Teacher> GetAllTeacherByCNIC(string CNIC)
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            var dtTeachers = new DataTable();
            dtTeachers = objTeacherDao.GetALLTeacherByCNIC(CNIC);
            List<Teacher> objTeacherList = new List<Teacher>();

            try
            {

                foreach (DataRow dr in dtTeachers.Rows)
                {
                    var TeacherDetails = new Teacher();
                    TeacherDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    TeacherDetails.FirstName = dr.IsNull("FirstName") ? string.Empty : Convert.ToString(dr["FirstName"]);
                    TeacherDetails.LastName = dr.IsNull("LastName") ? string.Empty : Convert.ToString(dr["LastName"]);
                    TeacherDetails.CNIC = dr.IsNull("CNIC") ? string.Empty : Convert.ToString(dr["CNIC"]);
                    TeacherDetails.LastQualification = dr.IsNull("LastQualification") ? String.Empty : Convert.ToString(dr["LastQualification"]);
                    TeacherDetails.JoinDate = dr.IsNull("JoinDate") ? DateTime.Now : Convert.ToDateTime(dr["JoinDate"]);
                    TeacherDetails.LeaveDate = dr.IsNull("LeaveDate") ? (DateTime?)null : Convert.ToDateTime(dr["LeaveDate"]);
                    TeacherDetails.RefrenceName = dr.IsNull("RefrenceName") ? string.Empty : Convert.ToString(dr["RefrenceName"]);
                    TeacherDetails.RefrenceContact = dr.IsNull("RefrenceContact") ? string.Empty : Convert.ToString(dr["RefrenceContact"]);
                    TeacherDetails.Active = Convert.ToBoolean(dr["Active"]);
                  
                    objTeacherList.Add(TeacherDetails);
                }

                return objTeacherList;

            }
            catch
            {

                throw;
            }
        }

        public Teacher GetTeacherById(int TeacherId)
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            DataTable teachDetail;
            Teacher teacher = new Teacher();
            try
            {
                teachDetail = objTeacherDao.GetTeacherById(TeacherId);
                if (teachDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in teachDetail.Rows)
                    {
                        teacher.TeacherId = item.IsNull("TeacherId") ? 0 : Convert.ToInt32(item["TeacherId"]);
                        teacher.FirstName = item.IsNull("FirstName") ? string.Empty : item["FirstName"].ToString();
                        teacher.LastName = item.IsNull("LastName") ? string.Empty : item["LastName"].ToString();
                        teacher.CNIC = item.IsNull("CNIC") ? string.Empty : Convert.ToString(item["CNIC"].ToString());
                        teacher.LastQualification = item.IsNull("LastQualification") ? string.Empty : Convert.ToString(item["LastQualification"]);
                        teacher.JoinDate = item.IsNull("JoinDate") ? DateTime.Now : Convert.ToDateTime(item["JoinDate"]);
                        teacher.LeaveDate = item.IsNull("LeaveDate") ? (DateTime?)null : Convert.ToDateTime(item["LeaveDate"]);
                        teacher.RefrenceName = item.IsNull("RefrenceName") ? string.Empty : Convert.ToString(item["RefrenceName"]);
                        teacher.RefrenceContact = item.IsNull("RefrenceContact") ? string.Empty : Convert.ToString(item["RefrenceContact"]);
                        teacher.Active = item.IsNull("Active") ? false : Convert.ToBoolean(item["Active"]);
                        teacher.CreatedById = item.IsNull("CreatedById") ? string.Empty : Convert.ToString(item["CreatedById"]);
                        teacher.CreatedDate = item.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(item["CreatedDate"]);
                        teacher.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : Convert.ToString(item["ModifiedById"]);
                        teacher.ModifiedDate = item.IsNull("ModifiedDate") ? DateTime.Now : Convert.ToDateTime(item["ModifiedDate"]);
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return teacher;



        }


        public int InsertUpdateTeacher(Teacher teacher)
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {

                ReturnValue = objTeacherDao.InsertUpdateTeacher(teacher);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

        public int DeleteTeacher(Teacher teacher)
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;
            try
            {
                ReturnValue = objTeacherDao.DeleteTeacher(teacher);


            }
            catch (Exception)
            {

                throw;
            }


            return ReturnValue;
        }

        #endregion


        #region Add, Edit, Update And Delete Teacher Address
        public List<TeacherAddress> GetAllTeacherAddress()
        {
            var objTAddressDao = new TeacherDAO(new SqlDatabase());
            var dtTAddress = new DataTable();
            dtTAddress = objTAddressDao.GetAllTeacherAddress();
            List<TeacherAddress> objTAddressList = new List<TeacherAddress>();

            try
            {

                foreach (DataRow dr in dtTAddress.Rows)
                {
                    var TAddressDetails = new TeacherAddress();
                    TAddressDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    TAddressDetails.PresentAddress = dr.IsNull("PresentAddress") ? string.Empty : Convert.ToString(dr["PresentAddress"]);
                    TAddressDetails.PermanentAddress = dr.IsNull("PermanentAddress") ? string.Empty : Convert.ToString(dr["PermanentAddress"]);

                    objTAddressList.Add(TAddressDetails);
                }

                return objTAddressList;

            }
            catch
            {

                throw;
            }
        }

        public TeacherAddress GetTeacherAddressByTeacherId(int TeacherId)
        {
            var objTAddressDao = new TeacherDAO(new SqlDatabase());
            DataTable tAddressDetail;
            TeacherAddress tAddress = new TeacherAddress();
            try
            {
                tAddressDetail = objTAddressDao.GetTeacherAddressByTeacherId(TeacherId);
                if (tAddressDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in tAddressDetail.Rows)
                    {
                        tAddress.TAddressId = item.IsNull("TAddressId") ? 0 : Convert.ToInt32(item["TAddressId"]);
                        tAddress.TeacherId = item.IsNull("TeacherId") ? 0 : Convert.ToInt32(item["TeacherId"]);
                        tAddress.PresentAddress = item.IsNull("PresentAddress") ? string.Empty : item["PresentAddress"].ToString();
                        tAddress.PermanentAddress = item.IsNull("PermanentAddress") ? string.Empty : item["PermanentAddress"].ToString();
                        
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tAddress;



        }


        public int InsertUpdateTAddress(TeacherAddress tAddress)
        {
            var objTAddressDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {

                ReturnValue = objTAddressDao.InsertUpdateTAddress(tAddress);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

        public int DeleteTeacher(TeacherAddress tAddress)
        {
            var objTAddressDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;
            try
            {
                ReturnValue = objTAddressDao.DeleteTAddress(tAddress);


            }
            catch (Exception)
            {

                throw;
            }


            return ReturnValue;
        }

        #endregion


        #region Add, Edit, Update And Delete Teacher Contacts
        public List<TeacherContact> GetAllTeacherContact()
        {
            var objTContactDao = new TeacherDAO(new SqlDatabase());
            var dtTContact = new DataTable();
            dtTContact = objTContactDao.GetAllTeacherContact();
            List<TeacherContact> objTContactList = new List<TeacherContact>();

            try
            {

                foreach (DataRow dr in dtTContact.Rows)
                {
                    var TContactDetails = new TeacherContact();
                    TContactDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    TContactDetails.ContactFrist = dr.IsNull("Contact1") ? string.Empty : Convert.ToString(dr["Contact1"]);
                    TContactDetails.ContactSecond = dr.IsNull("Contact2") ? string.Empty : Convert.ToString(dr["Contact2"]);

                    objTContactList.Add(TContactDetails);
                }

                return objTContactList;

            }
            catch
            {

                throw;
            }
        }

        public TeacherContact GetTContactsById(int TeacherId)
        {
            var objTContactDao = new TeacherDAO(new SqlDatabase());
            DataTable tContactDetail;
            TeacherContact tContact = new TeacherContact();
            try
            {
                tContactDetail = objTContactDao.GetTContactsById( TeacherId);
                if (tContactDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in tContactDetail.Rows)
                    {
                        tContact.TeacherContactId = item.IsNull("TeacherContactId") ? 0 : Convert.ToInt32(item["TeacherContactId"]);
                        tContact.TeacherId = item.IsNull("TeacherId") ? 0 : Convert.ToInt32(item["TeacherId"]);
                        tContact.ContactFrist = item.IsNull("Contact1") ? string.Empty : item["Contact1"].ToString();
                        tContact.ContactSecond = item.IsNull("Contact1") ? string.Empty : item["Contact1"].ToString();

                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tContact;



        }


        public int InsertUpdateTContact(TeacherContact tContact)
        {
            var objTContactDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {

                ReturnValue = objTContactDao.InsertUpdateTContact(tContact);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

        public int DeleteContact(TeacherContact tContact)
        {
            var objTContactDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;
            try
            {
                ReturnValue = objTContactDao.DeleteTContact(tContact);


            }
            catch (Exception)
            {

                throw;
            }


            return ReturnValue;
        }

        #endregion

        #region Add, Edit, Update And Delete Teacher Profile
        public List<TeacherProfile> GetAllTeacherProfile()
        {
            var objTProfileDao = new TeacherDAO(new SqlDatabase());
            var dtTProfile = new DataTable();
            dtTProfile = objTProfileDao.GetAllTeacherProfile();
            List<TeacherProfile> objTProfileList = new List<TeacherProfile>();

            try
            {

                foreach (DataRow dr in dtTProfile.Rows)
                {
                    var TProfileDetails = new TeacherProfile();
                    TProfileDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    TProfileDetails.ImagePath = dr.IsNull("ImagePath") ? string.Empty : Convert.ToString(dr["ImagePath"]);


                    objTProfileList.Add(TProfileDetails);
                }

                return objTProfileList;

            }
            catch
            {

                throw;
            }
        }

        public TeacherProfile GetTProfileById(int TeacherId)
        {
            var objTProfileDao = new TeacherDAO(new SqlDatabase());
            DataTable tProfileDetail;
            TeacherProfile tProfile = new TeacherProfile();
            try
            {
                tProfileDetail = objTProfileDao.GetTProfileById(TeacherId);
                if (tProfileDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in tProfileDetail.Rows)
                    {
                       // tProfile.TProfileId=item.IsNull()
                        tProfile.TProfileId = item.IsNull("TProfileId") ? 0 : Convert.ToInt32(item["TProfileId"]);
                        tProfile.TeacherId = item.IsNull("TeacherId") ? 0 : Convert.ToInt32(item["TeacherId"]);
                        tProfile.ImagePath = item.IsNull("ImagePath") ? string.Empty : item["ImagePath"].ToString();
                        //tProfile.ContactSecond = item.IsNull("Contact1") ? string.Empty : item["Contact1"].ToString();

                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return tProfile;



        }

        public List<Teacher> GetALLTeacherByName()
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            var dtTeachers = new DataTable();
            dtTeachers = objTeacherDao.GetALLTeacherByName();
            List<Teacher> objTeacherList = new List<Teacher>();
            try
            {
                if (dtTeachers.Rows.Count > 0)
                {
                    foreach (DataRow item in dtTeachers.Rows)
                    {
                        Teacher teacher = new Teacher();
                        teacher.TeacherName = item["TeacherName"].ToString();
                        teacher.TeacherId = Convert.ToInt32(item["TeacherId"]);
                        objTeacherList.Add(teacher); 

                    }
                    return objTeacherList;
                }

            }
            catch (Exception )
            {
                
                throw;
            }
            return objTeacherList;
        }

        public int InsertUpdateTProfile(TeacherProfile tProfile)
        {
            var objTProfileDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {

                ReturnValue = objTProfileDao.InsertUpdateTProfile(tProfile);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

        public int DeleteProfile(TeacherProfile tProfile)
        {
            var objTProfileDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;
            try
            {
                ReturnValue = objTProfileDao.DeleteTProfile(tProfile);


            }
            catch (Exception)
            {

                throw;
            }


            return ReturnValue;
        }

        #endregion 
        public int InsertUpdateTeacherAssignClass(TeacherAssignClass tclassAssign)
        {
            var objTProfileDao = new TeacherDAO(new SqlDatabase());
            int ReturnValue = 0;
            try
            {
                ReturnValue = objTProfileDao.InsertUpdateTeacherAssignClass(tclassAssign);


            }
            catch (Exception)
            {

                throw;
            }


            return ReturnValue;
        }
        public List<Teacher>  GetTeacherByClass(int AcadmicClassId)
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            var dtTeachers = new DataTable();
            dtTeachers = objTeacherDao.GetTeacherByClass(AcadmicClassId);
            List<Teacher> objTeacherList = new List<Teacher>();

            try
            {

                foreach (DataRow dr in dtTeachers.Rows)
                {
                    var TeacherDetails = new Teacher();
                    TeacherDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    TeacherDetails.TeacherName = dr.IsNull("TeacherName") ? string.Empty : Convert.ToString(dr["TeacherName"]);
                    objTeacherList.Add(TeacherDetails);
                }

                return objTeacherList;

            }
            catch
            {

                throw;
            }


        }
        public List<TeacherAssignClass> GetAllTeacherAssignClass()
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            var dtTeachers = new DataTable();
            dtTeachers = objTeacherDao.GetAllTeacherAssignClass();
            List<TeacherAssignClass> objTeacherList = new List<TeacherAssignClass>();

            try
            {

                foreach (DataRow dr in dtTeachers.Rows)
                {
                    var TeacherDetails = new TeacherAssignClass();
                    TeacherDetails.TeacherAssignId = dr.IsNull("TeacherAssignId") ? 0 : Convert.ToInt32(dr["TeacherAssignId"]);
                    TeacherDetails.TeacherName = dr.IsNull("TeacherName") ? string.Empty : Convert.ToString(dr["TeacherName"]);
                    TeacherDetails.ClassName = dr.IsNull("ClassName") ? string.Empty : Convert.ToString(dr["ClassName"]);
                    TeacherDetails.AcadmicClassId = dr.IsNull("ClassId") ? 0 : Convert.ToInt32(dr["ClassId"]);
                    TeacherDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    objTeacherList.Add(TeacherDetails);
                }

                return objTeacherList;

            }
            catch
            {

                throw;
            }


        }
        public List<TeacherAssignClass> GetAllTeacherAssignClassByTeacherName(string TeacherName)
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            var dtTeachers = new DataTable();
            dtTeachers = objTeacherDao.GetAllTeacherAssignClassByTeacherName(TeacherName);
            List<TeacherAssignClass> objTeacherList = new List<TeacherAssignClass>();

            try
            {

                foreach (DataRow dr in dtTeachers.Rows)
                {
                    var TeacherDetails = new TeacherAssignClass();
                    TeacherDetails.TeacherAssignId = dr.IsNull("TeacherAssignId") ? 0 : Convert.ToInt32(dr["TeacherAssignId"]);
                    TeacherDetails.TeacherName = dr.IsNull("TeacherName") ? string.Empty : Convert.ToString(dr["TeacherName"]);
                    TeacherDetails.ClassName = dr.IsNull("ClassName") ? string.Empty : Convert.ToString(dr["ClassName"]);
                    objTeacherList.Add(TeacherDetails);
                }

                return objTeacherList;

            }
            catch
            {

                throw;
            }


        }
        public TeacherAssignClass GetTeacherAssignClassById(int AssignId)
        {
            var objTeacherDao = new TeacherDAO(new SqlDatabase());
            var dtTeachers = new DataTable();
            dtTeachers = objTeacherDao.GetTeacherAssignClassById(AssignId);
            TeacherAssignClass teacher = new TeacherAssignClass();
            try
            {
                if (dtTeachers.Rows.Count > 0)
                {
                    foreach (DataRow item in dtTeachers.Rows)
                    {
                        teacher.TeacherAssignId = Convert.ToInt32(item["TeacherAssignId"]);
                        teacher.AcadmicClassId = Convert.ToInt32(item["ClassId"]);
                        teacher.TeacherId = Convert.ToInt32(item["TeacherId"]);
                        teacher.TeacherName = item["TeacherName"].ToString();
                        teacher.ClassName = item["ClassName"].ToString();
                        

                    }
                    return teacher;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return teacher;
        }
    }
}
