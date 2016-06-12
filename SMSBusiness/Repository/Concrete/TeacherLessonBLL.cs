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
    public class TeacherLessonBLL: ITeacherLesson
    {


        public int AddChangesLessonPlan(TeacherLessonPlan LessonPlan)
        {
            var objAssessmentDao = new TeacherLessonPlanDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objAssessmentDao.AddChangesLessonPlan(LessonPlan);
            }
            catch (Exception)
            {

                throw;
            }
            return ReturnValue;

        }

        public List<TeacherLessonPlan> GetTeacherLessons(int? AcadmicClassId, int? TeacherId, int? CourseId)
        {
            var objAssessmentDao = new TeacherLessonPlanDAO(new SqlDatabase());
            DataTable dt = objAssessmentDao.GetTeacherLessons(AcadmicClassId, TeacherId, CourseId);
            List<TeacherLessonPlan> lessons = new List<TeacherLessonPlan>();
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    var lessonPlan = new TeacherLessonPlan();
                    lessonPlan.TeacherLessonPlanId = item.IsNull("TeacherLessonPlanId") ? 0 : Convert.ToInt32(item["TeacherLessonPlanId"]);
                    lessonPlan.AcadmicClassName = item.IsNull("ClassName") ? string.Empty : item["ClassName"].ToString();
                    lessonPlan.TeacherName = item.IsNull("TeacherName") ? string.Empty : item["TeacherName"].ToString();
                    lessonPlan.CourseName = item.IsNull("CourseName") ? string.Empty : item["CourseName"].ToString();
                    lessonPlan.Lesson = item.IsNull("Lesson") ? string.Empty : item["Lesson"].ToString();
                    lessonPlan.Topic = item.IsNull("Topic") ? string.Empty : item["Topic"].ToString();
                    lessonPlan.SubTopic = item.IsNull("SubTopic") ? string.Empty : item["SubTopic"].ToString();
                    lessonPlan.Objective = item.IsNull("Objective") ? string.Empty : item["Objective"].ToString();
                    lessonPlan.OutComes = item.IsNull("OutComes") ? string.Empty : item["OutComes"].ToString();
                    lessonPlan.TeachingMethodology = item.IsNull("TeachingMethodology") ? string.Empty : item["TeachingMethodology"].ToString();
                    lessonPlan.ResourceRequired = item.IsNull("ResourceRequired") ? string.Empty : item["ResourceRequired"].ToString();
                    lessonPlan.CreateDate = Convert.ToDateTime(item["CreateDate"]);
                    lessons.Add(lessonPlan);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lessons;



        }

    }
}
