using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
  public class DailyAssessmentSubType
    {

      public DailyAssessmentSubType()
      {
          AssessmentSubTypeId = 0;
          AssessmentSubTypeName = string.Empty;
          CreatedById = string.Empty;
          CreateDate = DateTime.Now;
          ModifiedById = string.Empty;
          ModifiedDate = null;
      }
      public int AssessmentSubTypeId { get; set; }
      public string AssessmentSubTypeName { get; set; }
      public string CreatedById { get; set; }
      public DateTime CreateDate { get; set; }
      public string ModifiedById { get; set; }
      public DateTime? ModifiedDate { get; set; }
    }
}
