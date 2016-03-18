using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
    public class Student
    {
        public Student()
        {
            StudentId = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            DOB = DateTime.Now;
            Religion= string.Empty;
            StudentName = string.Empty;
            AcadmicClassId = 0;
            NoOfSibling = 0;
            NoOfSiblingCurrentSchool = 0;
            RollNumber = 0;
            IsActive = false;
            CreateDate = DateTime.Now;
            CreatedById = string.Empty;
            ModifiedDate = null;
            ModifiedById = null;
            
        }
        

        public int StudentId { get; set; }
        [Required (ErrorMessage="Please Enter First Name")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required (ErrorMessage="Enter Last Name")]
        [Display(Name="Last Name")]
        public string LastName { get; set; }
        [Display(Name="Class Name")]
        public string ClassName { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name="Date Of Birth")]
        //[DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }
        [Required( ErrorMessage= "Please Enter Religion")]
        public string Religion { get; set; }
        [Display(Name="Acadmic Class")]
        public int AcadmicClassId { get; set; }
        [Display(Name="NO. Sibling")]
        public int NoOfSibling { get; set; }
        [Display(Name="Sibling in Current School")]
        public int NoOfSiblingCurrentSchool { get; set; }
        [Display(Name="Roll No.")]
        public int RollNumber { get; set; }
         [Required (ErrorMessage="Please Enter CNIC")]
        [Display(Name="CNIC Card No.")]
        public string CNIC { get; set; }
        [Display(Name="Student Name")]
         public string StudentName { get; set; }
        [Display(Name="Is Active")]
        public bool IsActive { get; set; }
        [Display(Name="Create Date")]
        public DateTime CreateDate { get; set; }
        [Display(Name="Created By Id")]
        public string CreatedById { get; set; }
        [Display(Name="Modified Date")]
        public DateTime? ModifiedDate { get; set; }
        [Display(Name="Modified By Id")]
        public string ModifiedById { get; set; }
    }



}
