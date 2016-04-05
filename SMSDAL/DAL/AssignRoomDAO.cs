using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL.DAL
{
    public class AssignRoomDAO
    {
        private readonly IDatabase gObjDatabase;
        public AssignRoomDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public int InsertUpdateAssignRoom(AssignRoom assignRoom)
        {
            try
            {
                using (DbCommand objDbCommand = gObjDatabase.GetStoredProcCommand("sp_Room_AssignRoomInsertUpdate"))
                {
                    gObjDatabase.AddInParameter(objDbCommand, "@RAssignId", DbType.Int32, assignRoom.RAssignId);
                    gObjDatabase.AddInParameter(objDbCommand, "@RoomId", DbType.Int32, assignRoom.RoomId);
                    gObjDatabase.AddInParameter(objDbCommand, "@AcadmicClassId", DbType.Int32, assignRoom.AcadmicClassId);
                    gObjDatabase.AddInParameter(objDbCommand, "@WeekDayId", DbType.Int32, assignRoom.WeekDayId);
                    gObjDatabase.AddInParameter(objDbCommand, "@StartTime", DbType.Time,assignRoom.StartTime);
                    gObjDatabase.AddInParameter(objDbCommand, "@EndTime", DbType.Time, assignRoom.EndTime);
                    gObjDatabase.AddInParameter(objDbCommand, "@IsAvailable", DbType.Boolean, assignRoom.IsAvailable);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedDate", DbType.DateTime, assignRoom.CreatedDate);
                    gObjDatabase.AddInParameter(objDbCommand, "@CreatedById", DbType.String, assignRoom.CreatedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedById", DbType.String, assignRoom.ModifiedById == null ? DBNull.Value : (object)assignRoom.ModifiedById);
                    gObjDatabase.AddInParameter(objDbCommand, "@ModifiedDate", DbType.DateTime, assignRoom.ModifiedDate == null ? DBNull.Value : (object)assignRoom.ModifiedDate);
                    gObjDatabase.AddOutParameter(objDbCommand, "@RoomAssignednewId", DbType.Int32, 4);
                    SqlParameter returnParameter = new SqlParameter("RetValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    objDbCommand.Parameters.Add(returnParameter);
                    gObjDatabase.ExecuteNonQuery(objDbCommand);



                    if (assignRoom.RAssignId == 0)
                    {


                        int getAssignId = Convert.ToInt32(objDbCommand.Parameters["@RoomAssignednewId"].Value);
                        return getAssignId;
                    }
                    else if (assignRoom.RAssignId > 0)
                    {
                        var UpdateValue = returnParameter.Value;
                        return (int)UpdateValue;
                    }

                }
            }
            catch
            {
                throw;
            }



            return 0;// show Error in inserting or Updating Record
        }

        public DataTable GetRoomAssignClassDetailByID(int rAssignId)
        {
            DataTable dtRoomDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Room_GetRoomAssignClassDetailByID"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@RAssignId", DbType.Int32, rAssignId);
                    dtRoomDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtRoomDetails;
        }
        public DataTable GetRoomAssignedClassAvailablity(AssignRoom aroom)
        {
            DataTable dtRoomDetails;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("sp_Room_GetRoomAssignedClassAvailablity"))
                {
                    gObjDatabase.AddInParameter(objCommand, "@RoomId", DbType.Int32, aroom.RoomId);
                    gObjDatabase.AddInParameter(objCommand, "@ClassId", DbType.Int32, aroom.AcadmicClassId);
                    gObjDatabase.AddInParameter(objCommand, "@WeekDayId", DbType.Int32, aroom.WeekDayId);
                    gObjDatabase.AddInParameter(objCommand, "@StartTime", DbType.Time, aroom.StartTime);
                    gObjDatabase.AddInParameter(objCommand, "@EndTime", DbType.Time, aroom.EndTime);
                    dtRoomDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtRoomDetails;
        }
    }
    public class RoomDAO
    {
        private readonly IDatabase gObjDatabase;
        public RoomDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public int InsertUpdateRoom(Room r)
        {
            try
            {
                var query = string.Empty;
                if (r.RoomId == 0)
                    query = "Insert Into Room (RoomName) Values ('" + r.RoomName + "')";
                else
                    query = "Update Room set RoomId=" + r.RoomId + ", RoomName='" + r.RoomName + "'";
                using (DbCommand objDbCommand = gObjDatabase.GetSqlStringCommand(query))
                {
                   
                    gObjDatabase.ExecuteNonQuery(objDbCommand);

                    return 1; //Successfully Added Or updated..
                }
            }
            catch
            {
                throw;
            }


            
        }

        public DataTable GetRoomById(int RoomId)
        {
            DataTable dtRoomDetails;
            try
            {
                var query = "Select * from Room WHERE RoomId="+RoomId;
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query))
                {
                   
                    dtRoomDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtRoomDetails;


        }
        public DataTable GetALLRoom()
        {
            DataTable dtAllRoom;
            try
            {
                var query = "Select * from Room";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(query))
                {

                    dtAllRoom = gObjDatabase.GetDataTable(objCommand);
                }
                return dtAllRoom;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
  

    
}
