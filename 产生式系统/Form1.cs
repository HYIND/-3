using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 产生式系统
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("rule_group.txt", Encoding.UTF8);
            int linecount = 0;
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                int count = 0;
                int head = 0, length = s.Length;
                char a = s[count];
                string t = null;
                while (++count != length)
                {
                    if (a != '-' && a != '>' && a != '，' && a != ',')
                    {
                        a = s[count];
                    }
                    else if (a == '，' || a == ',')
                    {

                        t = s.Substring(head, count - head - 1);
                        head = count;
                        a = s[count];
                    }
                    else if (a == '-')
                    {
                        string aute_temp = s.Substring(0, count - 1);
                        listView1.Items.Add(aute_temp);
                        t = s.Substring(head, count - head - 1);
                        count++;
                        head = count;
                        a = s[count];
                    }
                }
                t = s.Substring(head, count - head);
                listView1.Items[linecount].SubItems.Add(t);
                linecount++;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
