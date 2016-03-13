using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
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
        public List<Teacher> GetAllStudents()
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
                    //TeacherDetails.TeacherId = dr.IsNull("TeacherId") ? 0 : Convert.ToInt32(dr["TeacherId"]);
                    TeacherDetails.FirstName = dr.IsNull("FirstName") ? string.Empty : Convert.ToString(dr["FirstName"]);
                    TeacherDetails.LastName = dr.IsNull("LastName") ? string.Empty : Convert.ToString(dr["LastName"]);
                    TeacherDetails.CNIC = dr.IsNull("CNIC") ? string.Empty : Convert.ToString(dr["CNIC"]);
                    TeacherDetails.LastQualification = dr.IsNull("LastQualification") ? String.Empty : Convert.ToString( dr["LastQualification"]);
                    TeacherDetails.JoinDate = dr.IsNull("JoinDate") ? DateTime.Now : Convert.ToDateTime(dr["JoinDate"]);
                    TeacherDetails.LeaveDate = dr.IsNull("LeaveDate") ? DateTime.Now : Convert.ToDateTime(dr["LeaveDate"]);
                    TeacherDetails.RefrenceName = dr.IsNull("RefrenceName") ? string.Empty : Convert.ToString(dr["RefrenceName"]);
                    TeacherDetails.RefrenceContact = dr.IsNull("RefrenceContact") ? string.Empty : Convert.ToString(dr["RefrenceContact"]);
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
                        teacher.LeaveDate = item.IsNull("LeaveDate") ? DateTime.Now : Convert.ToDateTime(item["LeaveDate"]);
                        teacher.RefrenceName = item.IsNull("RefrenceName") ? string.Empty : Convert.ToString(item["RefrenceName"]);
                        teacher.RefrenceContact = item.IsNull("RefrenceContact") ? string.Empty : Convert.ToString(item["RefrenceContact"]);
                        teacher.CreatedById = item.IsNull("CreatedById") ? string.Empty : Convert.ToString(item["CreatedById"]);
                        teacher.CreatedDate = item.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(item["CreatedDate"]);
                        teacher.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : Convert.ToString(item["ModifiedById"]);
                        teacher.ModifiedDate = item.IsNull("ModifiedDate") ? DateTime.Now : Convert.ToDateTime(item["ModifiedDate"]);
                    }
                }


            }
            catch (Exception ex)
            {

                throw;
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
                ReturnValue = objTeacherDao.DeleteStudent(teacher);


            }
            catch (Exception)
            {

                throw;
            }


            return ReturnValue;
        }

        #endregion
    }
}
