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
        public static eventnode ehead = new eventnode();    //命题表头结点
        public static eventnode etail = ehead;              //命题表尾结点，方便添加新命题
        public static rulenode rhead = new rulenode();      //规则表头结点
        public static rulenode rtail = rhead;               //规则表尾结点，方便他添加新规则
        public static int eventcount = 0;                   //命题计数
        public static int rulecount = 0;                    //规则计数
        public static DataTable dt = new DataTable();       //DataTable dt 绑定到datagridview中，显示规则

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

        public static void Init()       //初始化函数，根据存储规则的文本建立规则表，命题表
        {
            dt.Columns.Add("规则编号", typeof(int));
            dt.Columns.Add("规则前件", typeof(string));
            dt.Columns.Add("规则后件", typeof(string));
            //给dt添加新的列

            StreamReader sr = new StreamReader("rule_group.txt", Encoding.UTF8);
            //设置读取流，读入rule_group.txt文件
            while (!sr.EndOfStream)     //若非文件结尾，则循环，每次循环读取一行（一条规则）
            {
                rulecount++;
                rtail.next = new rulenode();
                rtail = rtail.next;
                //规则计数+1，给规则表开辟空间接收新规则

                DataRow row = dt.NewRow();
                //给dt新增一行，用以添加新的规则
                row[0] = rulecount;     //赋规则编号
                string s = sr.ReadLine();   //读取文件的一行
                while (s.Length == 0)       //跳过空行
                    s = sr.ReadLine();
                int count = 0;
                char a = s[count];      //读取一个字符
                bool terminal = false;  //标记后件是否为终点命题(即是否是目标状态)
                eventnode enode_temp = null;
                if (a == '?')
                //规定命题后件是如果终点则在该条规则前加一个"?"，据此可以判断是否是后件终点
                {
                    terminal = true;
                    rtail.terminal = true;
                    count++;
                }
                int head = count, length = s.Length;
                //head是截取字符串s时的截取开始位置
                string t = null;      //t是临时字符串，截取的字符串保存在t中
                while (++count != length)
                {
                    if (a != '-' && a != '，' && a != ',')
                        //读取的字符非分隔符，跳过
                        a = s[count];
                    else
                    {
                        t = s.Substring(head, count - head - 1);
                        //若读取到分隔符，则截取head之后的count-head-1个字符
                        //即截取head到count-1这一段的字符
                        enode_temp = isevent_exist(t);
                        if (enode_temp == null)     //若该命题是一个新的命题，则新建命题节点，添加入命题表中
                        {
                            eventcount++;
                            etail.next = new eventnode();
                            etail = etail.next;
                            etail.name = t;
                            etail.eventcount = eventcount;
                            enode_temp = etail;
                        }

                        //给该规则的前件节点添加新截取的命题
                        autenode anode_temp = rtail.first;
                        while (anode_temp.next != null)
                            anode_temp = anode_temp.next;

                        anode_temp.next = new autenode();
                        anode_temp = anode_temp.next;
                        anode_temp.name = t;
                        anode_temp.eventcount = enode_temp.eventcount;

                        if (a == '-')      //若字符是'-'，则将读取到->，所有前件即将读取完
                        {
                            string aute_temp = string.Empty;
                            //aute_temp字符串截取所有前件的部分形成新的字符串，用来展示规则
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
                //循环结束后head指向后件第一个字符，count指向length，
                //因此截取的是后件的字符串，操作与前件的命题相似
                row[2] = t;
                dt.Rows.Add(row);


                enode_temp = isevent_exist(t);
                if (enode_temp == null)     //若该命题是一个新的命题，则新建命题节点，添加入命题表中
                {
                    eventcount++;
                    etail.next = new eventnode();
                    etail = etail.next;
                    etail.name = t;
                    etail.eventcount = eventcount;
                    if (terminal)
                        etail.terminal_node = true;
                    enode_temp = etail;
                }
                else if (terminal)
                        enode_temp.terminal_node = true;
                rtail.name = t;
                rtail.eventcount = enode_temp.eventcount;
                rtail.rulecount = rulecount;
            }
            sr.Close();
            sr.Dispose();
            Form1.form1.dataGridView1.DataSource = dt;
        }

        public static bool Addrule(string t1, string t2)        //添加规则,成功添加返回true
        {
            bool terminal = false;      //判断该规则的后件是否为终点节点（具体动物）
            int eventcount_temp = 0;    //临时存储后件的命题编号
            eventnode enode_temp = isevent_exist(t2);   //检查命题库中是否有名字为t2的命题，

            if (enode_temp == null)     //为空则表示这是个新命题，须在命题库中添加新命题
            {
                if (MessageBox.Show("后件\"" + t2 + "\"不在命题库中，是否要添加新的命题?", "出了一点小问题！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    eventcount++;
                    etail.next = new eventnode();
                    etail = etail.next;
                    etail.name = t2;
                    etail.eventcount = eventcount;
                    eventcount_temp = eventcount;
                    if (MessageBox.Show("命题\"" + t2 + "\"是否是一个具体动物？", "确认！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        etail.terminal_node = true;
                        terminal = true;
                    }
                }
                else return false;
            }
            else
            {   //非空则表示该命题是已经存在命题库中的，直接读取该命题的终点信息和编号
                terminal = enode_temp.terminal_node;
                eventcount_temp = enode_temp.eventcount;
            }


            autenode ahead_temp = new autenode();   //前件链表的头节点，待链表构建完毕后直接将头结点插入规则中
            autenode anode_temp = ahead_temp;       //临时节点，用于构建链表

            //与初始化函数中的原理类似，读取字符串t1，然后根据规则构建前件链表
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

            //while运行完毕后，count指向字符串长度length最后一个字符，head指向前一个前件的末尾
            //即最后一个前件还没有读取到（因为读取前件是以分界符','为准，而最后一个前件后不接','，因此需要额外对最后一个前件处理
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

            //前件、后件都处理完毕后，新建规则节点
            rulecount++;
            rtail.next = new rulenode();
            rtail = rtail.next;
            rtail.name = t2;    //后件名
            rtail.eventcount = eventcount_temp;     //后件编号
            rtail.rulecount = rulecount;            //规则编号
            rtail.first = ahead_temp;               //前件链表
            rtail.terminal = terminal;              //终点标记

            DataRow row = dt.NewRow();              //在DataTable dt中添加新行
            row[0] = rulecount;
            row[1] = t1;
            row[2] = t2;
            dt.Rows.Add(row);

            //在数据文件中写入新的规则
            StreamWriter sw = new StreamWriter("rule_group.txt", true);
            sw.WriteLine();     //跳一行，防止当前光标位于最后一条规则那一行
            if (!terminal)
                sw.WriteLine(t1 + "->" + t2);
            else sw.WriteLine("?" + t1 + "->" + t2);

            sw.Flush();
            sw.Close();
            return true;
        }

        public static void RewriteFile()       //在删除规则后，对txt文件重写
        {
            StreamWriter sw = new StreamWriter("rule_group.txt", false);    //false表示覆盖,即重写文件
            rulenode rnode_temp = rhead;
            while (rnode_temp.next != null)     //循环读取规则，重写文件
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
                    sw.WriteLine("?" + str + "->" + rnode_temp.name);
                else sw.WriteLine(str + "->" + rnode_temp.name);
            }
            sw.Close();
        }

        public static void Reasoning(eventnode event_selected_head)     //推理函数
        {
            Reasoning R_method = new Reasoning();       //调用Reasoning类中的推理方法
            eventnode event_selected_tail = R_method.get_selected(event_selected_head);
            //返回根据窗口中所选规则而构建的一条已选命题链表表尾
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
                    if (cnode_temp.next.rule_source.terminal) //判断该规则后件是否是终点节点
                    {
                        //是则输出该节点，推理成功
                        Form2.form2.textBox1.Text = cnode_temp.next.rule_source.name;
                        return;
                    }
                    else
                    {   //非终点节点，则考虑把后件加入事实库中
                        eventnode temp = event_selected_head;
                        while (temp.next != null)
                        {   //查询该后件是否已经存在事实库，存在则不再重复添加
                            temp = temp.next;
                            if (temp.eventcount == cnode_temp.next.rule_source.eventcount)
                                break;
                        }
                        if (temp.next == null)
                        {
                            event_selected_tail.next = new eventnode();
                            event_selected_tail = event_selected_tail.next;
                            event_selected_tail.name = cnode_temp.next.rule_source.name;
                            event_selected_tail.eventcount = cnode_temp.next.rule_source.eventcount;

                            cnode_temp.next = cnode_temp.next.next;
                        }
                    }
                }
            }
            Form2.form2.textBox1.Text = " 没有结果!";
        }
    }
}
