using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    class SlotID
    {
        public static int ConvertPosToSlot(string sID)
        {
            switch (sID)
            {
                case "0000":
                    {
                        return 0;
                    }
                case "0100":
                    {
                        return 1;
                    }
                case "0200":
                    {
                        return 2;
                    }
                case "0300":
                    {
                        return 3;
                    }
                case "0400":
                    {
                        return 4;
                    }
                case "0500":
                    {
                        return 5;
                    }
                case "0600":
                    {
                        return 6;
                    }
                case "0700":
                    {
                        return 7;
                    }
                case "0800":
                    {
                        return 8;
                    }
                case "0900":
                    {
                        return 9;
                    }
                case "0a00":
                    {
                        return 10;
                    }
                case "0b00":
                    {
                        return 11;
                    }
                case "0c00":
                    {
                        return 12;
                    }
                case "0d00":
                    {
                        return 13;
                    }
                case "0e00":
                    {
                        return 14;
                    }
                case "0f00":
                    {
                        return 15;
                    }
                case "0101":
                    {
                        return 16;
                    }
                case "0201":
                    {
                        return 17;
                    }
                case "0301":
                    {
                        return 18;
                    }
                case "0401":
                    {
                        return 19;
                    }
                case "0501":
                    {
                        return 20;
                    }
                case "0601":
                    {
                        return 21;
                    }
                case "0701":
                    {
                        return 22;
                    }
                case "0801":
                    {
                        return 23;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
    }
}
