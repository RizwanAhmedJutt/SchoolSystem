using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class StudentBLL:IStudent
    {

        //public List<Student> GetALLStudents()
        //{
        //    var objStudentDao = new StudentDAO(new SqlDatabase());
        //    DataTable stdDetail;
        //    List<Student> studentList = new List<Student>();
        //    Student std = new Student();
        //    try
        //    {
        //        stdDetail = objStudentDao.GetALLStudents();
        //        if (stdDetail.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in stdDetail.Rows)
        //            {
        //                std.StudentId = int.Parse(item["StudentId"].ToString());
        //                std.RollNumber = Convert.ToInt32(item["RollNumber"]);

        //                std.FirstName = item["FirstName"].ToString();
        //                std.LastName = item["LastName"].ToString();
        //                std.DOB = Convert.ToDateTime(item["DateOfBirth"].ToString());

        //                std.CNIC = item["CNIC"].ToString();
        //                std.Religion = item["Religion"].ToString();
        //                std.AcadmicClassId = int.Parse(item["AcadmicClassId"].ToString());
                      
        //                std.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
        //                std.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //    return studentList;


        //}


        public List<Student> GetAllStudents()
        {
            var objStudDao = new StudentDAO(new SqlDatabase());
            var dtStudents = new DataTable();
            dtStudents = objStudDao.GetALLStudents();
            List<Student> objStdList = new List<Student>();

            try
            {

                foreach (DataRow dr in dtStudents.Rows)
                {
                    var StdDetails = new Student();
                    StdDetails.StudentId = dr.IsNull("StudentId") ? 0 : Convert.ToInt32(dr["StudentId"]);
                    StdDetails.FirstName = dr.IsNull("FirstName") ? string.Empty : Convert.ToString(dr["FirstName"]);
                    StdDetails.LastName = dr.IsNull("LastName") ? string.Empty : Convert.ToString(dr["LastName"]);
                    StdDetails.CNIC = dr.IsNull("CNIC") ? string.Empty : Convert.ToString(dr["CNIC"]);
                    StdDetails.DOB = dr.IsNull("DateOfBirth") ? DateTime.Now : Convert.ToDateTime(dr["DateOfBirth"]);
                   
                    
                    StdDetails.CreateDate = Convert.ToDateTime(dr["CreatedDate"]);
                     StdDetails.ClassName = dr.IsNull("ClassName") ? string.Empty : dr["ClassName"].ToString();
                    StdDetails.Religion = dr.IsNull("Religion") ? string.Empty : Convert.ToString(dr["Religion"]);
                    StdDetails.RollNumber = dr.IsNull("RollNumber") ? 0 : Convert.ToInt32(dr["RollNumber"]);
                    StdDetails.IsActive = dr.IsNull("IsActive") ? true : Convert.ToBoolean(dr["IsActive"]);
                    objStdList.Add(StdDetails);
                }

                return objStdList;

            }
            catch
            {

                throw;
            }
        }
        public List<Student> GetALLDisActiveStudents()
        {
            var objStudDao = new StudentDAO(new SqlDatabase());
            var dtStudents = new DataTable();
            dtStudents = objStudDao.GetALLDisActiveStudents();
            List<Student> objStdList = new List<Student>();

            try
            {

                foreach (DataRow dr in dtStudents.Rows)
                {
                    var StdDetails = new Student();
                    StdDetails.StudentId = dr.IsNull("StudentId") ? 0 : Convert.ToInt32(dr["StudentId"]);
                    StdDetails.FirstName = dr.IsNull("FirstName") ? string.Empty : Convert.ToString(dr["FirstName"]);
                    StdDetails.LastName = dr.IsNull("LastName") ? string.Empty : Convert.ToString(dr["LastName"]);
                    StdDetails.CNIC = dr.IsNull("CNIC") ? string.Empty : Convert.ToString(dr["CNIC"]);
                    StdDetails.DOB = dr.IsNull("DateOfBirth") ? DateTime.Now : Convert.ToDateTime(dr["DateOfBirth"]);


                    StdDetails.CreateDate = Convert.ToDateTime(dr["CreatedDate"]);
                    StdDetails.ClassName = dr.IsNull("ClassName") ? string.Empty : dr["ClassName"].ToString();
                    StdDetails.Religion = dr.IsNull("Religion") ? string.Empty : Convert.ToString(dr["Religion"]);
                    StdDetails.RollNumber = dr.IsNull("RollNumber") ? 0 : Convert.ToInt32(dr["RollNumber"]);
                    StdDetails.IsActive = dr.IsNull("IsActive") ? true : Convert.ToBoolean(dr["IsActive"]);
                    objStdList.Add(StdDetails);
                }

                return objStdList;

            }
            catch
            {

                throw;
            }
        }

        public List<Student> GetAllStudentByName()
        {
            var objStudDao = new StudentDAO(new SqlDatabase());
            var dtStudents = new DataTable();
            dtStudents = objStudDao.GetALLStudentByNames();
            List<Student> objStdList = new List<Student>();

            try
            {

                foreach (DataRow dr in dtStudents.Rows)
                {
                    var StdDetails = new Student();
                    StdDetails.StudentId = dr.IsNull("StudentId") ? 0 : Convert.ToInt32(dr["StudentId"]);
                    StdDetails.FirstName = dr.IsNull("FirstName") ? string.Empty : Convert.ToString(dr["FirstName"]);
                    StdDetails.LastName = dr.IsNull("LastName") ? string.Empty : Convert.ToString(dr["LastName"]);
                    StdDetails.StudentName = dr.IsNull("StudentName") ? string.Empty : Convert.ToString(dr["StudentName"]);
                    objStdList.Add(StdDetails);
                }

                return objStdList;

            }
            catch
            {

                throw;
            }
            

        }  
        public List<Student> GetALLStudentByClass(int AcadmicClassId)
        {
            var objStudDao = new StudentDAO(new SqlDatabase());
            var dtStudents = new DataTable();
            dtStudents = objStudDao.GetALLStudentByClass(AcadmicClassId);
            List<Student> objStdList = new List<Student>();

            try
            {

                foreach (DataRow dr in dtStudents.Rows)
                {
                    var StdDetails = new Student();
                    StdDetails.StudentId = dr.IsNull("StudentId") ? 0 : Convert.ToInt32(dr["StudentId"]);
                    StdDetails.StudentName = dr.IsNull("StudentName") ? string.Empty : Convert.ToString(dr["StudentName"]);
                    objStdList.Add(StdDetails);
                }

                return objStdList;

            }
            catch
            {

                throw;
            }
        }
        public Student GetStudentById(int StudentId)
        {
            var objStudentDao = new StudentDAO(new SqlDatabase());
            DataTable stdDetail;
            Student std = new Student();
            try
            {
                stdDetail = objStudentDao.GetStudentInfoByStudentId(StudentId);
                if (stdDetail.Rows.Count > 0)
                {
                    foreach (DataRow item in stdDetail.Rows)
                    {
                        std.StudentId = Convert.ToInt32(item["StudentId"]);
                        std.FirstName = item["FirstName"].ToString();
                        std.LastName = item["LastName"].ToString();
                        std.DOB = Convert.ToDateTime(item["DOB"].ToString());
                        std.CNIC = item["CNIC"].ToString();
                        std.NoOfSibling = int.Parse(item["NoOfSibling"].ToString());
                        std.NoOfSiblingCurrentSchool = Convert.ToInt32(item["NoOfSiblingCurrentSchool"]);
                        std.Religion = item["Religion"].ToString();
                        std.AcadmicClassId = int.Parse(item["AcadmicClassId"].ToString());
                        std.RollNumber = item.IsNull("RollNumber") ?0: Convert.ToInt32(item["RollNumber"]);
                        std.IsActive = Convert.ToBoolean(item["IsActive"].ToString());
                        std.CreateDate = Convert.ToDateTime(item["CreatedDate"]);
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return std;



        }

        public int StudentAddChanges(Student student)
        {
            var objStudentDao = new StudentDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                
                ReturnValue = objStudentDao.InsertUpdateStudent(student);
            }
            catch (Exception)
            {
                
                throw;
            }

            return ReturnValue;
        }

        public int DeleteStudent(Student student)
        {
            var objStudentDao = new StudentDAO(new SqlDatabase());
            int ReturnValue = 0;
            try
            {
                ReturnValue = objStudentDao.DeleteStudent(student);


            }
            catch (Exception)
            {
                
                throw;
            }


            return ReturnValue;
        }

    }
}
