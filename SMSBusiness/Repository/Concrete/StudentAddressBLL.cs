using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class StudentAddressBLL : IStudentAddressBLL
    {
        public StudentAddress GetStudentAddressByStudentId(int StudentId)
        {
            var objStudAddressDao = new StudentAddressDAO(new SqlDatabase());
            DataTable stdAddress;
            StudentAddress gStdAdd = new StudentAddress();
            try
            {
                stdAddress = objStudAddressDao.GetStudentInfoByStudentId(StudentId);
                if (stdAddress.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAddress.Rows)
                    {
                        gStdAdd.StudentId = Convert.ToInt32(item["StudentId"]);
                        gStdAdd.CityId = Convert.ToInt32(item["CityId"]);
                        gStdAdd.StudentAddressId = Convert.ToInt32(item["StudentAddressId"]);
                        gStdAdd.PermanentAddress = item["PermanentAddress"].ToString();
                        gStdAdd.PresentAddress = item["PresentAddress"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return gStdAdd;



        }

        public int StudentAddressAddChanges(StudentAddress stdAdd)
        {
            var objsAddDao = new StudentAddressDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objsAddDao.InsertUpdateStudentAddress(stdAdd);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

    }
}
