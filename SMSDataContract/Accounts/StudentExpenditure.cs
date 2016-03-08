using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Accounts
{
    public class StudentExpenditure
    {
        public int StFeeId { get; set; }
        public int StudentId { get; set; }
        public DateTime FeeMonth { get; set; }
        public string ClassName { get; set; }
        public float TuitionFee { get; set; }
        public float TransportationFee { get; set; }
        public float BooksFee { get; set; }
        public float NoteBookFee { get; set; }
        public float StationaryFee { get; set; }
        public float Uniform { get; set; }
        public float OtherFee { get; set; }
        public int CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
