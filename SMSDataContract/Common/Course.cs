using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
    public class Course
    {
        public Course()
        {
            CourseId = 0;
            CourseCode = string.Empty;
            ClassId = 0;
            IsActive = false;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;
        }
        public int CourseId { get; set; }
        [Display(Name="Course Code")]
        [Required(ErrorMessage="Enter Course Code")]

        public string CourseCode { get; set; }

        [Display(Name="Course Name")]
    
        [Required(ErrorMessage="Enter Course Name")]
        public string CourseName { get; set; }
        [Display(Name="Class")]
        [Required(ErrorMessage="Select a class")]
        public int ClassId { get; set; }
        [Display(Name="Is Active")]
        public bool IsActive { get; set; }
        [Display(Name="Class Name")]
        public string ClassName { get; set; }
        [Display(Name="Created By Id")]
        public string CreatedById { get; set; }
        [Display(Name="Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name="Updated By Id")]
        public string ModifiedById { get; set; }
        [Display(Name="Updated Date")]
        public DateTime? ModifiedDate { get; set; }

    }
}
