using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

public class eventnode  //表示单个命题，可能是规则中的前件，也可能是后件
{
    public string name;        //命题名
    public int eventcount;     //该命题的编号
    public bool terminal_node = false; //判断是否是终点节点(即该节点已经没有后件)
    public eventnode next = null;//下一个命题
}


public class autenode   //前件节点，表示规则中的前件中的一项
{
    public string name = null; //命题名
    public int eventcount;     //该命题的编号
    public autenode next = null;      //同规则中前件的下一项
}

public class rulenode   //单条规则
{
    public string name = null;      //后件名
    public int eventcount;          //该后件命题的编号
    public int rulecount;           //规则编号
    public bool terminal = false;
    public rulenode next = null;    //下一条规则
    public autenode first = new autenode();     //该规则中的前件链表表头
}

public class rule_cnode
//推理中用到的，c指copy,是对源规则的位置进行拷贝，据此建立规则库的open表/close表
{
    public rulenode rule_source;        //该规则的源，用来获取规则数据
    public rule_cnode next = null;      //该规则库的open表/close表的下一节点
}

namespace 产生式系统
{
    public class method
    {
        public static eventnode ehead = new eventnode();
        public static eventnode etail = ehead;
        public static rulenode rhead = new rulenode();
        public static rulenode rtail = rhead;
        public static int eventcount = 0;
        public static int rulecount = 0;
        public static DataTable dt = new DataTable();

        public static eventnode isevent_exist(string t)
        //判断命题是否已经存在，是则返回其在命题表中的位置，否则返回null
        {
            bool signal_exist1 = false;
            eventnode enode_temp = ehead;
            while (enode_temp.next != null)
            {
                enode_temp = enode_temp.next;
                if (enode_temp.name == t)
                {
                    signal_exist1 = true;
                    break;
                }
            }
            if (signal_exist1)
                return enode_temp;
            else return null;
        }

        public static void Init()
        {
            StreamReader sr = new StreamReader("rule_group.txt", Encoding.UTF8);
            dt.Columns.Add("规则编号", typeof(int));
            dt.Columns.Add("规则前件", typeof(string));
            dt.Columns.Add("规则后件", typeof(string));
            while (!sr.EndOfStream)
            {
                rulecount++;
                rtail.next = new rulenode();
                rtail = rtail.next;
                DataRow row = dt.NewRow();
                row[0] = rulecount;
                string s = sr.ReadLine();
                int count = 0;
                char a = s[count];
                bool terminal = false;
                if (a == '?')
                {
                    terminal = true;
                    rtail.terminal = true;
                    count++;
                }
                int head = count, length = s.Length;
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
                        if (!signal1)     //新建命题节点
                        {
                            eventcount++;
                            enode_temp1.next = new eventnode();
                            enode_temp1 = enode_temp1.next;
                            enode_temp1.name = t;
                            enode_temp1.eventcount = eventcount;
                            etail = enode_temp1;
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
                            string aute_temp = string.Empty;
                            if (!terminal)
                                aute_temp = s.Substring(0, count - 1);
                            else aute_temp = s.Substring(1, count - 2);
                            row[1] = aute_temp;
                            count++;
                        }
                        head = count;
                        a = s[count];
                    }
                }

                t = s.Substring(head, count - head);
                row[2] = t;
                dt.Rows.Add(row);

                eventnode enode_temp2 = ehead;
                bool signal2 = false;
                while (enode_temp2.next != null)
                {
                    enode_temp2 = enode_temp2.next;
                    if (enode_temp2.name == t)
                    {
                        if (terminal)
                            enode_temp2.terminal_node = true;
                        signal2 = true;
                        break;
                    }
                }
                if (!signal2)     //新建事件节点
                {
                    eventcount++;
                    enode_temp2.next = new eventnode();
                    enode_temp2 = enode_temp2.next;
                    enode_temp2.name = t;
                    enode_temp2.eventcount = eventcount;
                    if (terminal)
                        enode_temp2.terminal_node = true;
                    etail = enode_temp2;
                }
                rtail.name = t;
                rtail.eventcount = enode_temp2.eventcount;
                rtail.rulecount = rulecount;
            }
            sr.Close();
            sr.Dispose();
            Form1.form1.dataGridView1.DataSource = dt;
        }

        public static bool Addrule(string t1, string t2)        //添加规则,成功添加返回true
        {
            bool terminal = false;
            int eventcount_temp = 0;
            eventnode enode_temp = isevent_exist(t2);

            if (enode_temp == null)
            {
                if (MessageBox.Show("后件\"" + t2 + "\"不在命题库中，是否要添加新的命题?", "出了一点小问题！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    eventcount++;
                    etail.next = new eventnode();
                    etail = etail.next;
                    etail.name = t2;
                    etail.eventcount = eventcount;
                    if (MessageBox.Show("命题\"" + t2 + "\"是否是一个具体动物？", "确认！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        etail.terminal_node = true;
                        terminal = true;
                    }
                }
                else return false;
            }
            else
            {
                terminal = enode_temp.terminal_node;
                eventcount_temp = enode_temp.eventcount;
            }


            autenode ahead_temp = new autenode();
            autenode anode_temp = ahead_temp;
            int count = 0;
            int head = 0, length = t1.Length;
            char a = t1[count];
            string t = null;
            while (++count != length)
            {
                if (a != '，' && a != ',')
                    a = t1[count];
                else
                {
                    t = t1.Substring(head, count - head - 1);
                    enode_temp = isevent_exist(t);
                    if (enode_temp == null)
                    {
                        if (MessageBox.Show("前件\"" + t + "\"不在命题库中，是否要添加新的命题?", "出了一点小问题！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            eventcount++;
                            etail.next = new eventnode();
                            etail = etail.next;
                            etail.name = t;
                            etail.eventcount = eventcount;
                        }
                        else return false;
                    }
                    else
                    {
                        anode_temp.next = new autenode();
                        anode_temp = anode_temp.next;
                        anode_temp.name = t;
                        anode_temp.eventcount = enode_temp.eventcount;
                    }
                    head = count;
                    a = t1[count];
                }
            }
            t = t1.Substring(head, count - head);
            enode_temp = isevent_exist(t);
            if (enode_temp == null)
            {
                if (MessageBox.Show("前件\"" + t + "\"不在命题库中，是否要添加新的命题?", "出了一点小问题！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    eventcount++;
                    etail.next = new eventnode();
                    etail = etail.next;
                    etail.name = t;
                    etail.eventcount = eventcount;
                }
                else return false;
            }
            else
            {
                anode_temp.next = new autenode();
                anode_temp = anode_temp.next;
                anode_temp.name = t;
                anode_temp.eventcount = enode_temp.eventcount;
            }

            rulecount++;
            rtail.next = new rulenode();
            rtail = rtail.next;
            rtail.name = t2;
            rtail.eventcount = eventcount_temp;
            rtail.rulecount = rulecount;
            rtail.first = ahead_temp;
            rtail.terminal = terminal;

            DataRow row = dt.NewRow();
            row[0] = rulecount;
            row[1] = t1;
            row[2] = t2;
            dt.Rows.Add(row);


            //StreamReader sr = new StreamReader("rule_group.txt");
            //string sr_temp = string.Empty;
            //while (!sr.EndOfStream)
            //    sr_temp = sr.ReadLine();
            //sr.Close();

            StreamWriter sw = new StreamWriter("rule_group.txt", true);
            if (!terminal)
                sw.WriteLine(t1 + "->" + t2);
            else sw.WriteLine("?" + t1 + "->" + t2);

            sw.Flush();
            sw.Close();
            return true;
        }

        public static void RewriteFile()       //在删除规则后，对txt文件重写
        {
            StreamWriter sw = new StreamWriter("rule_group.txt", false);//saOrAp表示覆盖或者是追加  
            rulenode rnode_temp = rhead;
            while (rnode_temp.next != null)
            {
                string str = string.Empty;
                rnode_temp = rnode_temp.next;
                autenode anode_temp = rnode_temp.first;
                while (anode_temp.next != null)
                {
                    anode_temp = anode_temp.next;
                    if (anode_temp.next != null)
                    {
                        str = str + anode_temp.name + ",";
                        continue;
                    }
                    else
                    {
                        str = str + anode_temp.name;
                        break;
                    }
                }
                if (rnode_temp.terminal)
                {
                    sw.WriteLine("?" + str + "->" + rnode_temp.name);
                }
                else sw.WriteLine(str + "->" + rnode_temp.name);
            }
            sw.Close();
        }

        public static void Reasoning(eventnode event_selected_head)
        {
            Reasoning R_method = new Reasoning();
            eventnode event_selected_tail = R_method.get_selected(event_selected_head);
            for (eventnode temp = event_selected_head.next; temp != null; temp = temp.next)
            {
                if (temp.terminal_node)
                {
                    Form2.form2.textBox1.Text = temp.name;
                    return;
                }
            }
            rule_cnode chead = new rule_cnode();
            R_method.get_crule_list(chead);
            //每次循环取一条规则
            for (rule_cnode cnode_temp = chead; cnode_temp.next != null; cnode_temp = cnode_temp.next)
            {
                //判断规则是否成功匹配
                if (R_method.ismatch(cnode_temp.next, event_selected_head))
                {
                    //判断该规则后件是否是终点节点
                    if (R_method.isterminal(cnode_temp.next))
                    {
                        Form2.form2.textBox1.Text = cnode_temp.next.rule_source.name;
                        return;
                    }
                    else

                    {   //非终点节点，则考虑把后件加入事实库
                        eventnode temp = event_selected_head;
                        while (temp.next != null)
                        {
                            temp = temp.next;
                            if (temp.eventcount == cnode_temp.next.rule_source.eventcount)
                                break;
                        }
                        if (temp.next == null)
                        {
                            event_selected_tail.next = new eventnode();
                            event_selected_tail = event_selected_tail.next;
                            event_selected_tail.name = cnode_temp.next.rule_source.name;
                            event_selected_tail.eventcount = cnode_temp.rule_source.eventcount;

                            cnode_temp.next = cnode_temp.next.next;
                        }
                    }
                }
            }
        }
    }
}
