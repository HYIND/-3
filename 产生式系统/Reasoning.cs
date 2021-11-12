using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 产生式系统
{
    public class Reasoning
    {
        public void get_crule_list(rule_cnode cnode)
        //c为copy
        {   //构建一条指向规则的链表，用于推理，rule_cnode中存储指向该规则信息的变量source
            rulenode snode = method.rhead.next; //s为source
            while (snode != null)
            {
                cnode.next = new rule_cnode();
                cnode = cnode.next;
                cnode.rule_source = snode;
                snode = snode.next;
            }
        }

        public eventnode get_selected(eventnode event_selected_head)    
            //传入参数为已选规则链表表头，该方法用于根据已选规则构建一条链表
        {
            eventnode temp = event_selected_head;
            for (int i = 0; i < Form2.form2.dataGridView2.Rows.Count; i++)  //逐行读取已选列表中的第一列（编号）
            {
                for (eventnode search = method.ehead.next; search != null; search = search.next)    //查找该编号在命题库中的信息
                    if (search.eventcount == Convert.ToInt32(Form2.form2.dataGridView2.Rows[i].Cells[0].Value))
                    {       //找到后，构建已选链表信息
                        temp.next = new eventnode();
                        temp = temp.next;
                        temp.name = search.name;
                        temp.eventcount = search.eventcount;
                        temp.terminal_node = search.terminal_node;
                    }
            }
            return temp;    //返回表尾
        }

        public bool ismatch(rule_cnode cnode, eventnode head)   //匹配规则，若规则匹配成功则返回true
        {
            autenode aute_temp = cnode.rule_source.first.next;
            while (aute_temp != null)
            {
                bool exist = false;
                for (eventnode temp = head.next; temp != null; temp = temp.next)
                {
                    if (aute_temp.eventcount == temp.eventcount)
                    {
                        exist = true;
                        temp = temp.next;
                        break;
                    }
                }
                if (!exist)
                    return false;
                else aute_temp = aute_temp.next;
            }
            return true;
        }


        public bool isterminal(rule_cnode cnode)       //查询
        {
            int i = cnode.rule_source.eventcount;
            for (eventnode temp = method.ehead.next; temp != null; temp = temp.next)
            {
                if (temp.eventcount == i)
                {
                    if (temp.terminal_node)
                        return true;
                    else return false;
                }
            }
            return false;
        }
    }
}
