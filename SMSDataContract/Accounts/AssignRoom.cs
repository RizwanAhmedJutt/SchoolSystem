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
        public int CourseId { get; set; }
        [Display(Name ="Course Name")]
        [Required(ErrorMessage ="Please Select Course")]
        public string CourseName { get; set; }
      
        [Display(Name=" Check Available")]
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
        [Display(Name="Room Name")]
        [Required(ErrorMessage="Please Enter Room")]
        public string RoomName { get; set; }


    } 
}
