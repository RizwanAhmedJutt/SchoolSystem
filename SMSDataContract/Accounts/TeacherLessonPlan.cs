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
            CourseId = 0;
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
        [Required(ErrorMessage="Please Select Course")]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        [Required(ErrorMessage="Please Enter Lesson Name")]
        public string Lesson { get; set; }
         [Required(ErrorMessage = "Please Enter Topic")]
        public string Topic { get; set; }
        public string SubTopic { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please Enter Objective")]
        public string Objective { get; set; }
         [Required(ErrorMessage = "Please Enter Out Comes")]
        [DataType(DataType.MultilineText)]
        public string OutComes { get; set; }
         [Required(ErrorMessage = "Please Enter Teach Methodolgy")]
        public string TeachingMethodology { get; set; }
         [Required(ErrorMessage = "Please Enter Resource")]
        [DataType(DataType.MultilineText)]
        public string ResourceRequired { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedById { get; set; }

    }
}
