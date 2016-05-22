using SMSBusiness.Repository.Abstract;
using SMSDAL;
using SMSDAL.DAL;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Concrete
{
    public class AcadmicClassBLL : IAcadmicClassBLL
    {


        public List<AcadmicClass> GetALLAcadmicClassies()
        {
            var objAcadmicClassDao = new AcadmicClassDAO(new SqlDatabase());
            DataTable dtClass = objAcadmicClassDao.GetALLAcadmicClassies();
            List<AcadmicClass> aClassies = new List<AcadmicClass>();
            try
            {
                if (dtClass.Rows.Count > 0)
                {
                    foreach (DataRow item in dtClass.Rows)
                    {
                        AcadmicClass aclass = new AcadmicClass();
                        aclass.AcadmicClassId = int.Parse(item["AcadmicClassId"].ToString());
                        aclass.ClassName = item["ClassName"].ToString();
                        aClassies.Add(aclass);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }

            return aClassies;
        }

        public int AddChangesAcadmicClass(AcadmicClass acadmicClass)
        {
            var objAcadmicClassDao = new AcadmicClassDAO(new SqlDatabase());

            return objAcadmicClassDao.AddChangesAcadmicClass(acadmicClass);
        }



    }
}
