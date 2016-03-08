using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSDAL
{
    public class DbReturnValues
    {

        private int returnvalue;
        public int ReturnValue
        {
            get { return returnvalue; }
            set { returnvalue = value; }
        }

        private int identity;
        public int Identity
        {
            get { return identity; }
            set { identity = value; }
        }

        private string output1;
        public string Output1
        {
            get { return output1; }
            set { output1 = value; }
        }

        private string output2;
        public string Output2
        {
            get { return output2; }
            set { output2 = value; }
        }

        public DbReturnValues(int returnvalue, int identity, string output1, string output2)
        {

            this.returnvalue = returnvalue;
            this.identity = identity;
            this.output1 = output1;
            this.output2 = output2;

        }

    }
}
