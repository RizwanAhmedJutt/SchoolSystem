using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL.DAL
{
    public class TeacherDAO
    {
        private readonly IDatabase gObjDatabase;
        public TeacherDAO(IDatabase database)
        {
            gObjDatabase = database;
        }

        public DataTable GetALLTeachers()
        {
            DataTable dtTeachers;
            try
            {
                using (DbCommand objCommand = gObjDatabase.GetStoredProcCommand("USP_GetAllTeachers"))
                {

                    dtTeachers = gObjDatabase.GetDataTable(objCommand);
                }
            }
            catch
            {
                throw;
            }
            return dtTeachers;


        }
    }
}
