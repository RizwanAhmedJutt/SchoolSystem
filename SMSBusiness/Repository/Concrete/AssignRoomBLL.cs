using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
  public  class AssignRoomBLL
    {

      public int RoomAssignAddChanges(AssignRoom assignRoom)
      {
          var objAssignRoomDao = new AssignRoomDAO(new SqlDatabase());
          int ReturnValue = 0;  // Value will be 99 in case of Update
          try
          {
              ReturnValue = objAssignRoomDao.InsertUpdateAssignRoom(assignRoom);
          }
          catch (Exception)
          {

              throw;
          }

          return ReturnValue;
      }

    }
}
