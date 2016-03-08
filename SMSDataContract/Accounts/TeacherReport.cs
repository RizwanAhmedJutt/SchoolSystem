using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class TeacherReport
    {
        public int TeacherReportId { get; set; }
        public int TeacherId { get; set; }
        public int ReportTypeId { get; set; }
        public DateTime ReportingMonth { get; set; }
        public float AttendancePercent { get; set; }
        public string LessonPlanning { get; set; }
        public string InstructionlMethod { get; set; }
        public string ClassRoomManagement { get; set; }
        public string TimeManagement { get; set; }
        public string SpecialContribution { get; set; }
        public int TotalReward { get; set; }
        public int Ranking { get; set; }
        public string Remarks { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
