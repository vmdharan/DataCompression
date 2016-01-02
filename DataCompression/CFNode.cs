using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class CFNode
    {
        public char key { get; set; }      // Character value
        public int frequency { get; set; } // Character frequency
        public bool isLeaf { get; set; }   // True if this is a leaf node
        public CFNode prev { get; set; }   // Previous node
        public CFNode next { get; set; }   // Next node
        public CFNode left_link { get; set; } // Left link
        public CFNode right_link { get; set; } // Right link

        public CFNode()
        {
            key = '0';
            frequency = 0;
            isLeaf = false;
            prev = null;
            next = null;
            left_link = null;
            right_link = null;
        }

        public CFNode(char c, int f)
        {
            key = c;
            frequency = f;
            isLeaf = false;
            prev = null;
            next = null;
            left_link = null;
            right_link = null;
        }

    }
}
