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
        #region
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
                    TeacherDetails.TeacherId = dr.IsNull("StudentId") ? 0 : Convert.ToInt32(dr["StudentId"]);
                    TeacherDetails.FirstName = dr.IsNull("FirstName") ? string.Empty : Convert.ToString(dr["FirstName"]);
                    TeacherDetails.LastName = dr.IsNull("LastName") ? string.Empty : Convert.ToString(dr["LastName"]);
                    TeacherDetails.CNIC = dr.IsNull("CNIC") ? string.Empty : Convert.ToString(dr["CNIC"]);
                    TeacherDetails.LastQualification = dr.IsNull("LastQualification") ? String.Empty : Convert.ToString( dr["LastQualification"]);


                    TeacherDetails.JoinDate = dr.IsNull("JoinDate") ? DateTime.Now : Convert.ToDateTime(dr["JoinDate"]);
                    TeacherDetails.LeaveDate = dr.IsNull("LeaveDate") ? DateTime.Now : Convert.ToDateTime(dr["LeaveDate"]);
                    TeacherDetails.RefrenceName = dr.IsNull("RefrenceName") ? string.Empty : Convert.ToString(dr["RefrenceName"]);
                    TeacherDetails.RefrenceContact = dr.IsNull("RefrenceContact") ? string.Empty : Convert.ToString(dr["RefrenceContact"]);
                    TeacherDetails.CreatedById = dr.IsNull("CreatedById") ? string.Empty : Convert.ToString(dr["CreatedById"]);
                    TeacherDetails.CreatedDate = dr.IsNull("CreatedDate") ? DateTime.Now : Convert.ToDateTime(dr["CreatedDate"]);
                    TeacherDetails.ModifiedById = dr.IsNull("ModifiedById") ? string.Empty : Convert.ToString(dr["ModifiedById"]);
                    TeacherDetails.ModifiedDate = dr.IsNull("ModifiedDate") ?DateTime.Now : Convert.ToDateTime(dr["ModifiedById"]);
                    objTeacherList.Add(TeacherDetails);
                }

                return objTeacherList;

            }
            catch
            {

                throw;
            }
        }

        #endregion
    }
}
