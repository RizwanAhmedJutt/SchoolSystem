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
    public class ST_PreviousAcadmicDetailBLL : IST_PreviousAcadmicRecord
    {
        public ST_PreviousAcadmicDetail GetPreviousAcadmicInfoByStudentId(int StudentId)
        {
            var objacadmicDetailDao = new ST_PreviousAcadmicDetailDAO(new SqlDatabase());
            DataTable stdacadmicDetail;
            ST_PreviousAcadmicDetail PAcadmicDetail = new ST_PreviousAcadmicDetail();
            try
            {
                stdacadmicDetail = objacadmicDetailDao.GetStudentPreviousInfoByStudentId(StudentId);
                if (stdacadmicDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdacadmicDetail.Rows)
                    {
                        PAcadmicDetail.PAcadmicId = Convert.ToInt32(item["PAcadmicID"].ToString());
                        PAcadmicDetail.StudentId = Convert.ToInt32(item["StudentId"].ToString());
                        PAcadmicDetail.SchoolName = item["SchoolName"].ToString();
                        PAcadmicDetail.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"]);
                        PAcadmicDetail.PreviousExamPassed = Convert.ToBoolean(item["PreviousExamPassed"]);
                        PAcadmicDetail.Session = item["Session"].ToString();
                        PAcadmicDetail.MarksObtained = Convert.ToDecimal(item["MarksObtained".ToString()]);
                        PAcadmicDetail.TotalMark = Convert.ToDecimal(item["TotalMark"].ToString());
                        PAcadmicDetail.Grade = item["Grade"].ToString();
                        PAcadmicDetail.MediumOfInstruction = item["MediumOfInstruction"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {

                throw;
            }
            return PAcadmicDetail;



        }

        public int PreviousAcadmicDetailAddChanges(ST_PreviousAcadmicDetail PreviousDetail)
        {
            var objacadmicDetailDao = new ST_PreviousAcadmicDetailDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objacadmicDetailDao.InsertUpdateStudentPreviousInfo(PreviousDetail);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }




    }
}
