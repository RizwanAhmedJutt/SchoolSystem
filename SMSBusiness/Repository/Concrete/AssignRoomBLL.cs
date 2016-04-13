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
    public class AssignRoomBLL : IAssignRoom
    {
        public List<AssignRoom> GetALLRoomAssignedClass()
        {
            var objAssignRoomDao = new AssignRoomDAO(new SqlDatabase());
            DataTable AssignRoom = objAssignRoomDao.GetALLRoomAssignedClass();
            List<AssignRoom> AssignedRooms = new List<AssignRoom>();
            try
            {
                if (AssignRoom.Rows.Count > 0)
                {
                    foreach (DataRow item in AssignRoom.Rows)
                    {
                        AssignRoom aroom = new AssignRoom();
                        aroom.RAssignId = Convert.ToInt32(item["RAssignId"]);
                        aroom.RoomName = item["RoomName"].ToString();
                        aroom.ClassName = item["ClassName"].ToString();
                        aroom.DayName = item["DayName"].ToString();
                        aroom.StartTime = item["StartTime"].ToString();
                        aroom.EndTime = item["EndTime"].ToString();
                        aroom.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                        AssignedRooms.Add(aroom);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return AssignedRooms;
        }
        public List<AssignRoom> GetALLRoomAssignedClassByRoomName(string RoomName)
        {
            var objAssignRoomDao = new AssignRoomDAO(new SqlDatabase());
            DataTable AssignRoom = objAssignRoomDao.GetALLRoomAssignedClassByRoomName(RoomName);
            List<AssignRoom> AssignedRooms = new List<AssignRoom>();
            try
            {
                if (AssignRoom.Rows.Count > 0)
                {
                    foreach (DataRow item in AssignRoom.Rows)
                    {
                        AssignRoom aroom = new AssignRoom();
                        aroom.RAssignId = Convert.ToInt32(item["RAssignId"]);
                        aroom.RoomName = item["RoomName"].ToString();
                        aroom.ClassName = item["ClassName"].ToString();
                        aroom.DayName = item["DayName"].ToString();
                        aroom.StartTime = item["StartTime"].ToString();
                        aroom.EndTime = item["EndTime"].ToString();
                        aroom.IsAvailable = Convert.ToBoolean(item["IsAvailable"]);
                        AssignedRooms.Add(aroom);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return AssignedRooms;
        }
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
        public AssignRoom GetRoomAssignedDetailId(int rAssignId)
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
                        aroom.WeekDayId = Convert.ToInt32(item["DayId"]);
                        aroom.DayName = item["DayName"].ToString();
                        aroom.StartTime =item["StartTime"].ToString();
                        aroom.EndTime = item["EndTime"].ToString();
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
                        assignroom.ClassName = item["ClassName"].ToString();

                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return assignroom;



        }

    }
    public class RoomBLL : IRoom
    {

        public List<Room> GetALLRooms()
        {
            var objRoomDao = new RoomDAO(new SqlDatabase());
            DataTable tblRoom;
            List<Room> objRoom = new List<Room>();
            try
            {
                tblRoom = objRoomDao.GetALLRoom();
                if (tblRoom.Rows.Count > 0)
                {
                    foreach (DataRow item in tblRoom.Rows)
                    {
                        Room aroom = new Room();
                        aroom.RoomId = Convert.ToInt32(item["RoomId"]);
                        aroom.RoomName = item["RoomName"].ToString();
                        objRoom.Add(aroom);
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objRoom;

        }
        public int AddChangesRoom(Room r)
        {
            var objRoomDao = new RoomDAO(new SqlDatabase());
            int ReturnValue = 0;  // Value will be 99 in case of Update
            try
            {
                ReturnValue = objRoomDao.InsertUpdateRoom(r);
            }
            catch (Exception)
            {

                throw;
            }

            return ReturnValue;

        }
        public Room GetRoomById(int Id)
        {
            var objRoomDao = new RoomDAO(new SqlDatabase());
            DataTable tblRoom;
            Room aroom = new Room();
            try
            {
                tblRoom = objRoomDao.GetRoomById(Id);
                if (tblRoom.Rows.Count > 0)
                {
                    foreach (DataRow item in tblRoom.Rows)
                    {
                        aroom.RoomId = Convert.ToInt32(item["RoomId"]);
                        aroom.RoomName = item["RoomName"].ToString();
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
