using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
    public interface IAssignRoom
    {
        List<AssignRoom> GetALLRoomAssignedClass();
        List<AssignRoom> GetALLRoomAssignedClassByRoomName(string RoomName);
        int RoomAssignAddChanges(AssignRoom assignRoom);
        AssignRoom GetRoomAssignedDetailId(int rAssignId);
        AssignRoom GetRoomAssignedClassAvailablity(AssignRoom aroom);

    }
    public interface IRoom
    {
        List<Room> GetALLRooms();
        int AddChangesRoom(Room r);
        Room GetRoomById(int Id);


    }
}
