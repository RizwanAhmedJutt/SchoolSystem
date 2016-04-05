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

        int RoomAssignAddChanges(AssignRoom assignRoom);
        AssignRoom GetCourseDetailByCourseId(int rAssignId);
        AssignRoom GetRoomAssignedClassAvailablity(AssignRoom aroom);

    }
}
