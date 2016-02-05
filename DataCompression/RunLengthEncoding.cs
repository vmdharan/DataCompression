using System;
using System.Collections.Generic;
using System.IO;

namespace DataCompression
{
    class RunLengthEncoding
    {
        private List<RLEData> rle_data;
        private byte[] finalBytes;

        // Constructor
        public RunLengthEncoding(byte[] sourceData, string fileName, bool doEncode)
        {
            rle_data = new List<RLEData>();

            if (doEncode)
            {
                encoder(sourceData);
                writeRLE();
                File.WriteAllBytes(fileName + ".enc3", finalBytes);
            }
            else
            {
                decoder(sourceData);
                expandRLE();
                File.WriteAllBytes(fileName + ".dec3", finalBytes);
            }
        }

        // Encoder
        public void encoder(byte[] sourceData)
        {
            int i, j;
            int num = 1;
            byte curr = sourceData[0];

            // Loop over the source data
            for (i = 1; i < sourceData.Length; i++)
            {
                // If the current byte is same as previous, increase num.
                if (curr == sourceData[i])
                {
                    num++;
                }
                // Else, add to the list.
                else
                {
                    rle_data.Add(new RLEData() { b = curr, n = num });
                    num = 1;
                    curr = sourceData[i];
                }
            }

            // Add the last element.
            rle_data.Add(new RLEData() { b = curr, n = num });
        }

        // Decoder
        public void decoder(byte[] sourceData)
        {
            int i;
            byte[] val = new byte[4];

            for (i = 0; i < sourceData.Length; i += 5)
            {
                val[0] = sourceData[i + 1];
                val[1] = sourceData[i + 2];
                val[2] = sourceData[i + 3];
                val[3] = sourceData[i + 4];
                rle_data.Add(new RLEData() { b = sourceData[i], n = convertBytesToInt(val) });
            }
        }

        // Calculate output buffer size.
        public int calcBufferSize()
        {
            int i;
            int buf_size = 0;

            for (i = 0; i < rle_data.Count; i++)
            {
                buf_size += (int)(rle_data[i].n);
            }

            return buf_size;
        }

        // Write RLE data to file.
        public void writeRLE()
        {
            int i, k = 0;
            int buf_size = rle_data.Count * 5;
            finalBytes = new byte[buf_size];

            // Loop over all the RLE list elements.
            for (i = 0; i < rle_data.Count; i++)
            {
                finalBytes[k] = rle_data[i].b;
                writeIntToBytes(rle_data[i].n, k + 1);
                k += 5;
            }

        }

        // Expand RLE data to file.
        public void expandRLE()
        {
            int i, j, k = 0;
            int buf_size = calcBufferSize();
            finalBytes = new byte[buf_size];

            // Loop over all the RLE list elements.
            for (i = 0; i < rle_data.Count; i++)
            {
                // Write the byte 'n' times.
                for (j = 0; j < rle_data[i].n; j++)
                {
                    finalBytes[k] = rle_data[i].b;
                    k++;
                }
            }
        }

        // Write the integer number to the output byte array.
        public void writeIntToBytes(int num, int w)
        {
            byte[] byteStream;
            byte[] byteStreamF;

            // Convert the integer to a byte array.
            byteStream = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(byteStream);
            }
            byteStreamF = byteStream;

            // Update the data to be written.
            finalBytes[w] = byteStreamF[0];
            finalBytes[w + 1] = byteStreamF[1];
            finalBytes[w + 2] = byteStreamF[2];
            finalBytes[w + 3] = byteStreamF[3];
        }

        // Convert byte array to Integer.
        public int convertBytesToInt(byte[] b)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(b);
            }

            return (BitConverter.ToInt32(b, 0));
        }

        // Define the data structure for storing the RLE values.
        public class RLEData
        {
            public byte b;   // value
            public int n;   // frequency
        }
    }
}
