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
    public class GuardianDetailBLL : IGuardianDetail
    {

        public GuardianDetail GetGuardianInfoByStudentId(int StudentId)
        {
            var objgDetailDao = new GuardianDetailDAO(new SqlDatabase());
            DataTable stdgDetail;
            GuardianDetail gDetail = new GuardianDetail();
            try
            {
                stdgDetail = objgDetailDao.GetGuardianInfoByStudentId(StudentId);
                if (stdgDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdgDetail.Rows)
                    {
                        gDetail.StudentGuardianId = Convert.ToInt32(item["GuardianId"].ToString());
                        gDetail.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                        gDetail.FatherName = item["FatherName"].ToString();
                        gDetail.MotherName = item["MotherName"].ToString();
                        gDetail.MotherTongue = item["MotherTongue"].ToString();
                        gDetail.OccupationOfGuardian = item["OccupationOfGuardian"].ToString();
                        gDetail.RelationWithGuardian = item["RelationwithGuardian"].ToString();
                        gDetail.GuardianMontlyIncome = Convert.ToDecimal(item["GuardianMonthlyIncome"].ToString());
                        gDetail.GuardianQualification = item["GuardianQualification"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return gDetail;



        }

        public int GuardianContactAddChanges(GuardianDetail gDetail)
        {
            var objgDetailDao = new GuardianDetailDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objgDetailDao.InsertUpdateGuardian(gDetail);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }





    }
}
