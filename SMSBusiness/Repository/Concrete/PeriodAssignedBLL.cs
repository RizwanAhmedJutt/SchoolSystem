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
    public class PeriodAssignedBLL: IPeriodAssigned
    {

        
        public List<PeriodAssigned> GetALLAssignedPeriods()
        {
            var objPeriodAssignedDao = new PeriodAssignedDAO(new SqlDatabase());
            DataTable dtPeriods = objPeriodAssignedDao.GetALLAssignedPeriods();
            List<PeriodAssigned> periodAssigned = new List<PeriodAssigned>();
            try
            {

                foreach (DataRow item in dtPeriods.Rows)
                {
                    PeriodAssigned period = new PeriodAssigned();
                    period.PeriodAssignedId = item.IsNull("PeriodAssignedId") ? 0: Convert.ToInt32(item["PeriodAssignedId"]);
                    period.PeriodNumber =item.IsNull("PeriodNumber") ? 0 : Convert.ToInt32(item["PeriodNumber"]);
                    period.AcadmicClassId = item.IsNull("AcadmicClassId") ? 0 : Convert.ToInt32(item["AcadmicClassId"]);
                    period.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                    period.ClassName = item.IsNull("ClassName") ?string.Empty: item["ClassName"].ToString();
                    period.CourseName =item.IsNull("CourseName") ? string.Empty  : item["CourseName"].ToString();
                    periodAssigned.Add(period);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return periodAssigned;
        }

        public PeriodAssigned GetAssignedPeriodById(int PeriodAssignedId)
        {
            var objPeriodAssignedDao = new PeriodAssignedDAO(new SqlDatabase());
            DataTable dtPeriods = objPeriodAssignedDao.GetAssignedPeriodById(PeriodAssignedId);
            PeriodAssigned period = new PeriodAssigned();
            try
            {

                foreach (DataRow item in dtPeriods.Rows)
                {
                    period.PeriodAssignedId = item.IsNull("PeriodAssignedId") ? 0 : Convert.ToInt32(item["PeriodAssignedId"]);
                    period.PeriodNumber = item.IsNull("PeriodNumber") ? 0 : Convert.ToInt32(item["PeriodNumber"]);
                    period.AcadmicClassId = item.IsNull("AcadmicClassId") ? 0 : Convert.ToInt32(item["AcadmicClassId"]);
                    period.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return period;
        }

        public int AddChangesPeriods(PeriodAssigned periodAssigned)
        {
            var objPeriodAssignedDao = new PeriodAssignedDAO(new SqlDatabase());

            return objPeriodAssignedDao.AddChangesPeriods(periodAssigned);
        }
        
        public List<PeriodAssigned> PeroidAssignedByAcadmicClass(string AcadmicClassName)
        {
            var objPeriodAssignedDao = new PeriodAssignedDAO(new SqlDatabase());
            DataTable dtPeriods = objPeriodAssignedDao.PeriodAssignedByAcadmicClass(AcadmicClassName);
            List<PeriodAssigned> periodAssigned = new List<PeriodAssigned>();
            try
            {

                foreach (DataRow item in dtPeriods.Rows)
                {
                    PeriodAssigned period = new PeriodAssigned();
                    period.PeriodAssignedId = item.IsNull("PeriodAssignedId") ? 0 : Convert.ToInt32(item["PeriodAssignedId"]);
                    period.PeriodNumber = item.IsNull("PeriodNumber") ? 0 : Convert.ToInt32(item["PeriodNumber"]);
                    period.AcadmicClassId = item.IsNull("AcadmicClassId") ? 0 : Convert.ToInt32(item["AcadmicClassId"]);
                    period.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                    period.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    period.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    periodAssigned.Add(period);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return periodAssigned;

        }

        public List<PeriodAssigned> PeroidAssignedByCourse(string CourseName)
        {

            var objPeriodAssignedDao = new PeriodAssignedDAO(new SqlDatabase());
            DataTable dtPeriods = objPeriodAssignedDao.PeriodAssignedByCourse(CourseName);
            List<PeriodAssigned> periodAssigned = new List<PeriodAssigned>();
            try
            {

                foreach (DataRow item in dtPeriods.Rows)
                {
                    PeriodAssigned period = new PeriodAssigned();
                    period.PeriodAssignedId = item.IsNull("PeriodAssignedId") ? 0 : Convert.ToInt32(item["PeriodAssignedId"]);
                    period.PeriodNumber = item.IsNull("PeriodNumber") ? 0 : Convert.ToInt32(item["PeriodNumber"]);
                    period.AcadmicClassId = item.IsNull("AcadmicClassId") ? 0 : Convert.ToInt32(item["AcadmicClassId"]);
                    period.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                    period.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    period.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    periodAssigned.Add(period);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return periodAssigned;
        } 
        public PeriodAssigned CheckAlreadyPeriodAssigned(int PeriodNumber,int AcadmicClassId ,int CourseId)
        {
            var objPeriodAssignedDao = new PeriodAssignedDAO(new SqlDatabase());
            DataTable dtPeriods = objPeriodAssignedDao.CheckalreadyPeriodAssigned(PeriodNumber, AcadmicClassId, CourseId);
            PeriodAssigned period = new PeriodAssigned();
            try
            {

                foreach (DataRow item in dtPeriods.Rows)
                {
                    period.PeriodAssignedId = item.IsNull("PeriodAssignedId") ? 0 : Convert.ToInt32(item["PeriodAssignedId"]);
                    period.PeriodNumber = item.IsNull("PeriodNumber") ? 0 : Convert.ToInt32(item["PeriodNumber"]);
                    period.AcadmicClassId = item.IsNull("AcadmicClassId") ? 0 : Convert.ToInt32(item["AcadmicClassId"]);
                    period.CourseId = item.IsNull("CourseId") ? 0 : Convert.ToInt32(item["CourseId"]);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return period;
        }
    }
}
