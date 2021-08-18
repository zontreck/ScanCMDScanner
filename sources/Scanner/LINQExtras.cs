using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    public static class LINQExtras
    {
        public static int IndexOf(this byte[] arr, byte b)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                byte x = arr[i];
                if (x == b)
                {
                    return i;
                }
            }
            return -1;
        }
        public static byte[] Slice(this byte[] arr, int start, int end)
        {
            List<byte> b = new List<byte>();
            for(int i = start; i < end; i++)
            {
                b.Add(arr[i]);
            }
            return b.ToArray();
        }

        public static List<byte> FormatBytes(this byte b, int count)
        {
            List<byte> bx = new List<byte>();
            for(int i = 0; i < count; i++)
            {
                bx.Add(b);
            }
            return bx;
        }
    }
}
