using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PinyinC;

namespace Temp_Util
{
    public partial class Form1 : Form
    {
        public string[] s = null;

        public string[] s_b = null;

        public int index = 0;

        public Form1()
        {
            InitializeComponent();
            s_b = s = File.ReadAllLines(Application.StartupPath + @"/pinyindb/unicode_to_hanyu_pinyin.txt");
            index = FindNext(0);
            display(index);
            //PinyinC.PinyinC.initializeResource();
        }

        public int FindNext(int start)
        {
            int i = start;
            while (!s[i].Contains(',') && i < s.Length)
            {
                i++;
            }
            return i;
        }

        public int FindBack(int start)
        {
            int i = start;
            while (!s[i].Contains(',') && i > 1)
            {
                i--;
            }
            return i;
        }

        public void display(int i)
        {

            label1.Text = !int.TryParse(s[i].Substring(0, s[i].IndexOf(' ')), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int _) ? s[i].Substring(0, s[i].IndexOf(' ')) : PinyinC.PinyinC.GetCHAR(s[i].Substring(0, s[i].IndexOf(' ')));
            textBox1.Text = s[i].Substring(s[i].IndexOf(' ') + 1, s[i].Length - s[i].IndexOf(' ') - 1);
            label2.Text = "TOTAL RESULTS:" + s.Length.ToString() + ", CURRENT:" + index;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            index = FindNext(index + 1);
            display(index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index = FindBack(index - 1);
            display(index);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure about that?", "Hye", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.WriteAllLines(Application.StartupPath + @"/pinyindb/unicode_to_hanyu_pinyin.txt", s);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            s[index] = s[index].Substring(0, s[index].IndexOf(' ') + 1) + textBox1.Text; 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure about that?", "Hye", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                s[index] = s_b[index];
                display(index);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i = -1;
            if (PinyinC.PinyinC.IsValid(textBox2.Text[0]))
            {
                string tmp = (textBox2.Text.Length > 1 ? PinyinC.PinyinC.GetUNICODE(textBox2.Text[0], textBox2.Text[1]) : PinyinC.PinyinC.GetUNICODE(textBox2.Text[0])).ToUpper();
                i = Array.FindIndex<string>(s, d => d.Contains(tmp));
                if (i == -1)
                {
                    MessageBox.Show("Not found");
                    return;
                }
            }
            else
            {
                i = Array.FindIndex<string>(s, d => d.Contains(textBox2.Text));
                if (i == -1)
                {
                    MessageBox.Show("Not found");
                    return;
                }
            }
            index = i;
            display(i);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label1.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MessageBox.Show((chr.Length == 1 ? char.ConvertToUtf32(chr, 0) : char.ConvertToUtf32(chr[0], chr[1])).ToString("X"));
            textBox3.Text = char.IsHighSurrogate(textBox3.Text[0]) ? PinyinC.PinyinC.GetUNICODE(textBox3.Text[0], textBox3.Text[1]) : PinyinC.PinyinC.GetUNICODE(textBox3.Text[0]);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text = char.ConvertFromUtf32(int.Parse(textBox3.Text, System.Globalization.NumberStyles.HexNumber)).ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string tmp = null;
            int start = 0;
            int len = 0;
            int ind = 0;
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = s[i].Replace("(", "").Replace(")", "");
                ind = s[i].IndexOf(' ');
                start = ind + 1;
                if (s[i].IndexOf(',') != -1)
                {
                    for (int j = start; j < s[i].Length - 1; j++)
                    {
                        if (s[i][j] == ',')
                        {
                            len = j - start;
                           // MessageBox.Show("Start : " + start.ToString() + "Len : " + len.ToString());
                            tmp += PinyinFormatter.AppendToneNumber(s[i].Substring(start, len)) + ",";
                            start = j + 1;
                        }
                    }
                    len = s[i].Length - start;
                    //MessageBox.Show("Start : " + start.ToString() + "Len : " + len.ToString());
                    s[i] = s[i].Substring(0, ind + 1) + tmp + PinyinC.PinyinFormatter.AppendToneNumber(s[i].Substring(start, len));
                    tmp = string.Empty;
                }
                else
                {
                    //MessageBox.Show("Start : " + start.ToString() + "Len : " + (s[i].Length - start).ToString());
                    s[i] = s[i].Substring(0, ind + 1) + PinyinC.PinyinFormatter.AppendToneNumber(s[i].Substring(start, s[i].Length - start));
                }
            }
            tmp = null;
            start = 0;
            len = 0;
            ind = 0;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox4.Text = PinyinFormatter.AppendToneNumber(textBox4.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            s = s.Concat( new string[1] { string.Concat(textBox5.Text, " ", textBox6.Text) }).ToArray();
        }
    }
}
