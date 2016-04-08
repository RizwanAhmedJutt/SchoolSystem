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
        public ActionResult AddChangesRoomAssignClass()
        {
            
            AssignRoom aroom = new AssignRoom();
            return View(aroom);
        }
        [HttpPost]
        public ActionResult AddChangesRoomAssignClass(AssignRoom assignRoom)
        {
            var userloggedId = User.Identity.GetUserId();
            assignRoom.CreatedById = userloggedId;

           int getStatus = assignRepo.RoomAssignAddChanges(assignRoom);

            return View();
        }

      
    }

}