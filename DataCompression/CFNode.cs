using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class CFNode
    {
        char key { get; set; }      // Character value
        int frequency { get; set; } // Character frequency
        bool isLeaf { get; set; }   // True if this is a leaf node
        CFNode prev { get; set; }   // Previous node
        CFNode next { get; set; }   // Next node

        CFNode(char c, int f)
        {
            key = c;
            frequency = f;

            isLeaf = false;

            prev = null;
            next = null;
        }

    }
}
