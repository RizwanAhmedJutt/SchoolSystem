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
    public class DailyAssessmentSubTypeBLL : IDailyAssessmentSubType
    {
        public List<DailyAssessmentSubType> GetAllAssessmentSubType()
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLDailyAssessmentSubType();
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.AssessmentSubTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["AssementName"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.AssessmentTypeId=Convert.ToInt32(dr["AssessmentTypeId"]);
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentSubType> GetALLAcadmicAssessmentSubType()
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLAcadmicAssessmentSubType();
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.AssessmentSubTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["AssementName"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["AssessmentTypeId"]);
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentSubType> GetALLTeacherGeneralAssessmentSubType()
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetALLTeacherGeneralAssessmentSubType();
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.AssessmentSubTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["AssementName"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["AssessmentTypeId"]);
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        } 
        public DailyAssessmentSubType GetDailyAssessmentSubTypeById(int AssessmentSubTypeId)
        {
            var objgConatactsDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            DailyAssessmentSubType assessment = new DailyAssessmentSubType();
            try
            {
                stdAssessmentDetail = objgConatactsDao.GetDailyAssessmentSubTypeById(AssessmentSubTypeId);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentSubTypeId = int.Parse(item["AssessmentSubTypeId"].ToString());
                        assessment.AssessmentSubTypeName = item["AssessmentSubTypeName"].ToString();
                        assessment.AssessmentTypeId = Convert.ToInt32(item["AssessmentTypeId"]);
                        assessment.ParentAssementName = item["AssementName"].ToString();
                        assessment.AssessmentFormat = Convert.ToBoolean(item["AssessmentFormat"]);
                        assessment.CreatedById = item["CreatedById"].ToString();
                        assessment.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                        assessment.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);
                        assessment.ModifiedById = item.IsNull("ModifiedById") ? string.Empty : item["ModifiedById"].ToString();


                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assessment;



        }
        public int AddChangesAssessmentSubType(DailyAssessmentSubType dAssessmentsubType)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAssessmentDao.InsertUpdateDailyAssessmentSubType(dAssessmentsubType);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;
        }
        public DailyAssessmentSubType CheckSubAssementExist(int ParentAssessmentId, string SubAssessmentName)
        {
            var objgConatactsDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            DataTable stdAssessmentDetail;
            DailyAssessmentSubType assessment = new DailyAssessmentSubType();
            try
            {
                stdAssessmentDetail = objgConatactsDao.CheckSubAssementExist(ParentAssessmentId, SubAssessmentName);
                if (stdAssessmentDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdAssessmentDetail.Rows)
                    {
                        assessment.AssessmentSubTypeId = int.Parse(item["AssessmentSubTypeId"].ToString());
                        assessment.AssessmentSubTypeName = item["AssessmentSubTypeName"].ToString();
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assessment;


        }
        public List<DailyAssessmentSubType> GetStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetStudentGeneralAssessment(AcadmicClassId,StudentId,CreateDate);
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.OperationalId = Convert.ToInt32(dr["AssessmentOperationalId"]);
                    AssessmentDetails.AcadmicClassName = dr["AcadmicClass"].ToString();
                    AssessmentDetails.StudentName = dr["StudentName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["ParentAssessmentName"].ToString();
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.SelectedEvaluation = dr["AssementStatus"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.Concequence = dr.IsNull("WorseConsequence") ? string.Empty : dr["WorseConsequence"].ToString();
                    AssessmentDetails.FormateCreateDate = dr["CreatedDate"].ToString();
                    AssessmentDetails.StudentId = Convert.ToInt32(dr["StudentId"]);
                    AssessmentDetails.AcadmicClassId = Convert.ToInt32(dr["AcadmicClassId"]);
                    
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentSubType> GetStudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetStudentAcadmicAssessment(AcadmicClassId, StudentId, CourseId, CreateDate);
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.OperationalId = Convert.ToInt32(dr["AssessmentOperationalId"]);
                    AssessmentDetails.AcadmicClassName = dr["AcadmicClass"].ToString();
                    AssessmentDetails.StudentName = dr["StudentName"].ToString();
                    AssessmentDetails.CourseName = dr["CourseName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["ParentAssessmentName"].ToString();
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.SelectedEvaluation = dr["AssementStatus"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.AverageConcequence = dr.IsNull("AverageConsequence") ? string.Empty : dr["AverageConsequence"].ToString();
                    AssessmentDetails.Concequence = dr.IsNull("WorseConsequence") ? string.Empty : dr["WorseConsequence"].ToString();
                    AssessmentDetails.FormateCreateDate = dr["CreatedDate"].ToString();
                    AssessmentDetails.StudentId = Convert.ToInt32(dr["StudentId"]);
                    AssessmentDetails.AcadmicClassId = Convert.ToInt32(dr["AcadmicClassId"]);
                    AssessmentDetails.CourseId = Convert.ToInt32(dr["CourseId"]);
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentSubType> GetTeacherGeneralAssessments(int? AcadmicClassId, int? TeacherId, int? CourseId, string CreateDate)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetTeacherGeneralAssessment(AcadmicClassId, TeacherId, CourseId, CreateDate);
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.OperationalId = Convert.ToInt32(dr["AssessmentOperationalId"]);
                    AssessmentDetails.AcadmicClassName = dr["AcadmicClass"].ToString();
                    AssessmentDetails.TeacherName = dr["TeacherName"].ToString();
                    AssessmentDetails.CourseName = dr["CourseName"].ToString();
                    AssessmentDetails.ParentAssementName = dr["ParentAssessmentName"].ToString();
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.SelectedEvaluation = dr["AssementStatus"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.AverageConcequence = dr.IsNull("AverageConsequence") ? string.Empty : dr["AverageConsequence"].ToString();
                    AssessmentDetails.Concequence = dr.IsNull("WorseConsequence") ? string.Empty : dr["WorseConsequence"].ToString();
                    AssessmentDetails.FormateCreateDate = dr["CreatedDate"].ToString();
                    AssessmentDetails.TeacherId = Convert.ToInt32(dr["TeacherId"]);
                    AssessmentDetails.AcadmicClassId = Convert.ToInt32(dr["AcadmicClassId"]);
                    AssessmentDetails.CourseId = Convert.ToInt32(dr["CourseId"]);
                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        } 
        public List<DailyAssessmentSubType> GetStudentSingleGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetStudentSingleGeneralAssessment(AcadmicClassId, StudentId, CreateDate);
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.OperationalId = Convert.ToInt32(dr["DailyAssessmentOpertationId"]);
                    AssessmentDetails.AcadmicClassId = Convert.ToInt32( dr["AcadmicClassId"]);
                    AssessmentDetails.StudentId =Convert.ToInt32( dr["StudentId"]);
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["ParentAssessmentId"]);
                    AssessmentDetails.AssessmentSubTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.AssessmentFormat =Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.SelectedEvaluation = dr["AssementStatus"].ToString();
                    AssessmentDetails.Concequence = dr.IsNull("WorseConsequence") ? string.Empty : dr["WorseConsequence"].ToString();
                    AssessmentDetails.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                    AssessmentDetails.CreatedById = dr["CreatedById"].ToString();
               

                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }
        public List<DailyAssessmentSubType> GetStudentSingleAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetStudentSingleAcadmicAssessment(AcadmicClassId, StudentId,CourseId ,CreateDate);
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.OperationalId = Convert.ToInt32(dr["AcadmicAssessmentOperationId"]);
                    AssessmentDetails.AcadmicClassId = Convert.ToInt32(dr["AcadmicClassId"]);
                    AssessmentDetails.StudentId = Convert.ToInt32(dr["StudentId"]);
                    AssessmentDetails.CourseId = Convert.ToInt32(dr["CourseId"]);
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["ParentAssessmentId"]);
                    AssessmentDetails.AssessmentSubTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.SelectedEvaluation = dr["AssementStatus"].ToString();
                    AssessmentDetails.Concequence = dr.IsNull("AverageConsequence") ? string.Empty : dr["AverageConsequence"].ToString();
                    AssessmentDetails.Concequence = dr.IsNull("WorseConsequenec") ? string.Empty : dr["WorseConsequenec"].ToString();
                    AssessmentDetails.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                    AssessmentDetails.CreatedById = dr["CreatedById"].ToString();


                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        }

        public List<DailyAssessmentSubType> GetTeacherSingleGeneralAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            var objAssessmentDao = new DailyAssessmentSubTypeDAO(new SqlDatabase());
            var dtAssement = new DataTable();
            dtAssement = objAssessmentDao.GetTeacherSingleGeneralAssessment(AcadmicClassId, StudentId, CourseId, CreateDate);
            List<DailyAssessmentSubType> objAssementList = new List<DailyAssessmentSubType>();

            try
            {

                foreach (DataRow dr in dtAssement.Rows)
                {
                    var AssessmentDetails = new DailyAssessmentSubType();
                    AssessmentDetails.OperationalId = Convert.ToInt32(dr["TeacherAssessmentOperationId"]);
                    AssessmentDetails.AcadmicClassId = Convert.ToInt32(dr["AcadmicClassId"]);
                    AssessmentDetails.TeacherId = Convert.ToInt32(dr["TeacherId"]);
                    AssessmentDetails.CourseId = Convert.ToInt32(dr["CourseId"]);
                    AssessmentDetails.AssessmentTypeId = Convert.ToInt32(dr["ParentAssessmentId"]);
                    AssessmentDetails.AssessmentSubTypeId = Convert.ToInt32(dr["AssessmentSubTypeId"]);
                    AssessmentDetails.AssessmentSubTypeName = dr["AssessmentSubTypeName"].ToString();
                    AssessmentDetails.AssessmentFormat = Convert.ToBoolean(dr["AssessmentFormat"]);
                    AssessmentDetails.SelectedEvaluation = dr["AssementStatus"].ToString();
                    AssessmentDetails.Concequence = dr.IsNull("AverageConsequence") ? string.Empty : dr["AverageConsequence"].ToString();
                    AssessmentDetails.Concequence = dr.IsNull("WorseConsequenec") ? string.Empty : dr["WorseConsequenec"].ToString();
                    AssessmentDetails.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                    AssessmentDetails.CreatedById = dr["CreatedById"].ToString();


                    objAssementList.Add(AssessmentDetails);

                }

                return objAssementList;

            }
            catch
            {

                throw;
            }
        } 

    }
}
