using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Web.Script.Serialization;

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        ICourse courserepositry = new CourseBLL();
        IStudentAssignCourse stdAssignCourserepo = new StudentAssignCourseBLL();
        ITeacherAssignedCourse teacherrepo = new TeacherAssignCourseBLL();
        IExport exportfiles = new ExportFileBLL();
        [HttpGet]
        public ActionResult GetALLCourse(string SearchBy, string search, int? page)
        {
            List<Course> objCourses;
            if (SearchBy == "CourseCode" && search != "")
            {
                objCourses = courserepositry.GetALLCourseByCourseCode(search);
                return View(objCourses.ToList().ToPagedList(page ?? 1, 10));
            }
            else if (SearchBy == "NotActive" && search != "")
            {
                var query = (from x in courserepositry.GetALLCourse().ToList()
                             where x.IsActive == false && x.CourseName == search
                             select x).ToList();
                return View(query.ToPagedList(page ?? 1, 10));
            }
            else if (SearchBy == "NotActive" && string.IsNullOrEmpty(search))
            {
                var query = (from x in courserepositry.GetALLCourse().ToList()
                             where x.IsActive == false
                             select x).ToList();
                return View(query.ToPagedList(page ?? 1, 10));
            }
            else
                return View(courserepositry.GetALLCourse().ToPagedList(page ?? 1, 10));
        }
        [HttpPost]
        public ActionResult GetCourseReport()
        {
            GetAllCourseReport();
            return RedirectToAction("GetALLCourse");
        }
        [HttpGet]
        public ActionResult AddChangesCourse(int Id)
        {
            Course c;

            if (Id == 0)
            {
                Course course = new Course();

                return View(course);
            }
            else
            {

                c = courserepositry.GetCourseDetailByCourseId(Id);
                return View(c);
            }



        }
        [HttpPost]
        public ActionResult AddChangesCourse(Course c, int AcadmicClassId)
        {
            var userloggedId = User.Identity.GetUserId();
            if (c.CourseId > 0)
            {
                c.ModifiedById = userloggedId;
                c.ClassId = AcadmicClassId;
                c.ModifiedDate = DateTime.Now;

            }
            else
            {
                c.CreatedById = userloggedId;
                c.ClassId = AcadmicClassId;


            }

            int courseId = courserepositry.CourseAddChanges(c);


            return RedirectToAction("GetALLCourse", "Course");

        }
        [HttpGet]
        public ActionResult GetALLStudentAssignCourse(string SearchBy, string search, int? page)
        {
            if (SearchBy == "CourseName" && search != "")
            {
                List<StudentAssignedCourse> stdByName = stdAssignCourserepo.GetStudentAssignedCourseByCourseName(search);

                return View(stdByName.ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                List<StudentAssignedCourse> stdassignCourse = stdAssignCourserepo.GetStudentAssignedCourse();
                return View(stdassignCourse.ToList().ToPagedList(page ?? 1, 10));
            }

        }
        [HttpPost]
        public ActionResult GetStudentAssingCourseReport()
        {
            GetALLStudentAssignCourseReport();
            return RedirectToAction("GetALLStudentAssignCourse");
        }

        [HttpGet]
        public ActionResult AddChangesStudentAssignCourse(int id)
        {

            StudentAssignedCourse stdAssigncourse;
            if (id == 0)
            {
                stdAssigncourse = new StudentAssignedCourse();
                return View(stdAssigncourse);
            }
            {
                stdAssigncourse = stdAssignCourserepo.GetStudentAssignedCourseById(id);
                return View(stdAssigncourse);
            }
        }
        [HttpPost]
        public ActionResult AddChangesStudentAssignCourse(StudentAssignedCourse stdCourse)
        {
            var userloggedId = User.Identity.GetUserId();
            if (stdCourse.AssignCourseId > 0)
            {
                stdCourse.ModifiedById = userloggedId;
                stdCourse.ModifiedDate = DateTime.Now;
            }
            else
            {
                stdCourse.CreatedById = userloggedId;
                stdCourse.CreatedDate = DateTime.Now;
            }
            int getReturnValue = stdAssignCourserepo.InsertUpdateAssignedCourseAddChanges(stdCourse);
            return RedirectToAction("GetALLStudentAssignCourse", "Course");
        }
        [HttpGet]
        public ActionResult GetALLTeacherAssignCourse(string SearchBy, string search, int? page)
        {
            if (SearchBy == "CourseName" && search != "")
            {
                List<TeacherAssignedCourse> objTeacherAssignCourse = teacherrepo.GetTeacherAssignedCourseByCourseName(search);
                return View(objTeacherAssignCourse.ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                List<TeacherAssignedCourse> objTeacherAssignCourse = teacherrepo.GetTeacherAssignedCourse();
                return View(objTeacherAssignCourse.ToList().ToPagedList(page ?? 1, 10));
            }
        }
        [HttpPost]
        public ActionResult GetTeacherAssignCourseReport()
        {
            GetALLTeacherAssignCourseReport();
            return RedirectToAction("GetALLTeacherAssignCourse");
        }
        [HttpGet]
        public ActionResult AddChangesTeacherAssignCourse(int Id)
        {
            TeacherAssignedCourse assignCourse ;
            if (Id == 0)
            {
                assignCourse = new TeacherAssignedCourse();
                return View(assignCourse);
            }
            else
            {
                assignCourse = teacherrepo.GetTeacherAssignedCourseById(Id);
                return View(assignCourse);
            }
        }

        [HttpPost]
        public ActionResult AddChangesTeacherAssignCourse(TeacherAssignedCourse tAssignCourse)
        {
            var userloggedId = User.Identity.GetUserId();
            if(tAssignCourse.TeacherAssignedCourseId>0)
            {
                tAssignCourse.ModifiedById = userloggedId;
                tAssignCourse.ModifiedDate = DateTime.Now;
            }
            else
            {
                tAssignCourse.CreatedById = userloggedId;
                tAssignCourse.CreatedDate = DateTime.Now;
            }
           
            int getStatus = teacherrepo.InsertUpdateAssignedCourseAddChanges(tAssignCourse);

            return RedirectToAction("GetALLTeacherAssignCourse", "Course");

        }

        public ActionResult DeleteCourse(int CourseId)
        {
            var userloggedId = User.Identity.GetUserId();
            Course c = courserepositry.GetCourseDetailByCourseId(CourseId);
            c.IsActive = false;
            c.ModifiedById = userloggedId;
            c.ModifiedDate = DateTime.Now;
            int getStatus = courserepositry.DeleteCourse(c);
            return RedirectToAction("GetALLCourse", "Course");
        }

        public ActionResult DDLCourse(int AcadmicClassId)
        {
            try
            {
                if (AcadmicClassId > 0)
                {
                    ViewData["DDLCourse"] = new SelectList(courserepositry.GetALLCourseByAcadmicClassId(AcadmicClassId).OrderBy(c => c.CourseId).ToList(), "CourseId", "CourseName");
                    return View("../DropDownLists/DDLCourse");
                }
                else
                {
                    List<Course> courses = new List<Course>();
                    var query = (from x in courserepositry.GetALLCourse().ToList()
                                 where x.IsActive == true
                                 select x).FirstOrDefault();
                    courses.Add(query);
                    ViewData["DDLCourse"] = new SelectList(courses.ToList(), "CourseId", "CourseName");
                    return View("../DropDownLists/DDLCourse");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult DDLStudent(int AcadmicClassId)
        {
            if (AcadmicClassId > 0)
            {
                IStudent std = new StudentBLL();
                ViewData["DDLStudent"] = new SelectList(std.GetALLStudentByClass(AcadmicClassId).OrderBy(c => c.StudentId).ToList(), "StudentId", "StudentName");
                return View("../DropDownLists/DDLStudent");
            }
            else
            {
                IStudent std = new StudentBLL();
                List<Student> students = new List<Student>();
                var getStudent = std.GetAllStudentByName().Where(x => x.IsActive == true).FirstOrDefault();
                students.Add(getStudent);
                ViewData["DDLStudent"] = new SelectList(students.ToList(), "StudentId", "StudentName");
                return View("../DropDownLists/DDLStudent");
            }

        }

        public void GetAllCourseReport()
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Course");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Course Code";
                    ws.Cells[rowNo, 9].Value = "Course Name";
                    ws.Cells[rowNo, 10].Value = "Class Name";
                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<Course> teacherDetail = exportfiles.GetALLCourse();
                foreach (var item in teacherDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.CourseCode;
                    ws.Cells[rowNo, 9].Value = item.CourseName;
                    ws.Cells[rowNo, 10].Value = item.ClassName;

                }
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=SiteProductivity.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }

        }
        public void GetALLTeacherAssignCourseReport()
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Teacher Assigned Course");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Teacher Name";
                    ws.Cells[rowNo, 9].Value = "Course Name";
                    ws.Cells[rowNo, 10].Value = "Class Name";
                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<TeacherAssignedCourse> teacherDetail = exportfiles.GetALLTeacherAssignCourse();
                foreach (var item in teacherDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.TeacherName;
                    ws.Cells[rowNo, 9].Value = item.CourseName;
                    ws.Cells[rowNo, 10].Value = item.ClassName;

                }
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=TeacherAssignCourse.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }

        }
        public void GetALLStudentAssignCourseReport()
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Student Assigned Course ");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Student Name";
                    ws.Cells[rowNo, 9].Value = "Course Name";
                    ws.Cells[rowNo, 10].Value = "Class Name";
                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<StudentAssignedCourse> teacherDetail = exportfiles.GetALLStudentAssignCourse();
                foreach (var item in teacherDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.StudentName;
                    ws.Cells[rowNo, 9].Value = item.CourseName;
                    ws.Cells[rowNo, 10].Value = item.ClassName;

                }
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=StudentAssignCourse.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }

        }

        public string IsCourseNameExist(string CName, int AcadmicClassId)
        {
            List<Course> lst = courserepositry.GetALLCourse();
            var query = (from c in lst
                         where c.CourseName == CName && c.ClassId == AcadmicClassId
                         select c);

            if (query.Any())
                return new JavaScriptSerializer().Serialize(true);// Course   already Assign That Class
            else
                return new JavaScriptSerializer().Serialize(false);   // Course  not already Assign That Class

        }
        public string IsModifiedCourseNameExist(string CName, int AcadmicClassId, string CourseCode, bool Active)
        {

            List<Course> lst = courserepositry.GetALLCourse();
            var query = (from c in lst
                         where c.CourseName == CName && c.ClassId == AcadmicClassId && CourseCode == c.CourseCode && c.IsActive == Active
                         select c);

            if (query.Any())
                return new JavaScriptSerializer().Serialize(true);// Course   already Assign That Class
            else
                return new JavaScriptSerializer().Serialize(false);   // Course  not already Assign That Class

        }

        public string IsCourseAlreadyAssignedToTeacher(int AcadmicClassId, int CourseId, int TeacherId)
        {
            List<TeacherAssignedCourse> tCourse = teacherrepo.GetTeacherAssignedCourse();
            var query = (from t in tCourse
                         where t.CourseId == CourseId && t.TeacherId == TeacherId && t.AcadmicClassId == AcadmicClassId
                         select t).ToList();
            if (query.Any())
                return new JavaScriptSerializer().Serialize(true);// Course   already Assign That Teacher
            else
                return new JavaScriptSerializer().Serialize(false);   // Course  not already Assign That Teacher
        }
        public string IsCourseAlreadyAssignedToStudent(int AcadmicClassId, int CourseId, int StudentId)
        {
            List<StudentAssignedCourse> sCourse = stdAssignCourserepo.GetStudentAssignedCourse();
            var query = (from s in sCourse
                         where s.CourseId == CourseId && s.StudentId == StudentId && s.AcadmicClassId == AcadmicClassId
                         select s).ToList();
            if (query.Any())
                return new JavaScriptSerializer().Serialize(true);// Course   already Assign That Class
            else
                return new JavaScriptSerializer().Serialize(false);   // Course  not already Assign That Class
        }
    }
}