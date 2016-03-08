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
   public class CategoryBLL : ICategoryBLL
    {

       public List<Category> GetALLCategories()
       {
           var objCategoryDao = new CategoryDAO(new SqlDatabase());
           DataTable dtCity = objCategoryDao.GetALLCategories();
           List<Category> Categories = new List<Category>();
           try
           {
               if (dtCity.Rows.Count > 0)
               {
                   foreach (DataRow item in dtCity.Rows)
                   {
                       Category category = new Category();
                       category.CategoryId = int.Parse(item["CategoryId"].ToString());
                       category.CategoryName = item["CategoryName"].ToString();
                       Categories.Add(category);
                   }


               }
           }
           catch (Exception)
           {

               throw;
           }

           return Categories;
       }


    }
}
