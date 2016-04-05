using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class RoomController : Controller
    {
        //
        // GET: /Room/
        IRoom roomRepo = new RoomBLL();
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
            return View();
        }
        
        public ActionResult AddChangesRoomAssignClass()
        {
            return View();
        }
    }
}