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
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string Religion { get; set; }
        public int AcadmicClassId { get; set; }
        public int NoOfSibling { get; set; }
        public int NoOfSiblingCurrentSchool { get; set; }
        public int RollNumber { get; set; }
        public string CNIC { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedById { get; set; }
    }



}
