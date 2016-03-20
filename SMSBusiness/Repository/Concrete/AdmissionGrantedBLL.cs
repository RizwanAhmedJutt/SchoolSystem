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
    public class AdmissionGrantedBLL : IAdmissionGranted
    {

        public AdmissionGranted GetAdmissionGrantedInfoByStudentId(int StudentId)
        {
            var objAdmissionDao = new AdmissionGrantedDAO(new SqlDatabase());
            DataTable stdAdmissionDetail;
            AdmissionGranted aGranted = new AdmissionGranted();
            try
            {
                stdAdmissionDetail = objAdmissionDao.GetAdmissionGrantedInfoByStudentId(StudentId);
                if (stdAdmissionDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAdmissionDetail.Rows)
                    {
                        aGranted.AdmissionId = Convert.ToInt32(item["AdmissionId"]);
                        aGranted.StudentId = Convert.ToInt32(item["StudentId"]);
                        aGranted.AssessmentResult = item["AssessmentResult"].ToString();
                        aGranted.IsGranted = Convert.ToBoolean(item["IsGranted"].ToString());
                        aGranted.AdmissionGrantedForClass = item["AdmissionGrantedForClass"].ToString();
                        aGranted.AdmissionGrantedDate = Convert.ToDateTime(item["AdmissionGrantedDate"].ToString());
                        aGranted.Remarks = item["Remarks"].ToString();
                        aGranted.CreatedById = item["CreatedById"].ToString();
                        aGranted.CreatedDate = Convert.ToDateTime(item["CreatedDate"].ToString());
                        aGranted.ModifiedById = item["ModifiedById"].ToString();
                       // aGranted.ModifiedDate = Convert.ToDateTime(item["ModifiedDate"].ToString());


                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return aGranted;



        }

        public int AdmissionAddChanges(AdmissionGranted aGranted)
        {
            var objAdmissionDao = new AdmissionGrantedDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAdmissionDao.InsertUpdateAdmissionGranted(aGranted);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }




    }
}
