using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentExpenditure
    {
        public StudentExpenditure()
        {
            StdFeeId = 0;
            StudentId = 0;
            FeeMonth = string.Empty;
            AcadmicClassId = 0;
            ClassName = string.Empty;
            Tuition = 0;
            Transportation = 0;
            Books = 0;
            NoteBook = 0;
            Stationary = 0;
            Uniform = 0;
            Other = 0;
            CreateById = string.Empty;
            CreateDate = DateTime.Now;
            ModifiedById = null;
            ModifiedDate = null;
        }

        public int StdFeeId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string FeeMonth { get; set; }
        public int AcadmicClassId { get; set; }
        public string ClassName { get; set; }
        public int Tuition { get; set; }
        public int? Transportation { get; set; }
        public int? Books { get; set; }
        public int? NoteBook { get; set; }
        public int? Stationary { get; set; }
        public int? Uniform { get; set; }
        public int? Other { get; set; }
        public string CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
