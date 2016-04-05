﻿using SMSDataContract.Accounts;
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


    }
    public class Room
    {
        private readonly IDatabase gObjDatabase;

        public Room()
        {
            RoomId = 0;
            RoomName = string.Empty;

        }
        public int RoomId { get; set; }
        public string RoomName { get; set; }


    } 

    
}