using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherAssessmentOperation
    {
        public TeacherAssessmentOperation()
        {
            TeacherAssessmentOperationId = 0;
            TeacherId = 0;
            AcadmicClassId = 0;
            CourseId = 0;
            ParentAssessmentId = 0;
            AssessmentSubTypeId = 0;
            AssementStatus = string.Empty;
            AverageConsequence = string.Empty;
            WorseConsequence = string.Empty;
            AssessmentFormat = false;
            CreatedById = string.Empty;
            CreateDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;

        }
        public int TeacherAssessmentOperationId { get; set; }
        public int TeacherId { get; set; }
        public int AcadmicClassId { get; set; }
        public int CourseId { get; set; }
        public int ParentAssessmentId { get; set; }
        public int AssessmentSubTypeId { get; set; }
        public string AssementStatus { get; set; }
        public string AverageConsequence { get; set; }
        public string WorseConsequence { get; set; }
        public bool AssessmentFormat { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }



    }
}
