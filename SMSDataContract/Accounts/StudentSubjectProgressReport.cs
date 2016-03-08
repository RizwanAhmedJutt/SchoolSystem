using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentSubjectProgressReport
    {
        public int StudentProgressTypeId { get; set; }
        public int StudentId { get; set; }
        public string Reporting { get; set; }
        public int CourseId { get; set; }
        public int EvalTypeId { get; set; }
        public int Rewards { get; set; }
        public string Consequence { get; set; }
        public float Average { get; set; }

    }
}
