using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDataContract.Common
{
  public  class City
    {
        public City()
        {
            this.CityId = 0;
            this.CityName = string.Empty;
        }
        #region City
        public int CityId { get; set; }
        public string CityName { get; set; }
        #endregion



    }
}
