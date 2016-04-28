using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSDataContract.Common;

namespace SMSDataContract.Accounts
{
    public class DailyAssessmentSubType
    {

        public DailyAssessmentSubType()
        {
            OperationalId = 0;
            AssessmentSubTypeId = 0;
            AssessmentSubTypeName = string.Empty;
            Concequence = string.Empty;
            AssessmentFormat = false;
            CreatedById = string.Empty;
            CreateDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;


        }
        public int OperationalId { get; set; }
        public int AssessmentSubTypeId { get; set; }

        [Required(ErrorMessage = "Select Assessment Type")]
        public int AssessmentTypeId { get; set; }
        [Display(Name = "Sub Assessment")]
        [Required(ErrorMessage = "Enter Sub Assessment Name")]
        public string AssessmentSubTypeName { get; set; }
        [Display(Name = "Parent Assessment")]
        public string ParentAssementName { get; set; }
        [Display(Name = "Assessment Format")]
        public bool AssessmentFormat { get; set; }
        public int AcadmicClassId { get; set; }
        [Display(Name = "Acadmic Class")]
        public string AcadmicClassName { get; set; }
        public int StudentId { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        public int TeacherId { get; set; }
        [Display(Name="Teacher Name")]
        public string TeacherName { get; set; }
        [Display(Name = "Evaluation")]
        public string SelectedEvaluation { get; set; }
        public string Concequence { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string AverageConcequence { get; set; }

        public string CreatedById { get; set; }
        [Display(Name="Create Date")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Create Date")]
        public string FormateCreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
