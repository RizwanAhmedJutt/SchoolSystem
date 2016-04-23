using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSBusiness.Repository.Abstract
{
  public  interface IAssessmentCategories
    {
      List<AssessmentCategories> GetALLAssessmentCategories();
      AssessmentCategories GetAssessmentCategoryById(int AssessmentCategoryId);
      int AddChangeAssessmentCategories(AssessmentCategories dAssessmentCategory);
      AssessmentCategories GetAssessmentCategoryByName(string AssessmentName);
    }
}
