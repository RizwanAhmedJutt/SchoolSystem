using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
  public  class DailyAssessmentHelper
    {

        public List<DailyAssessmentType> ParentAssessments { get; set; }
      //  public IEnumerable<AnswerVM> PossibleAnswers { get; set; }
        public List<DailyAssessmentSubType> ChildAssessments { get; set; }
       

    }
}
