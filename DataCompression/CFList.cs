using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class CFList
    {
        public CFNode head { get; set; }
        public CFNode foot { get; set; }

        public CFList()
        {
            head = null;
            foot = null;
        }
    }
}
