using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL.DAL
{
    public class CategoryDAO
    {
        private readonly IDatabase gObjDatabase;

        public CategoryDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public DataTable GetALLCategories()
        {
            DataTable dtCategoryDetails;
            try
            {
                var getcategories = "Select CategoryId, CategoryName from Categories";
                using (DbCommand objCommand = gObjDatabase.GetSqlStringCommand(getcategories))
                {

                    dtCategoryDetails = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtCategoryDetails;
        }



    }
}
