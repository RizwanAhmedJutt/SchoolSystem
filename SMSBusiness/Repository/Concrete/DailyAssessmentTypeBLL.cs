﻿using SMSBusiness.Repository.Abstract;
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
    public class DailyAssessmentTypeBLL : IDailyAssessmentType
    {
        public List<DailyAssessmentType> GetAllAssessmentType()
        {
            var objAssessmentDao = new DailyAssessmentTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLDailyAssessmentType();
            List<DailyAssessmentType> objAssementList = new List<DailyAssessmentType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentType();
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["AssessmentTypeId"]);
                    AssessmentDetails.AssessmentName = dr["AssementName"].ToString();
                    AssessmentDetails.AssessmentCategoryId = Convert.ToInt32(dr["AssessmentCategoryId"]);
                    AssessmentDetails.AssementCategory = dr["AssessmentCategory"].ToString();
                    AssessmentDetails.AssessmentCriteria = dr["AssessmentCriteria"].ToString();
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentType> GetALLAssignedParentAssessments()
        {
            var objAssessmentDao = new DailyAssessmentTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLAssignedParentAssessments();
            List<DailyAssessmentType> objAssementList = new List<DailyAssessmentType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentType();
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["AssessmentTypeId"]);
                    AssessmentDetails.AssessmentName = dr["AssementName"].ToString();
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentType> GetALLAssignedParentAcadmicAssessments()
        {
            var objAssessmentDao = new DailyAssessmentTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLAssignedParentAcadmicAssessments();
            List<DailyAssessmentType> objAssementList = new List<DailyAssessmentType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentType();
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["AssessmentTypeId"]);
                    AssessmentDetails.AssessmentName = dr["AssementName"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentType> GetALLAssignedParentTeacherAcadmicAssessments()
        {
            var objAssessmentDao = new DailyAssessmentTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLAssignedParentTeacherAcadmicAssessments();
            List<DailyAssessmentType> objAssementList = new List<DailyAssessmentType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentType();
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["AssessmentTypeId"]);
                    AssessmentDetails.AssessmentName = dr["AssementName"].ToString();
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        } 
        public DailyAssessmentType GetDailyAssessmentById(int AssessmentTypeId)
        {
            var objgConatactsDao = new DailyAssessmentTypeDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            DailyAssessmentType assessment = new DailyAssessmentType();
            try
            {
                stdAssessmentDetail = objgConatactsDao.GetDailyAssessmentTypeById(AssessmentTypeId);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentTypeId = int.Parse(item["AssessmentTypeId"].ToString());
                        assessment.AssessmentName = item["AssementName"].ToString();
                        assessment.AssessmentCriteria = item["AssessmentCriteria"].ToString();
                        assessment.AssessmentCategoryId = Convert.ToInt32(item["AssessmentCategoryId"]);
                        assessment.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                        assessment.CreatedById = item["CreatedById"].ToString();
                        assessment.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : item["ModifiedById"].ToString();
                        assessment.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);


                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assessment;



        }

        public int AddChangeDailyAssessmentType(DailyAssessmentType dAssessmentsubType)
        {
            var objAssessmentDao = new DailyAssessmentTypeDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAssessmentDao.InsertUpdateDailyAssessmentType(dAssessmentsubType);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }

        public DailyAssessmentType GetDailyAssessmentTypeByName(string AssessmentName, int AssessmentCategoryId)
        {

            var objgConatactsDao = new DailyAssessmentTypeDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            DailyAssessmentType assessment = new DailyAssessmentType();
            try
            {
                stdAssessmentDetail = objgConatactsDao.GetDailyAssessmentTypeByName(AssessmentName, AssessmentCategoryId);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentTypeId = int.Parse(item["AssessmentTypeId"].ToString());
                        assessment.AssessmentName = item["AssementName"].ToString();
                        assessment.AssessmentCategoryId = Convert.ToInt32(item["AssessmentCategoryId"]);
                        assessment.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                        assessment.CreatedById = item["CreatedById"].ToString();
                        assessment.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : item["ModifiedById"].ToString();
                        assessment.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);


                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assessment;


        }

    }
}
