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
    public class RoomController : Controller
    {
        //
        // GET: /Room/
        IRoom roomRepo = new RoomBLL();
        IAssignRoom assignRepo = new AssignRoomBLL();
        public ActionResult GetALLRoom()
        {

            return View(roomRepo.GetALLRooms().ToList());
        }

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
        [HttpGet]
        public string CheckRoomAvailabilty(string[] CheckAttributes)
        {
            AssignRoom aroom = new AssignRoom
            {
                RoomId = Convert.ToInt32(CheckAttributes[0]),
                AcadmicClassId = Convert.ToInt32(CheckAttributes[1]),
                WeekDayId = Convert.ToInt32(CheckAttributes[2]),
                StartTime = CheckAttributes[3],
                EndTime = CheckAttributes[4]
            };
        
            aroom = assignRepo.GetRoomAssignedClassAvailablity(aroom);
            if(aroom.RAssignId>0)
                return new JavaScriptSerializer().Serialize(false);// Room is not Available for Class with selected time
              
            else
                return new JavaScriptSerializer().Serialize(true);   // Room is Available for Class with selected time
        }

    }

}