using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdk1.AppData
{
    internal class Connect
    {
        public static Testmdk1Entities c;
        public static Testmdk1Entities contex
        {
            get
            {
                if (c == null)
                    c = new Testmdk1Entities();
                return c;
            }
        }
    }
}
