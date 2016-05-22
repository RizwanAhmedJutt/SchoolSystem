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
  public  class AcadmicClassDAO
    {

       private readonly IDatabase gObjDatabase;
       public AcadmicClassDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

       public DataTable GetALLAcadmicClassies()
       {
           DataTable dtClassDetails;
           try
           {
               var getcategories = "Select AcadmicClassId,ClassName from AcadmicClass";
               using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getcategories))
               {

                   dtClassDetails = gObjDatabase.GetDataTable(objCommand);
               }
           }
           catch
           {
               throw;
           }
           return dtClassDetails;
       }

       public int AddChangesAcadmicClass(AcadmicClass acadmicClass)
        {
            try
            {
                var query = "Insert into AcadmicClass (ClassName) values('" + acadmicClass.ClassName + "')";
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
