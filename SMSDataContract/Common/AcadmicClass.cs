using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
 public   class AcadmicClass
    {
        public AcadmicClass()
        {
            this.AcadmicClassId = 0;
            this.ClassName = string.Empty;
        }
        #region AcadmicClass
        public int AcadmicClassId { get; set; }
        public string ClassName { get; set; }

        #endregion

    }
}
