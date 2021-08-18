using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Scanner
{
    class Program
    {
        static string bytesToString(byte[] str)
        {
            string ret = "";
            foreach(byte b in str)
            {
                ret += (ret.Length==0?"":" ") + b.ToString("x2");
            }
            return ret;
        }
        static void Main(string[] args)
        {
            if (!File.Exists("Memcard.json"))
                NVRAM.Instance.GenerateRandomData();
            else
                NVRAM.Instance.ReadMemFile();


            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5667);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:5667");

            KnownPackets.INIT_DICT();

            Thread X = new Thread(() =>
            {

                while (true)
                {
                    bool iDataChanged = false;
                    bool bDataWritten = false;
                    int iDataSlot = 0;
                    bool bAck = false;

                    Console.WriteLine("Waiting for client!");
                    TcpClient cli = server.AcceptTcpClient();
                    Console.WriteLine("Client has connected");
                    while (cli.Connected)
                    {
                        bool iAlreadySent = false;
                        if (cli.Available == 0)
                        {
                            if (NVRAM.Instance.mem.Count == 0) continue;
                            byte[] bmem = NVRAM.Instance.mem.ToArray();
                            try
                            {
                                if(iDataChanged && bmem.Length >= 8 && !bAck)
                                {
                                    bAck = true;
                                    iAlreadySent = true;
                                    byte[] reply = new byte[] { 0x5A, 0x5A, 0x5A, 0x5A };
                                    cli.GetStream().Write(reply, 0, reply.Length);
                                }else if(iDataChanged && bAck)
                                {
                                    if (NVRAM.Instance.mem.Contains(0xAC))
                                    {
                                        int index = NVRAM.Instance.mem.IndexOf(0xAC);
                                        index++;
                                        if (NVRAM.Instance.mem[index] == 0x4A)
                                        {
                                            NVRAM.Instance.MemoryItems[iDataSlot] = new byte[] { NVRAM.Instance.mem[0], NVRAM.Instance.mem[1], NVRAM.Instance.mem[2], NVRAM.Instance.mem[3], NVRAM.Instance.mem[4], NVRAM.Instance.mem[5] };
                                            NVRAM.Instance.SaveMemFile();
                                            byte[] reply = new byte[] { 0xE1, 0xE1, 0xE1, 0xE1 };
                                            cli.GetStream().Write(reply, 0, reply.Length);
                                            iAlreadySent = true;
                                            NVRAM.Instance.mem.Clear();
                                            iDataChanged = false;
                                            bAck = false;
                                            bDataWritten = true;
                                        }
                                    }
                                }

                                byte[] last = new byte[8];
                                if (bmem.Length > 16)
                                {
                                    last = bmem.Slice(bmem.Length - 8, bmem.Length);
                                }
                                if(bDataWritten)
                                {
                                    bmem = last;
                                }

                                if (bmem[0] == 0xAC)
                                {
                                    if (bmem[1] == 0x0C)
                                    {
                                        if (bmem[2] == 0x00)
                                        {
                                            if (bmem[3] == 0xA0)
                                            {
                                                if (bmem.Length == 8)
                                                {

                                                    // get the offset
                                                    string slotBin = bytesToString(new byte[] { bmem[4] });
                                                    slotBin = new string($"{slotBin[1]}{slotBin[0]}{bytesToString(new byte[] { bmem[5] })}");
                                                    // Bits reversed.
                                                    // Now pull from the real memory
                                                    // After reading the shape/color we will zero out this memory
                                                    int pos = SlotID.ConvertPosToSlot(slotBin);
                                                    if (NVRAM.Instance.MemoryItems.Count > pos)
                                                    {
                                                        // We're OK!
                                                        byte[] origin = NVRAM.Instance.MemoryItems[pos];
                                                        byte[] reply = { origin[0], origin[1], origin[2], origin[3] };
                                                        cli.GetStream().Write(reply, 0, reply.Length);
                                                        NVRAM.Instance.mem.Clear();
                                                        //Console.WriteLine($"Sent the parameters: {bytesToString(reply)}");
                                                        iAlreadySent = true;
                                                    }
                                                    else
                                                    {
                                                        // The memory does not exist
                                                        //Console.WriteLine($"Could not find memory at {slotBin}");
                                                    }
                                                }


                                            }
                                        }
                                        else if (bmem[2] == 0x20)
                                        {
                                            if (bmem[3] == 0x80)
                                            {
                                                if (bmem.Length == 8)
                                                {

                                                    // shape/color
                                                    // get the offset
                                                    string slotBin = bytesToString(new byte[] { bmem[4] });
                                                    slotBin = new string($"{slotBin[1]}{slotBin[0]}{bytesToString(new byte[] { bmem[5] })}");
                                                    // Bits reversed.
                                                    // Now pull from the real memory
                                                    int pos = SlotID.ConvertPosToSlot(slotBin);
                                                    if (NVRAM.Instance.MemoryItems.Count > pos)
                                                    {
                                                        // OK
                                                        NVRAM.Instance.mem.Clear();
                                                        byte[] origin = NVRAM.Instance.MemoryItems[pos];
                                                        byte[] reply = { origin[4], origin[5], 0xFF, 0xFF };
                                                        cli.GetStream().Write(reply, 0, reply.Length);
                                                        NVRAM.Instance.MemoryItems[pos] = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                                                        NVRAM.Instance.SaveMemFile();
                                                        //Console.WriteLine($"Sent shape: {bytesToString(reply)}");
                                                        iAlreadySent = true;
                                                    }
                                                    else
                                                    {
                                                        //Console.WriteLine($"Could not find memory at {slotBin}");
                                                    }
                                                }
                                            }
                                        }
                                    } else if(bmem[1] == 0x00)
                                    {
                                        if(bmem[2] == 0x00)
                                        {
                                            if(bmem[3] == 0xAC)
                                            {
                                                if(bmem[4] == 0x00 && bmem[5] == 0x00 && bmem[6] == 0x00 && bmem[7] == 0x00)
                                                {

                                                    bDataWritten = false;
                                                    NVRAM.Instance.mem.Clear();
                                                    byte[] reply = { 0x5A, 0x5A, 0x5A, 0x5A };
                                                    cli.GetStream().Write(reply, 0, reply.Length);
                                                    iAlreadySent = true;
                                                }
                                            }
                                        }
                                    } else if(bmem[1] == 0x4C)
                                    {
                                        // Upload
                                        // This should set a flag. We'll store the slot position we are modifying
                                        // We will then write the data until we hear the 4A, E6 signal telling us to commit data
                                        if(bmem[3] == 0xE0)
                                        {
                                            if (bmem.Length == 8)
                                            {
                                                // Set the committing flag
                                                iDataChanged = true;
                                                string slotBin = bytesToString(new byte[] { bmem[4] });
                                                slotBin = new string($"{slotBin[1]}{slotBin[0]}{bytesToString(new byte[] { bmem[5] })}");
                                                // Bits reversed.
                                                // Now pull from the real memory
                                                int pos = SlotID.ConvertPosToSlot(slotBin);
                                                iDataSlot = pos;
                                                NVRAM.Instance.mem.Clear();
                                                iAlreadySent = true;
                                                byte[] reply = new byte[] { 0x5A, 0x5A, 0x5A, 0x5A };
                                                cli.GetStream().Write(reply, 0, reply.Length);
                                            }
                                        }

                                    } else if(bmem[1] == 0x4A)
                                    {
                                        if(bmem[3] == 0xE6)
                                        {
                                            if (bmem.Length == 8)
                                            {
                                                if (iDataChanged)
                                                {

                                                    byte[] reply = new byte[] { 0xE1, 0xE1, 0xE1, 0xE1 };
                                                    cli.GetStream().Write(reply, 0, reply.Length);
                                                    iAlreadySent = true;
                                                    iDataChanged = false;
                                                    bDataWritten = true;
                                                    bAck = false;
                                                    NVRAM.Instance.mem.Clear();
                                                }
                                                else
                                                {
                                                    byte[] reply = new byte[] { 0x5A, 0x5A, 0x5A, 0x5A };
                                                    cli.GetStream().Write(reply, 0, reply.Length);
                                                    iAlreadySent = true;
                                                    NVRAM.Instance.mem.Clear();
                                                }
                                            }
                                        }
                                    } else if(bmem[1] == 0xBA)
                                    {
                                        if(bmem[3] == 0x16)
                                        {
                                            if (bmem.Length == 8)
                                            {
                                                byte b = 0x00;
                                                List<byte> replyArr = b.FormatBytes(16*4);
                                                replyArr[0] = NVRAM.Instance.MemoryItems[iDataSlot][0];
                                                replyArr[1] = NVRAM.Instance.MemoryItems[iDataSlot][1];
                                                replyArr[2] = NVRAM.Instance.MemoryItems[iDataSlot][2];
                                                replyArr[3] = NVRAM.Instance.MemoryItems[iDataSlot][3];
                                                replyArr[4] = NVRAM.Instance.MemoryItems[iDataSlot][4];
                                                replyArr[5] = NVRAM.Instance.MemoryItems[iDataSlot][5];

                                                // add the standard data (??)
                                                replyArr[(16 * 2) + 5] = 0x64;
                                                replyArr[(16 * 2) + 6] = 0x05;
                                                replyArr[(16 * 2) + 13] = 0x51;
                                                replyArr[(16 * 2) + 14] = 0x01;
                                                replyArr[(16 * 3) + 5] = 0x04;
                                                replyArr[(16 * 3) + 9] = 0x06;
                                                byte bF = 0xFF;
                                                replyArr.AddRange(bF.FormatBytes(16 * 4));
                                                replyArr.AddRange(b.FormatBytes(16 * 3));

                                                // add more standard data (??) - What the purpose of this data is... unknown. Upload process won't complete without it
                                                replyArr.AddRange(new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,0x00, 0xFC, 0xFF,
                                                0xFF,0x1F,0x64, 0x8C, 0x5C, 0x11,
                                                0xD4, 0xEE,0x5B,0x1B,0xD4, 0x8E,0x1C,0x1B, 0x54, 0xEE,0x5E, 0x19, 0xE4, 0x8E, 0xB8, 0x1B,
                                                0xFC, 0xFF,0xFF, 0x1F, 0x48, 0xDD, 0x5C, 0x09, 0x48, 0x49, 0x45, 0x09, 0xC8, 0xC8, 0xDC, 0x08,
                                                0x48, 0x49, 0x45, 0x09, 0xDC, 0xDC, 0xDC, 0x1C, 0x00, 0x00, 0x00, 0x00, 0xB8, 0x74, 0xF5, 0x04,
                                                0x88, 0x16, 0x93, 0x04, 0xB8, 0x77, 0xD7, 0x0E, 0x88, 0x15, 0x15, 0x0A, 0xB8, 0x74, 0xF7, 0x0A});



                                                replyArr.AddRange(bF.FormatBytes(16 * 16));

                                                byte[] reply = replyArr.ToArray();
                                                cli.GetStream().Write(reply, 0, reply.Length);
                                                iAlreadySent = true;
                                                NVRAM.Instance.mem.Clear();

                                                string sFormatBin = "";
                                                for(int i = 0; i < replyArr.Count; i += 16)
                                                {
                                                    byte[] bTmp = new byte[16];
                                                    replyArr.CopyTo(i, bTmp, 0, 16);
                                                    sFormatBin += "\n" + bytesToString(bTmp);
                                                }
                                                File.WriteAllText("BinPrint.txt", sFormatBin);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                // Nothing we could have done then
                                //Console.WriteLine("Error: " + e.Message);
                            }
                            
                            if (KnownPackets.PKT_DICTIONARY.ContainsKey(bytesToString(bmem)) && !iAlreadySent)
                            {
                                NVRAM.Instance.mem.Clear();
                                //Console.WriteLine("REQUEST: " + bytesToString(bmem));
                                byte[] reply = KnownPackets.PKT_DICTIONARY[bytesToString(bmem)];
                                cli.GetStream().Write(reply, 0, reply.Length);
                                //Console.WriteLine("REPLY: " + bytesToString(reply));
                            }
                            //else Console.WriteLine($"Packet not found in dictionary : ( {bmem.Length} ) : {bytesToString(bmem)}");
                            Console.Write("\rRequest: " + bytesToString(bmem));

                        }
                        else
                        {

                            byte[] b8 = new byte[cli.Available];

                            cli.GetStream().Read(b8, 0, b8.Length);
                            foreach (byte b in b8)
                            {
                                int index = b8.IndexOf(b);
                                try
                                {
                                    if (index != -1)
                                    {
                                        // OK
                                        if (b == 0xAC)
                                        {
                                            if (b8[index + 1] == 0x4A)
                                            {

                                                byte[] bNewData = new byte[6] { b8[0], b8[1], b8[2], b8[3], b8[4], b8[5] };

                                                NVRAM.Instance.MemoryItems[iDataSlot] = bNewData;
                                                NVRAM.Instance.SaveMemFile();
                                                Console.WriteLine("\n > RAW UPLOAD <\n" + bytesToString(NVRAM.Instance.mem.ToArray()));
                                                NVRAM.Instance.mem.Clear();
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    // ignore
                                }
                                
                                NVRAM.Instance.mem.Add(b);
                            }
                            NVRAM.Instance.LastByteReceived = DateTime.Now;
                        }


                    }
                    Console.WriteLine("Client disconnected");
                    cli.Close();
                }
            });
            X.Start();
            bool R = true;
            while (R)
            {
                Console.Write("\r\nSCANNER $ ");
                switch (Console.ReadLine())
                {
                    case "gen":
                        {
                            Console.WriteLine("Regenerating the memory!");
                            NVRAM.Instance.GenerateRandomData();
                            break;
                        }

                    case "help":
                        {
                            Console.WriteLine("> gen\t\tRegenerates the memory\n" +
                                "> erase\t\tErases the memory\n" +
                                "> quit\t\tQuits the scanner software\n" +
                                "> help\t\tThis message\n" +
                                "> dump\t\tDumps the current memory\n");
                            break;
                        }
                    case "erase":
                        {
                            Console.WriteLine("Command accepted. Memory erased");
                            for(int i = 0; i < NVRAM.Instance.MemoryItems.Count; i++)
                            {
                                NVRAM.Instance.MemoryItems[i] = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
                            }
                            NVRAM.Instance.SaveMemFile();
                            break;
                        }
                    case "quit":
                        {
                            Console.WriteLine("Program terminating!");
                            NVRAM.Instance.SaveMemFile();
                            server.Stop();
                            X.Abort();
                            R = false;
                            break;
                        }
                    case "dump":
                        {
                            foreach(byte[] bx in NVRAM.Instance.MemoryItems)
                            {
                                Console.WriteLine($"Parameters: {bytesToString(new byte[] { bx[0], bx[1], bx[2], bx[3] })}\n>Shape/Color: {bytesToString(new byte[] { bx[4], bx[5], 0xFF, 0xFF })}");
                                Console.WriteLine("\n");
                            }
                            break;
                        }
                }
            }
        }
    }
}
