using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
  public  class StudentMonthAssessment
    {
        public int MonthAssessmentId { get; set; }
        public int StudentId { get; set; }
        public string ReportingMonth { get; set; }
        public int MonthAssessmentTypeId { get; set; }

        public int Reward { get; set; }
        public string Consequence { get; set; }
        public float Average { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
