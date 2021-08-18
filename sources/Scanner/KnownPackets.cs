using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    /// <summary>
    /// The purpose of this is to house pre-recorded packet maps as a fallback for commands we dont care about implementing
    /// </summary>
    public class KnownPackets
    {
        public static Dictionary<string, byte[]> PKT_DICTIONARY { get; set; } = new Dictionary<string, byte[]>();

        /*
         * Protocol documentation
         * AC - packet start
         *          2nd PACKET: 
         *              02 - Change memory location
         *              0c - Read
         *          3rd PACKET:
         *              80 - Shape/Color
         *              ac - Clear current memory location
         *              a0 - Request for parameters
         *              ae - set mem location
         */
        public static void INIT_DICT()
        {
            KnownPackets.PKT_DICTIONARY.Clear();
            // Connect packet?
            PKT_DICTIONARY.Add("ac 00 00 ac 00 00 00 00", new byte[] {0x5A, 0x5A, 0x5A, 0x5A });

            // Likely a scanned barcode? Memory location?
            PKT_DICTIONARY.Add("ac 15 00 b9 00 00 00 00", new byte[] { 0x17, 0x86, 0x08, 0x20 });

            // Unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 00 00 00 00", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            // Unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 00 00 00 00", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            // Unknown, memory location? - AC0000AC00000000 prior!
            PKT_DICTIONARY.Add("ac 02 00 ae 00 00 00 00", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            // Unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 10 00 00 10", new byte[] { 0x29, 0x72, 0x11, 0x53 });

            // unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 10 00 00 10", new byte[] { 0x00, 0x00, 0xFF, 0xFF });

            // unknown, memory location? - AC0000AC prior
            PKT_DICTIONARY.Add("ac 02 00 ae 10 00 00 10", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 20 00 00 20", new byte[] { 0x50, 0x60, 0x49, 0x22 });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 20 00 00 20", new byte[] { 0x08, 0x02, 0xFF, 0xFF });

            // unknown, appears to be a repeat of the connection packet twice
            PKT_DICTIONARY.Add("ac 00 00 ac 00 00 00 00 ac 00 00 ac 00 00 00 00", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 20 00 00 20", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 30 00 00 30", new byte[] { 0x99, 0x12, 0x84, 0x53 });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 30 00 00 30", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            // unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 30 00 00 30", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            // unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 40 00 00 40", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 40 00 00 40", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 40 00 00 40", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 50 00 00 50", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 50 00 00 50", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 50 00 00 50", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 60 00 00 60", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 60 00 00 60", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 60 00 00 60", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 70 00 00 70", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 70 00 00 70", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 70 00 00 70", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 80 00 00 80", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 80 00 00 80", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 80 00 00 80", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 90 00 00 90", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 90 00 00 90", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 90 00 00 90", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 a0 00 00 a0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 a0 00 00 a0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae a0 00 00 a0", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 b0 00 00 b0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 b0 00 00 b0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae b0 00 00 b0", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 c0 00 00 c0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 c0 00 00 c0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae c0 00 00 c0", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 d0 00 00 d0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 d0 00 00 d0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae d0 00 00 d0", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 e0 00 00 e0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 e0 00 00 e0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae e0 00 00 e0", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 f0 00 00 f0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 f0 00 00 f0", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae f0 00 00 f0", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 00 01 00 01", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 00 01 00 01", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 00 01 00 01", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 10 01 00 11", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 10 01 00 11", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 10 01 00 11", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 20 01 00 21", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 20 01 00 21", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 20 01 00 21", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 30 01 00 31", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 30 01 00 31", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 30 01 00 31", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 40 01 00 41", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 40 01 00 41", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 40 01 00 41", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 50 01 00 51", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 50 01 00 51", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 50 01 00 51", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 60 01 00 61", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 60 01 00 61", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 60 01 00 61", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 70 01 00 71", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 70 01 00 71", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 70 01 00 71", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 00 a0 80 01 00 81", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown
            PKT_DICTIONARY.Add("ac 0c 20 80 80 01 00 81", new byte[] { 0xFF, 0xFF, 0xFF, 0xFF });

            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 02 00 ae 80 01 00 81", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });
            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 48 00 e4 00 00 00 00", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });
            //unknown, memory location?
            PKT_DICTIONARY.Add("ac 4a 00 e6 00 00 00 00", new byte[] { 0x5A, 0x5A, 0x5A, 0x5A });

        }
    }
}
