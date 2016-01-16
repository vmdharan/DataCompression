using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression
{
    class DeltaEncoding
    {
        // Constructor
        public DeltaEncoding(byte[] data, string fileName, bool doEncode)
        {
            if(doEncode)
            {
                encode(data);
                File.WriteAllBytes(fileName + ".enc2", data);
            }
            else
            {
                decode(data);
                File.WriteAllBytes(fileName + ".dec2", data);
            }
        }

        // Encoder
        public void encode(byte[] data)
        {
            int i;
            byte prev = 0x00;
            byte curr = 0x00;

            for(i = 0; i < data.Length; i++)
            {
                curr = data[i];
                data[i] = (byte) (curr - prev - 0x80);
                prev = curr;
            }
        }

        // Decoder
        public void decode(byte[] data)
        {
            int i;
            byte prev = 0x00;
            byte delta = 0x00;

            for(i = 0; i < data.Length; i++)
            {
                delta = data[i];
                data[i] = (byte) (delta + prev + 0x80);
                prev = data[i];
            }
        }
    }
}
