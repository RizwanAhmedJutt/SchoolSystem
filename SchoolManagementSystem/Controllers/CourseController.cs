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

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        ICourse courserepositry = new CourseBLL();
        IStudentAssignCourse stdAssignCourserepo = new StudentAssignCourseBLL();
        ITeacherAssignedCourse teacherrepo = new TeacherAssignCourseBLL();
        IExport exportfiles;
        [HttpGet]
        public ActionResult GetALLCourse(string SearchBy, string search, int? page)
        {
            if (SearchBy == "CourseCode" && search != "")
            {
                List<Course> objCourses = courserepositry.GetALLCourseByCourseCode(search);
                return View(objCourses.ToList().ToPagedList(page ?? 1, 10));
            }
            else
                return View(courserepositry.GetALLCourse().ToPagedList(page ?? 1, 10));
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
        [HttpGet]
        public ActionResult AddChangesStudentAssignCourse(int id)
        {
            var userloggedId = User.Identity.GetUserId();
            StudentAssignedCourse stdAssignCourse;
            stdAssignCourse = stdAssignCourserepo.GetStudentAssignedCourseById(id);
            if (id == 0)
            {
                StudentAssignedCourse stdAssigncourse = new StudentAssignedCourse();
                stdAssignCourse.CreatedById = userloggedId;

                return View(stdAssigncourse);
            }
            {
                stdAssignCourse.ModifiedById = userloggedId;
                stdAssignCourse.ModifiedDate = DateTime.Now;
                return View(stdAssignCourse);
            }
        }
        [HttpPost]
        public ActionResult AddChangesStudentAssignCourse(StudentAssignedCourse stdCourse, int CourseId, int StudentId, int AcadmicClassId)
        {
            stdCourse.CourseId = CourseId;
            stdCourse.StudentId = StudentId;
            stdCourse.AcadmicClassId = AcadmicClassId;
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
        [HttpGet]
        public ActionResult AddChangesTeacherAssignCourse(int Id)
        {
            TeacherAssignedCourse assignCourse = teacherrepo.GetTeacherAssignedCourseById(Id);
            if (assignCourse.TeacherAssignedCourseId == 0)
            {
                TeacherAssignedCourse tassignCourse = new TeacherAssignedCourse();
                return View(tassignCourse);
            }
            else
            {
                return View(assignCourse);
            }
        }
        [HttpPost]
        public ActionResult AddChangesTeacherAssignCourse(TeacherAssignedCourse tAssignCourse, int CourseId, int TeacherId, int AcadmicClassId)
        {
            var userloggedId = User.Identity.GetUserId();
            tAssignCourse.TeacherId = TeacherId;
            tAssignCourse.CourseId = CourseId;
            tAssignCourse.ClassId = AcadmicClassId;
            tAssignCourse.CreatedById = userloggedId;
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
                    ViewData["DDLCourse"] = new SelectList(courserepositry.GetALLCourse().OrderBy(c => c.CourseId).ToList(), "CourseId", "CourseName");
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
                ViewData["DDLStudent"] = new SelectList(std.GetAllStudentByName().OrderBy(c => c.StudentId).ToList(), "StudentId", "StudentName");
                return View("../DropDownLists/DDLStudent");
            }

        }

        public void GetAllCourse()
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
        public void GetALLTeacherAssignCourse()
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
                Response.AddHeader("content-disposition", "attachment;  filename=SiteProductivity.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }

        }
        public void GetALLStudentAssignCourse()
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
                Response.AddHeader("content-disposition", "attachment;  filename=SiteProductivity.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }

        }
    }
}