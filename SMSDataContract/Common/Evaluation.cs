using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
  public  class Evaluation
    {
        [Display(Name = "V.Good")]
        public bool VeryGood { get; set; }

        public bool Good { get; set; }
        [Display(Name = "AVG")]
        public bool Average { get; set; }

        public bool Worse { get; set; }
        public string WorseConcequence { get; set; }
    }
}
