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
        {
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
        {
            eventnode temp = event_selected_head;
            for (int i = 0; i < Form2.form2.dataGridView2.Rows.Count; i++)
            {
                for (eventnode search = method.ehead.next; search != null; search = search.next)
                    if (search.eventcount == Convert.ToInt32(Form2.form2.dataGridView2.Rows[i].Cells[0].Value))
                    {
                        temp.next = new eventnode();
                        temp = temp.next;
                        temp.name = search.name;
                        temp.eventcount = search.eventcount;
                        temp.terminal_node = search.terminal_node;
                    }
            }
            return temp;
        }

        public bool ismatch(rule_cnode cnode, eventnode head)
        {
            autenode aute_temp = cnode.rule_source.first.next;
            while (aute_temp != null)
            {
                for (eventnode temp = head.next; temp != null; temp = temp.next)
                {
                    bool exist = false;
                    if (aute_temp.eventcount == temp.eventcount)
                    {
                        exist = true;
                        aute_temp = aute_temp.next;
                        break;
                    }
                    if (!exist)
                        return false;
                    else aute_temp = aute_temp.next;
                }
            }
            return true;
        }

        public bool isterminal(rule_cnode cnode)
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
