using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

public class eventnode  //表示单个事件，可能是规则中的前件，也可能是后件
{
    public string name;        //事件名
    public int eventcount;     //该事件的编号
    public eventnode next = null;//下一个事件
}


public class autenode   //前件节点，表示规则中的前件中的一项
{
    public string name = null; //事件名
    public int eventcount;     //该事件的编号
    public bool open = false;  //表示该事件是否被推出，推出则为true
    public autenode next = null;      //同规则中前件的下一项
}

public class rulenode   //单条规则
{
    public string name = null;        //事件名
    public int eventcount;     //该事件的编号
    public int rulecount;       //规则编号
    public rulenode next = null; //下一条规则
    public autenode first = new autenode();     //该规则中的前件链表表头
}

namespace 产生式系统
{
    public class method
    {
        static eventnode ehead = new eventnode();
        static rulenode rhead = new rulenode();
        static rulenode rtail = rhead;

        public static void Init()
        {
            StreamReader sr = new StreamReader("rule_group.txt", Encoding.UTF8);
            int eventcount = 1;
            int linecount = 0;
            int rulecount = 1;
            while (!sr.EndOfStream)
            {
                rtail.next = new rulenode();
                rtail = rtail.next;
                Form1.form1.listView1.Items.Add(rulecount.ToString());
                string s = sr.ReadLine();
                int count = 0;
                int head = 0, length = s.Length;
                char a = s[count];
                string t = null;
                while (++count != length)
                {
                    if (a != '-' && a != '，' && a != ',')
                        a = s[count];
                    else
                    {
                        t = s.Substring(head, count - head - 1);
                        eventnode enode_temp1 = ehead;
                        bool signal1 = false;
                        while (enode_temp1.next != null)
                        {
                            enode_temp1 = enode_temp1.next;
                            if (enode_temp1.name == t)
                            {
                                signal1 = true;
                                break;
                            }
                        }
                        if (!signal1)     //新建事件节点
                        {
                            enode_temp1.next = new eventnode();
                            enode_temp1 = enode_temp1.next;
                            enode_temp1.name = t;
                            enode_temp1.eventcount = eventcount;
                            eventcount++;
                        }

                        autenode anode_temp = rtail.first;
                        while (anode_temp.next != null)
                            anode_temp = anode_temp.next;
                        anode_temp.next = new autenode();
                        anode_temp = anode_temp.next;
                        anode_temp.name = t;
                        anode_temp.eventcount = enode_temp1.eventcount;

                        if (a == '-')
                        {
                            string aute_temp = s.Substring(0, count - 1);
                            Form1.form1.listView1.Items[linecount].SubItems.Add(aute_temp);
                            count++;
                        }
                        head = count;
                        a = s[count];
                    }
                }

                t = s.Substring(head, count - head);
                Form1.form1.listView1.Items[linecount].SubItems.Add(t);

                eventnode enode_temp2 = ehead;
                bool signal2 = false;
                while (enode_temp2.next != null)
                {
                    enode_temp2 = enode_temp2.next;
                    if (enode_temp2.name == t)
                    {
                        signal2 = true;
                        break;
                    }
                }
                if (!signal2)     //新建事件节点
                {
                    enode_temp2.next = new eventnode();
                    enode_temp2 = enode_temp2.next;
                    enode_temp2.name = t;
                    enode_temp2.eventcount = eventcount;
                    eventcount++;
                }

                rtail.name = t;
                rtail.eventcount = enode_temp2.eventcount;
                rtail.rulecount = rulecount;

                linecount++;
                rulecount++;
            }
        }
    }
}
