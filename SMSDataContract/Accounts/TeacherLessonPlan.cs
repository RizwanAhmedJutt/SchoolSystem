using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherLessonPlan
    {
        public TeacherLessonPlan()
        {
            TeacherLessonPlanId = 0;
            AcadmicClassId = 0;
            TeacherId = 0;
            Lesson = string.Empty;
            Topic = string.Empty;
            SubTopic = string.Empty;
            Objective = string.Empty;
            OutComes = string.Empty;
            TeachingMethodology = string.Empty;
            ResourceRequired = string.Empty;
            CreatedById = string.Empty;
            ModifiedById = null;
            ModifiedDate = null;
        }
        public int TeacherLessonPlanId { get; set; }
        public int AcadmicClassId { get; set; }
        public string AcadmicClassName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Lesson { get; set; }
        public string Topic { get; set; }
        public string SubTopic { get; set; }
        [DataType(DataType.MultilineText)]
        public string Objective { get; set; }
        [DataType(DataType.MultilineText)]
        public string OutComes { get; set; }
        public string TeachingMethodology { get; set; }
        [DataType(DataType.MultilineText)]
        public string ResourceRequired { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedById { get; set; }

    }
}
