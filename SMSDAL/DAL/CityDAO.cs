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


    }
}
