using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
  public  class AssignRoomBLL:IAssignRoom
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
      public AssignRoom GetCourseDetailByCourseId(int rAssignId)
      {
          var objAssignRoomDao = new AssignRoomDAO(new SqlDatabase());
          DataTable tblRoom;
          AssignRoom aroom = new AssignRoom();
          try
          {
              tblRoom = objAssignRoomDao.GetRoomAssignClassDetailByID(rAssignId);
              if (tblRoom.Rows.Count > 0)
              {
                  foreach (DataRow item in tblRoom.Rows)
                  {
                      aroom.RAssignId = Convert.ToInt32(item["RAssignId"]);
                      aroom.RoomId = Convert.ToInt32(item["RoomId"]);
                      aroom.RoomName = item["RoomName"].ToString();
                      aroom.AcadmicClassId = Convert.ToInt32(item["AcadmicClassId"]);
                      aroom.ClassName = item["ClassName"].ToString();
                      aroom.WeekDayId = Convert.ToInt32(item["WeekDayId"]);
                      aroom.DayName = item["DayName"].ToString();
                      aroom.StartTime = (TimeSpan)item["StartTime"];
                      aroom.EndTime = (TimeSpan)item["EndTime"];
                      aroom.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                      aroom.CreatedById = item["CreatedById"].ToString();
                      aroom.CreatedDate = Convert.ToDateTime(item["CreatedDate"]);
                      aroom.ModifiedById = item.IsNull("ModifiedById") ? null : item["ModifiedById"].ToString();
                      aroom.ModifiedDate = item.IsNull("ModifiedDate") ? (DateTime?)null : Convert.ToDateTime(item["ModifiedDate"]);
                  }
              }


          }
          catch (Exception ex)
          {

              throw ex;
          }
          return aroom;



      }

      public AssignRoom GetRoomAssignedClassAvailablity(AssignRoom aroom)
      {
          var objAssignRoomDao = new AssignRoomDAO(new SqlDatabase());
          DataTable tblRoom;
          AssignRoom assignroom = new AssignRoom();
          try
          {
              tblRoom = objAssignRoomDao.GetRoomAssignedClassAvailablity(aroom);
              if (tblRoom.Rows.Count > 0)
              {
                  foreach (DataRow item in tblRoom.Rows)
                  {
                      assignroom.RAssignId = Convert.ToInt32(item["RAssignId"]);
                      assignroom.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                      aroom.WeekDayId = Convert.ToInt32(item["WeekDayId"]);
                     
                  }
              }


          }
          catch (Exception ex)
          {

              throw ex;
          }
          return aroom;



      }

    }
}
