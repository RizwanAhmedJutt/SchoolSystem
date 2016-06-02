using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class CityBLL : ICityBLL
    {

        public List<City> GetALLCities()
        {
            var objCityDao = new CityDAO(new SqlDatabase());
            DataTable dtCity = objCityDao.GetALLCities();
            List<City> Cities = new List<City>();
            try
            {
                if (dtCity.Rows.Count > 0)
                {
                    foreach (DataRow item in dtCity.Rows)
                    {
                        City city = new City();
                        city.CityId = int.Parse(item["CityId"].ToString());
                        city.CityName = item["CityName"].ToString();
                        Cities.Add(city);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return Cities;
        }

        public int AddChangesCity(City c)
        {
            var objAcadmicClassDao = new CityDAO(new SqlDatabase());

            return objAcadmicClassDao.AddChangesCity(c);
        }

    }
}
