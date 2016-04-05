using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class AssignRoom
    {
        public AssignRoom()
        {
            RAssignId = 0;
            RoomId = 0;
            AcadmicClassId = 0;
            WeekDayId = 0;
            StartTime = TimeSpan.Zero;
            EndTime = TimeSpan.Zero;
            IsAvailable = false;
            CreatedById = string.Empty;
            CreatedDate = DateTime.Now;
            ModifiedById = string.Empty;
            ModifiedDate = null;
        }

        public int RAssignId { get; set; }
        [Display(Name="Room")]
        [Required(ErrorMessage="Select Room")]
        public int RoomId { get; set; }
        [Display(Name="Acadmic Class")]
        [Required(ErrorMessage="Select Acadmic Class")]
        public int AcadmicClassId { get; set; }
        [Display(Name="Week Day")]
        [Required(ErrorMessage="Select Week Day")]
        public int WeekDayId { get; set; }
        [Display(Name="Start Time")]
        [Required(ErrorMessage="Start Time Is Required")]
        public TimeSpan StartTime { get; set; }
        [Display(Name="End Time")]
        [Required(ErrorMessage="End Time Is Required")]
        public TimeSpan EndTime { get; set; }
        [Display(Name="Available")]
        public bool IsAvailable { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ClassName { get; set; }
        public string DayName { get; set; }
        public string RoomName { get; set; }


    }
    public class Room
    {
        

        public Room()
        {
            RoomId = 0;
            RoomName = string.Empty;

        }
        public int RoomId { get; set; }
        public string RoomName { get; set; }


    } 
}
