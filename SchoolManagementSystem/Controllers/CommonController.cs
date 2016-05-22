using PagedList;
using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SchoolManagementSystem.Controllers
{
    public class CommonController : Controller
    {
        ICityBLL cities = new CityBLL();
        IAcadmicClassBLL acadmicClass = new AcadmicClassBLL();
        // GET: Common
        public ActionResult GetCity(int? page)
        {

            return View(cities.GetALLCities().ToPagedList(page ?? 1, 10));
        }
        [HttpGet]
        public ActionResult AddChangesCity(int Id)
        {


            if (Id == 0)
            {
                City c = new City();
                return View(c);
            }
            else
            {
                City getAllCities = cities.GetALLCities().Where(c => c.CityId == Id).Select(x => new City { CityId = x.CityId, CityName = x.CityName }).First();
                return View(getAllCities);
            }
        }
        [HttpPost]
        public ActionResult AddChangesCity(City c)
        {
            int getStatus = cities.AddChangesCity(c);
            return RedirectToAction("GetCity");
        }
        public string CheckCityNameExist(string CityName)
        {
            List<City> city = cities.GetALLCities();
            var query = from c in city
                        where c.CityName == CityName
                        select c;
            if (query.Any())
                return new JavaScriptSerializer().Serialize(true);// City Name  already Exist
            else
                return new JavaScriptSerializer().Serialize(false);   // City name not already Exist

        }
    }
}