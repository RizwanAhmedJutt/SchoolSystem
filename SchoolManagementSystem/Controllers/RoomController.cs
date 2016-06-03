using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using System.Web.Script.Serialization;


namespace SchoolManagementSystem.Controllers
{
     [Authorize(Roles = "Admin")]
    public class RoomController : Controller
    {
        //
        // GET: /Room/
        IRoom roomRepo = new RoomBLL();
        IAssignRoom assignRepo = new AssignRoomBLL();
        IPeriodAssigned periodRepo = new PeriodAssignedBLL();
        public ActionResult GetALLRoom(string SearchBy, string search, int? page)
        {

            return View(roomRepo.GetALLRooms().ToPagedList(page ?? 1, 10));
        }
        [HttpGet]
        public ActionResult AddChangesRoom(int Id)
        {
            Room r = roomRepo.GetRoomById(Id);
            if (Id == 0)
            {
                Room room = new Room();
                return View(room);
            }
            else
                return View(r);
        }
        [HttpPost]
        public ActionResult AddChangesRoom(Room r)
        {
           int getStatus = roomRepo.AddChangesRoom(r);
            return RedirectToAction("GetALLRoom");
        }
        //Manage Room Assign Class
        [HttpGet]
        public ActionResult GetALLRoomAssignedClass(string SearchBy, string search, int? page)
        {
            if (SearchBy == "RoomName" && search != "")
            {
                List<AssignRoom> objAssignRoom = assignRepo.GetALLRoomAssignedClassByRoomName(search);
                return View(objAssignRoom.ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                List<AssignRoom> objAssignedRoom = assignRepo.GetALLRoomAssignedClass();
                return View(objAssignedRoom.ToList().ToPagedList(page ?? 1, 10));
            }
        }

        [HttpGet]
        public ActionResult AddChangesRoomAssignClass(int Id)
        {
            AssignRoom asroom = assignRepo.GetRoomAssignedDetailId(Id);
            if (asroom.RAssignId == 0)
            {
                AssignRoom aroom = new AssignRoom();
                return View(aroom);
            }
            else
                return View(asroom);
        }
        [HttpPost]
        public ActionResult AddChangesRoomAssignClass(AssignRoom assignRoom)
        {
            var userloggedId = User.Identity.GetUserId();
            if (assignRoom.RAssignId == 0)
            { assignRoom.CreatedById = userloggedId; }
            else
            {
                assignRoom.ModifiedById = userloggedId;
                assignRoom.ModifiedDate = DateTime.Now;
            }
            int getStatus = assignRepo.RoomAssignAddChanges(assignRoom);

            return RedirectToAction("GetALLRoomAssignedClass");
        }
        //Manage Class Assigned Period
        public ActionResult GetALLAssignedPeriods(string SearchBy, string search, int? page)
        {
            if(SearchBy == "AcadmicClass" && search != "")
            {
                List<PeriodAssigned> objAssignPeriod = periodRepo.PeroidAssignedByAcadmicClass(Convert.ToInt32(search));
                return View(objAssignPeriod.ToList().ToPagedList(page ?? 1, 10));
            }
            else
            {
                List<PeriodAssigned> objAssignedPeriod = periodRepo.GetALLAssignedPeriods();
                return View(objAssignedPeriod.ToList().ToPagedList(page ?? 1, 10));
            }
         
        }
        [HttpGet]
        public ActionResult AddChangesPeriods(int Id)
        {
            if (Id == 0)
            {
                PeriodAssigned pAssigned = new PeriodAssigned();
                return View(pAssigned);
            }
            else
            {
                PeriodAssigned periodAssigned = periodRepo.GetAssignedPeriodById(Id);
                return View(periodAssigned);
            }
        }
        [HttpPost]
        public ActionResult AddChangesPeriods(PeriodAssigned pAssigned)
        {
            int getStatus = periodRepo.AddChangesPeriods(pAssigned);
           return RedirectToAction("GetALLAssignedPeriods");
        }

        [HttpGet]
        public string CheckRoomAvailabilty(string[] CheckAttributes)
        {
            AssignRoom aroom = new AssignRoom
            {
                RoomId = Convert.ToInt32(CheckAttributes[0]),
                AcadmicClassId = Convert.ToInt32(CheckAttributes[1]),
                WeekDayId = Convert.ToInt32(CheckAttributes[2]),
               CourseId=Convert.ToInt32(CheckAttributes[3])
            };
        
            aroom = assignRepo.GetRoomAssignedClassAvailablity(aroom);
            if(aroom.RAssignId>0)
                return new JavaScriptSerializer().Serialize(false);// Room is not Available for Class with selected time
              
            else
                return new JavaScriptSerializer().Serialize(true);   // Room is Available for Class with selected time
        } 

        public string  CheckRoomNameExist(string RoomName)
        {
            Room room = roomRepo.CheckRoomNameExist(RoomName);
            if(room.RoomId>0)
                return new JavaScriptSerializer().Serialize(true);// Room Name  already Exist
            else
                return new JavaScriptSerializer().Serialize(false);   // Room name not already Exist

        }
        public string CheckAlreadyPeriodAssigned(string[] CheckAttributes)
        {
            PeriodAssigned pAssigned = new PeriodAssigned
            {
                PeriodNumber = Convert.ToInt32(CheckAttributes[0]),
                AcadmicClassId = Convert.ToInt32(CheckAttributes[1]),
                CourseId = Convert.ToInt32(CheckAttributes[2])
            };
            pAssigned = periodRepo.CheckAlreadyPeriodAssigned(pAssigned.PeriodNumber, pAssigned.AcadmicClassId, pAssigned.CourseId);
            if (pAssigned.PeriodAssignedId > 0)
                return new JavaScriptSerializer().Serialize(false);

            else
                return new JavaScriptSerializer().Serialize(true); 
        }
    }

}