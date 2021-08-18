using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Scanner
{
    public sealed class NVRAM
    {
        private static readonly object lck = new object();
        private static NVRAM inst = null;
        static NVRAM() { }

        public static NVRAM Instance
        {
            get
            {
                if (inst != null) return inst;
                else
                {
                    lock (lck)
                    {
                        if (inst == null) inst = new NVRAM();
                        return inst;
                    }
                }
            }
        }

        public List<byte[]> MemoryItems = new List<byte[]>();

        public void GenerateRandomData()
        {
            Random rng = new Random();
            MemoryItems.Clear();
            while (MemoryItems.Count < 24)
            {
                byte[] item = new byte[6];
                for(int i = 0; i < 6; i++)
                {
                    int rngX = rng.Next(0, 32);

                    item[i] = Convert.ToByte($"{rngX}", 16);
                }

                MemoryItems.Add(item);
            }

            SaveMemFile();
        }

        public void SaveMemFile()
        {
            string jsonized = JsonConvert.SerializeObject(MemoryItems, Formatting.Indented);
            File.WriteAllText("Memcard.json", jsonized);
        }

        public void ReadMemFile()
        {
            MemoryItems = (List<byte[]>)JsonConvert.DeserializeObject<List<byte[]>>(File.ReadAllText("Memcard.json"));
        }


        public List<byte> mem { get; set; } = new List<byte>();

        public DateTime LastByteReceived { get; set; } = DateTime.Now;
    }
}
