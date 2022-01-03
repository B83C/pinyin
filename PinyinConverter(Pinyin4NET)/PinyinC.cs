//#define DEBUG_PINYIN
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.IO;

namespace PinyinC
{
    public class PinyinC
    {
        private static Dictionary<string, string> PinyinTable;

        static string resourceName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"pinyindb\unicode_to_hanyu_pinyin.txt");

        public static bool initializeResource()
        {
            if (!File.Exists(resourceName))
            {
                MessageBox.Show("PinyinDB : " + resourceName + " couldn't be found", "Error");
                return false;
            }
            PinyinTable = new Dictionary<string, string>();
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(resourceName));
            StreamReader sr = new StreamReader(ms);
            string[] s = new string[2];
            while (!sr.EndOfStream)
            {
                s = sr.ReadLine().Split(' ');
                PinyinTable.Add(s[0], s[1]);
            }
            s = null;
            sr.Dispose();
            ms.Dispose();
#if DEBUG_ERR
            if (Error())
            {
                return false;
            }
#endif
#if DEBUG_PINYIN
            File.WriteAllText(Directory.GetCurrentDirectory() + @"\Debug_Pinyin.txt", "");
#endif
            return true;
        }

        private static bool Error()
        {
            string str = null;
            Regex reg = new Regex("[a-z]*[1-5]?", RegexOptions.Compiled);
            for (int i = 0; i < PinyinTable.Count; i++)
            {
                str = PinyinTable.ElementAt(i).Value;
                if (str == null || str == string.Empty || !reg.IsMatch(str))
                {
                    MessageBox.Show("Unrecognized pattern in " + resourceName + " at" + i.ToString() + "th line.", "Error");
                    return true;
                }
            }
            return false;
        }

        static public string C2P(char chr, bool format, int pinyin_index = 0)
        {
            string unicode = GetUNICODE(chr);
            if (PinyinTable.ContainsKey(unicode))
            {
                if(format)
                    return PinyinFormatter.AppendToneNumber(SGet(PinyinTable[unicode], pinyin_index));
                else
                    return SGet(PinyinTable[unicode], pinyin_index);
            }
#if DEBUG_PINYIN
                File.AppendAllText(Directory.GetCurrentDirectory() + @"\Debug_Pinyin.txt", unicode + chr + "\n");
#endif
            return null;
        }        
        
        static public string C2P(char HighSurrogate, char LowSurrogate, bool format, ref int index, int pinyin_index = 0)
        {
            index += 1;
            string unicode = GetUNICODE(HighSurrogate, LowSurrogate);
            if (PinyinTable.ContainsKey(unicode))
            {
                if(format)
                    return PinyinFormatter.AppendToneNumber(SGet(PinyinTable[unicode], pinyin_index));
                else
                    return SGet(PinyinTable[unicode], pinyin_index);
            }
#if DEBUG_PINYIN
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Debug_Pinyin.txt", unicode + GetCHAR(unicode) + "\n");
#endif
            return null;
        }

        
        public static int ExtractToneNumber(char chr, int pinyin_index = 0)
        {
            string UNICODE = GetUNICODE(chr);
            if (PinyinTable.ContainsKey(UNICODE))
            {
                return PinyinFormatter.Disintegrate(SGet(PinyinTable[UNICODE], pinyin_index));
            }
            return -1;
        }

        public static int ExtractToneNumber(char HighSurrogate, char LowSurrogate, int pinyin_index = 0)
        { 
            string UNICODE = GetUNICODE(HighSurrogate, LowSurrogate);
            if (PinyinTable.ContainsKey(UNICODE))
            {
                return PinyinFormatter.Disintegrate(SGet(PinyinTable[UNICODE], pinyin_index));
            }
            return -1;
        }

        public static bool IsValid(char text)
        {
            return ((text >= 0x4E00 && text <= 0x9FFF) ||
                (text >= 0x3400 && text <= 0x4DBF) ||
                (text >= 0x3400 && text <= 0x4DBF) ||
                (text >= 0x20000 && text <= 0x2CEAF) ||
                (text >= 0x2E80 && text <= 0x31EF) ||
                (text >= 0xF900 && text <= 0xFAFF) ||
                (text >= 0xFE30 && text <= 0xFE4F) ||
                (text >= 0xF2800 && text <= 0x2FA1F) || char.IsHighSurrogate(text)) &&
                !((text >= 0xFF01 && text <= 0xFF1F) || (text >= 0x3000 && text <= 0x303F));
            //return ();
        }

        static public string GetUNICODE(char chr)
        {
            return ((int)chr).ToString("X");
        }        
        
        static public string GetUNICODE(char HighSurrogate, char LowSurrogate)
        {
            return char.ConvertToUtf32(HighSurrogate, LowSurrogate).ToString("X");
        }

        static public string GetCHAR(string unicode)
        {
            return char.ConvertFromUtf32(int.Parse(unicode, System.Globalization.NumberStyles.HexNumber));
        }

        public static string SGet(string chr, int index)
        {
            int startIndex = 0;
            int endIndex = -1;
            int count = 0;
            for (int i = 0; i <= chr.Length; i++)
            {
                if (i == chr.Length || chr[i] == ',')
                {
                    startIndex = endIndex + 1;
                    endIndex = i;
                    if (count == index)
                    {
                        return chr.Substring(startIndex, endIndex - startIndex);
                    }
                    count++;
                }
            }
            throw new Exception("Pinyin with the given index can't be found for " + chr + " Index : " + index);
        }

    }
}
