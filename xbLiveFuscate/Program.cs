using System;
using System.IO;
using System.Linq;

namespace xbLiveFuscate
{
    internal class Program
    {
        public struct XexSectionInfo
        {
            public byte[] szName;

            public uint dwVirtualAddress;

            public uint dwVirtualSize;
        }

        public static uint dwBaseAddress = 2430599168u;

        public static uint RandomStuff(uint rand)
        {
            return (((((((((((((((((((((((rand + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4221) ^ 0x64 ^ 0x12) + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4237) ^ 0x64 ^ 0x12) + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4253) ^ 0x64 ^ 0x12) + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4381) ^ 0x64u ^ 0x12u;
        }

        public static byte RandomStuffByte(uint rand)
        {
            return (byte)(((((((((((((((((((((((((rand + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4221) ^ 0x64 ^ 0x12) + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4237) ^ 0x64 ^ 0x12) + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4253) ^ 0x64 ^ 0x12) + 999) ^ 0x539 ^ 0x1337) + 69) ^ 0x14D ^ 0x29A) + 123 - 4381) ^ 0x64u ^ 0x12u) & 0xFFu);
        }

        public static void RC4(ref byte[] Data, byte[] Key, int startOffset = 0, int size = 0)
        {
            int num = 0;
            byte[] array = new byte[256];
            byte[] array2 = new byte[256];
            int i;
            for (i = 0; i < 256; i++)
            {
                array[i] = (byte)i;
                array2[i] = Key[i % Key.GetLength(0)];
            }
            for (i = 0; i < 256; i++)
            {
                num = (num + array[i] + array2[i]) % 256;
                byte b = array[i];
                array[i] = array[num];
                array[num] = b;
            }
            i = (num = 0);
            if (size == 0)
            {
                size = Data.GetLength(0);
            }
            for (int j = 0; j < size; j++)
            {
                i = (i + 1) % 256;
                num = (num + array[i]) % 256;
                byte b = array[i];
                array[i] = array[num];
                array[num] = b;
                int num2 = (array[i] + array[num]) % 256;
                Data[startOffset + j] = (byte)(Data[startOffset + j] ^ array[num2]);
            }
        }



        public static int SearchByteByByte(byte[] bytes, byte[] pattern)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes.Length - i < pattern.Length)
                {
                    return 0;
                }
                if (pattern[0] != bytes[i])
                {
                    continue;
                }
                for (int j = 0; j < pattern.Length && bytes[i + j] == pattern[j]; j++)
                {
                    if (j == pattern.Length - 1)
                    {
                        return i;
                    }
                }
            }
            return 0;
        }

        public static XexSectionInfo GetSectionInfo(byte[] file, byte[] name)
        {
            XexSectionInfo result = default(XexSectionInfo);
            int num = SearchByteByByte(file, name);
            if (num != 0)
            {
                BinaryReader binaryReader = new BinaryReader(new MemoryStream(file.Skip(num).ToArray()));
                result.szName = binaryReader.ReadBytes(8);
                result.dwVirtualSize = binaryReader.ReadUInt32();
                result.dwVirtualAddress = binaryReader.ReadUInt32();
                binaryReader.Close();
            }
            return result;
        }

        public static byte[] GenerateRandomData(int count)
        {
            byte[] array = new byte[count];
            new Random().NextBytes(array);
            return array;
        }

        public static int FindArrayOffset(byte[] file)
        {
            return SearchByteByByte(file, new byte[8] { 110, 105, 103, 103, 101, 114, 54, 57 });
        }
        //6E696767
        private static void Main(string[] args)
        {
            if (File.Exists("decrypted_xbLive.xex"))
            {
                byte[] Data = File.ReadAllBytes("decrypted_xbLive.xex");
                if (Data.Length == 0)
                {
                    Console.WriteLine("Data Length 0!");
                    return;
                }
                int num = FindArrayOffset(Data);
                if (num != 0)
                {
                    XexSectionInfo sectionInfo = GetSectionInfo(Data, new byte[6] { 46, 112, 116, 101, 120, 116 });//old:46, 116, 101, 120, 116 equals 2E 74 65 78 74  New: 2E 70 74 65 78 74
                    XexSectionInfo sectionInfo2 = GetSectionInfo(Data, new byte[5] { 46, 100, 97, 116, 97 });//2E 64 61 74 61 
                    Console.WriteLine("[Section] .text - 0x{0:X} to 0x{1:X}", dwBaseAddress + sectionInfo.dwVirtualAddress, dwBaseAddress + sectionInfo.dwVirtualAddress + sectionInfo.dwVirtualSize);
                    Console.WriteLine("[Section] .data - 0x{0:X} to 0x{1:X}", dwBaseAddress + sectionInfo2.dwVirtualAddress, dwBaseAddress + sectionInfo2.dwVirtualAddress + sectionInfo2.dwVirtualSize);
                    Random random = new Random();
                    byte b = (byte)random.Next(255);
                    byte[] array = GenerateRandomData(48);
                    byte b2 = (byte)random.Next(255);
                    byte b3 = RandomStuffByte(18u);
                    RC4(ref Data, array, (int)(sectionInfo.dwVirtualAddress + 4096), (int)(sectionInfo.dwVirtualSize - 1616));
                    for (uint num2 = sectionInfo.dwVirtualAddress + 4096; num2 < sectionInfo.dwVirtualAddress + 4096 + sectionInfo.dwVirtualSize - 1616; num2++)
                    {
                        Data[num2] ^= b;
                    }
                    for (uint num3 = sectionInfo.dwVirtualAddress + 4096; num3 < sectionInfo.dwVirtualAddress + 4096 + sectionInfo.dwVirtualSize - 1616; num3++)
                    {
                        Data[num3] ^= b3;
                    }
                    for (int i = 0; i < 48; i++)
                    {
                        array[i] ^= b2;
                    }
                    byte[] array2 = new byte[4096];
                    EndianWriter endianWriter = new EndianWriter(new MemoryStream(array2), EndianStyle.BigEndian);
                    endianWriter.Write(dwBaseAddress + sectionInfo.dwVirtualAddress);
                    endianWriter.Write(sectionInfo.dwVirtualSize - 1616);
                    endianWriter.Write(b);
                    endianWriter.Write(b2);
                    endianWriter.Write(array);
                    endianWriter.Close();
                    Array.Copy(array2, 0, Data, num, array2.Length);
                    using (StreamWriter streamWriter = File.CreateText("output.xex"))
                    {
                        streamWriter.BaseStream.Write(Data, 0, Data.Length);
                    }
                    for (int j = 0; j < 48; j++)
                    {
                        array[j] ^= b2;
                    }
                    using (StreamWriter streamWriter2 = File.CreateText("rc4_key_dec.bin"))
                    {
                        streamWriter2.BaseStream.Write(array, 0, array.Length);
                    }
                    using (StreamWriter streamWriter3 = File.CreateText("text.bin"))
                    {
                        streamWriter3.BaseStream.Write(Data, (int)(sectionInfo.dwVirtualAddress + 4096), (int)(sectionInfo.dwVirtualAddress + 4096 + sectionInfo.dwVirtualSize - 1616));
                    }
                }
                else {
                    Console.WriteLine("Couldnt find array offset!");
                }
            }
            else
            {
                Console.WriteLine("Failed to find decrypted_xbLive.xex");
            }
        }
    }
}

