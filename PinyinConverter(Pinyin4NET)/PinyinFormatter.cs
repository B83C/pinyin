using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinyinC
{
    public class PinyinFormatter
    {
        private const String allUnmarkedVowelStr = "aeiouv";
        private const String allMarkedVowelStr = "āáăà-ēéĕè-īíĭìiōóŏò-ūúŭù-ǖǘǚǜü-ńňǹ-";

        private static int CIE(char c)
        {
            for (int i = 1; i < 6; i++)
            {
                if (c == allUnmarkedVowelStr[i])
                    return i;
            }
            return -1;
        }

        public static int Disintegrate(string chr)
        {
            for (int i = 0; i < chr.Length; i++)
            {
                int found = allMarkedVowelStr.IndexOf(chr[i]);
                if (found == -1) continue;
                return (found + 1) % 5; 
            }
            return -1;
        }

        public static string AppendToneNumber(string chr)
        {
            int tuneNumber = chr[chr.Length - 1] - '0';
            int indexOfA = chr.IndexOf('a');
            int indexOfE = chr.IndexOf('e');
            int ouIndex = chr.IndexOf("ou");
            int nIndex = chr.IndexOf("n");
            int ind = -1;
            int tobechanged = -1;

            if (-1 != indexOfA)
            {
                tobechanged = indexOfA;
                ind = 0;
            }
            else if (-1 != indexOfE)
            {
                tobechanged = indexOfE;
                ind = 1;
            }
            else if (-1 != ouIndex)
            {
                tobechanged = ouIndex;
                ind = 3;
            }
            else
            {
                for (int i = chr.Length - 2; i >= 0; i--)
                {
                   // System.Windows.Forms.MessageBox.Show(chr[i].ToString());
                    int x = CIE(chr[i]);
                    if (x != -1)
                    {
                        tobechanged = i;
                        ind = x;
                        goto SKIP;
                    }
                }
                if (nIndex != -1)
                {
                    tobechanged = nIndex;
                    ind = 6;
                }
            }
            if (tobechanged == -1)
            {

                System.Windows.Forms.MessageBox.Show("Error occured while trying to add tone number to " + chr, "Error");
                return chr;
            }
            SKIP:
            char markedVowel = allMarkedVowelStr[ind * 5 + tuneNumber - 1];

            string mod = chr.Replace(chr[tobechanged], markedVowel).Replace("v", "ü").Substring(0, chr.Length - 1);

            return mod;
        }
    }
}
