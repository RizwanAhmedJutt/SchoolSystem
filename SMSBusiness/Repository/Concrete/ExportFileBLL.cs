﻿using SMSDAL;
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
    public class ExportFileBLL
    {
        public List<StudentDetail> GetStudentReport()
        {
            var objExportFileDao = new ExportFileDAO(new SqlDatabase());
            DataTable dtDetail = objExportFileDao.GetStudentReport();
            List<StudentDetail> stdDetail = new List<StudentDetail>();
            try
            {

                foreach (DataRow item in dtDetail.Rows)
                {
                    StudentDetail getStdDetail = new StudentDetail();
                    getStdDetail.Student.StudentName = item.IsNull("StudentName") ? string.Empty : item["StudentName"].ToString();
                    getStdDetail.GuardianDetail.FatherName = item.IsNull("FatherName") ? string.Empty : item["FatherName"].ToString();
                    getStdDetail.AcadmicClass.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    getStdDetail.Student.DOB = Convert.ToDateTime(item["DateOfBirth"]);
                    getStdDetail.Student.Religion = item.IsNull("Religion") ? string.Empty : item["Religion"].ToString();
                    getStdDetail.Student.CNIC = item.IsNull("CNIC") ? string.Empty : item["CNIC"].ToString();
                    getStdDetail.Student.NoOfSibling = item.IsNull("TotalSibling") ? 0 : Convert.ToInt32(item["TotalSibling"]);
                    getStdDetail.Student.NoOfSiblingCurrentSchool = item.IsNull("CurrentSchoolSibling") ? 0:Convert.ToInt32(item["CurrentSchoolSibling"]);
                    getStdDetail.Cities.CityName = item.IsNull("CityName") ? string.Empty : item["CityName"].ToString();
                    getStdDetail.StudentAddress.PermanentAddress = item.IsNull("PermanentAddress") ? string.Empty : item["PermanentAddress"].ToString();
                    getStdDetail.StudentAddress.PresentAddress = item.IsNull("PresentAddress") ? string.Empty : item["PresentAddress"].ToString();
                    getStdDetail.GuardianDetail.GuardianName = item.IsNull("GuardianName") ? string.Empty : item["GuardianName"].ToString();
                    getStdDetail.GuardianContact.FirstContact = item.IsNull("GuardianContacts") ? string.Empty : item["GuardianContacts"].ToString();
                    getStdDetail.AdmissionGranted.AdmissionGrantedDate = Convert.ToDateTime(item["AdmissionGrantedDate"]);
                    getStdDetail.AdmissionGranted.AdmissionGrantedForClass = item.IsNull("AdmissionGrantedForClass") ? string.Empty : item["AdmissionGrantedForClass"].ToString();
                    getStdDetail.AdmissionGranted.AssessmentResult = item.IsNull("AssessmentResult") ? string.Empty : item["AssessmentResult"].ToString();
                    getStdDetail.AdmissionGranted.IsGranted = Convert.ToBoolean(item["IsGranted"]);
                    stdDetail.Add(getStdDetail);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return stdDetail;
        }

        public List<Teacher> GetTeacherReport()
        {
            var objExportFileDao = new ExportFileDAO(new SqlDatabase());
            DataTable dtDetail = objExportFileDao.GetALLTeachers();
            List<Teacher> tDetail = new List<Teacher>();
            try
            {

                foreach (DataRow item in dtDetail.Rows)
                {
                    Teacher getteacherDetail = new Teacher();
                    getteacherDetail.TeacherName = item.IsNull("TeacherName") ? string.Empty : item["TeacherName"].ToString();
                    getteacherDetail.CNIC = item.IsNull("CNIC") ? string.Empty : item["CNIC"].ToString();
                    getteacherDetail.LastQualification = item.IsNull("LastQualification") ? string.Empty : item["LastQualification"].ToString();
                    getteacherDetail.JoinDate = Convert.ToDateTime(item["JoinDate"]);
                    getteacherDetail.RefrenceName = item.IsNull("RefrenceName") ? string.Empty : item["RefrenceName"].ToString();
                    getteacherDetail.RefrenceContact = item.IsNull("RefrenceContact") ? string.Empty : item["RefrenceContact"].ToString();
                    getteacherDetail.LeaveDate = item.IsNull("LeaveDate") ? (DateTime?)null : Convert.ToDateTime(item["LeaveDate"]);
                    tDetail.Add(getteacherDetail);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return tDetail;

        }
        public List<TeacherAssignClass> GetALLTeachersAssignedClass()
        {
            var objExportFileDao = new ExportFileDAO(new SqlDatabase());
            DataTable dtDetail = objExportFileDao.GetALLTeacherAssignCourse();
            List<TeacherAssignClass> tDetail = new List<TeacherAssignClass>();
            try
            {

                foreach (DataRow item in dtDetail.Rows)
                {
                    TeacherAssignClass getteacherDetail = new TeacherAssignClass();
                    getteacherDetail.TeacherName = item.IsNull("TeacherName") ? string.Empty : item["TeacherName"].ToString();
                    getteacherDetail.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    tDetail.Add(getteacherDetail);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return tDetail;

        }

        public List<Course> GetALLCourse()
        {
            var objExportFileDao = new ExportFileDAO(new SqlDatabase());
            DataTable dtDetail = objExportFileDao.GetALLCourse();
            List<Course> cDetail = new List<Course>();
            try
            {

                foreach (DataRow item in dtDetail.Rows)
                {
                    Course getCourseDetail = new Course();

                    getCourseDetail.CourseCode = item.IsNull("CourseCode") ? string.Empty : item["CourseCode"].ToString();
                    getCourseDetail.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    getCourseDetail.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    cDetail.Add(getCourseDetail);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return cDetail;

        }

        public List<TeacherAssignedCourse> GetALLTeacherAssignCourse()
        {
            var objExportFileDao = new ExportFileDAO(new SqlDatabase());
            DataTable dtDetail = objExportFileDao.GetALLTeacherAssignCourse();
            List<TeacherAssignedCourse> cDetail = new List<TeacherAssignedCourse>();
            try
            {

                foreach (DataRow item in dtDetail.Rows)
                {
                    TeacherAssignedCourse getCourseDetail = new TeacherAssignedCourse();

                    getCourseDetail.TeacherName = item.IsNull("teachername") ? string.Empty : item["teachername"].ToString();
                    getCourseDetail.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    getCourseDetail.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    cDetail.Add(getCourseDetail);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return cDetail;
        }

        public List<StudentAssignedCourse> GetALLStudentAssignCourse()
        {
            var objExportFileDao = new ExportFileDAO(new SqlDatabase());
            DataTable dtDetail = objExportFileDao.GetALLStudentAssignCourse();
            List<StudentAssignedCourse> cDetail = new List<StudentAssignedCourse>();
            try
            {

                foreach (DataRow item in dtDetail.Rows)
                {
                    StudentAssignedCourse getCourseDetail = new StudentAssignedCourse();

                    getCourseDetail.StudentName = item.IsNull("StudentName") ? string.Empty : item["StudentName"].ToString();
                    getCourseDetail.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    getCourseDetail.ClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    cDetail.Add(getCourseDetail);
                }



            }
            catch (Exception)
            {

                throw;
            }

            return cDetail;
        } 

    }
}
