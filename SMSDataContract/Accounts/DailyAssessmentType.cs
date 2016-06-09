using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class DailyAssessmentType
    {
        public DailyAssessmentType()
        {
            AssessmentTypeId = 0;
            AssessmentName = string.Empty;
            CreatedById = string.Empty;
            CreateDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;


        }
        public int OperationalId { get; set; }
        [Display(Name="Assessment Id")]
        public int AssessmentTypeId { get; set; }
        [Display(Name = "Assessment Name")]
        [Required(ErrorMessage="Please Enter Assessment Name")]
        public string AssessmentName { get; set; }
        [Display(Name="Assessment Category")]
        public string AssementCategory { get; set; }
        public int AssessmentCategoryId { get; set; }
        [Display(Name="Assessment Criteria")]
        [Required(ErrorMessage="Please Select Criteria")]
        public string AssessmentCriteria { get; set; }

        [Display(Name = "Assessment Format")]
        public bool AssessmentFormat { get; set; }

        [Display(Name = "Evaluation")]
        public string SelectedEvaluation { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string AverageConcequence { get; set; }
         public int AcadmicClassId { get; set; }
        [Display(Name = "Acadmic Class")]
        public string AcadmicClassName { get; set; }
        public int StudentId { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        public int TeacherId { get; set; }
        [Display(Name="Teacher Name")]
        public string TeacherName { get; set; }
        public string Concequence { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }




    }
}
