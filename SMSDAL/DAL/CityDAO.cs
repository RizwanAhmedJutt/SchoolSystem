using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL.DAL
{
    public class CityDAO
    {

        private readonly IDatabase gObjDatabase;
        public CityDAO(IDatabase database)
        {
            gObjDatabase = database;
        }
        public DataTable GetALLCities()
        {
            DataTable dtCityDetails;
            try
            {
                var getcities = "Select CityId,CityName from Cities";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getcities))
                {

                    dtCityDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtCityDetails;
        }

        public int AddChangesCity(City c)
        {
            try
            {
                var query=c.CityId>0? "Update Cities set CityName='" + c.CityName + "' Where cityId="+c.CityId : "Insert into Cities (CityName) values('" + c.CityName + "')";
                using (DbCommand objDbCommand = gObjDatabase.GetSqlStringCommand(query))
                {

                    gObjDatabase.ExecuteNonQuery(objDbCommand);
                    return 1;
                }
            }
            catch
            {
                throw;
            }

            return 0;  // show Error in inserting or Updating Record
        }

    }
}
