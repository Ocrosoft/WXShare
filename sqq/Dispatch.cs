using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WXShare.sqq
{
    public class Dispatch
    {
        public static Dictionary<string, Object> execute(int maxTask = 1)
        {
            Dictionary<string, Object> ret = new Dictionary<string, Object>();
            ret["error"] = false;
            ret["message"] = string.Empty;
            var sendingList =  new List<string>();

            // ordered by submit time descend.
            var problemsNeedSend = Database.GetsProblemSaved();
            // ordered by register time ascend.
            var senders = Database.GetsSender();
            senders.Sort(delegate (Database.Sender left, Database.Sender right)
            {
                // left is full task, but right not, so right first.
                if(left.sending == maxTask && right.sending != maxTask)
                {
                    return 1; // positive, right first.
                }
                // right is full task, but left not, so left first.
                else if(left.sending != maxTask && right.sending == maxTask)
                {
                    return -1; // negative, left first.
                }
                // sender who send slower first(rate smaller first).
                else
                {
                    return (right.sendCost / right.sent).CompareTo(left.sendCost / left.sent);
                }
            });

            var dispatchCount = Math.Min(problemsNeedSend.Count, senders.Count);
            for(int i=0;i<dispatchCount;i++)
            {
                // this task will dispatch to this sender.
                var task = problemsNeedSend[i];
                var sender = senders[i];

                // this sender and following are full task.
                if(sender.sending >= maxTask)
                {
                    break;
                }

                // add to sending.
                if(Database.AddProblemSending(task.team_id, task.num, sender.open_id))
                {
                    if(Database.DeleteProblemSaved(task.team_id, task.num))
                    {
                        sendingList.Add(task.team_id + " " + (char)(task.num + 'A') + "题 分配给" + sender.name);
                    }
                    else // delete failed, cancel previous add.
                    {
                        Database.DeleteProblemSending(task.team_id, task.num);
                        ret["error"] = true;
                        ret["message"] = "An error has occured, can't delete problem from saved.";
                        ret["detail"] = "problem(" + task.team_id + "," + task.num + ")";
                        return ret;
                    }
                }
                else
                {
                    ret["error"] = true;
                    ret["message"] = "An error has occured, can't add problem to sending.";
                    ret["detail"] = "problem(" + task.team_id + "," + task.num + "),sender(" + sender.open_id + ")";
                    return ret;
                }
            }
            ret["sendingList"] = sendingList;
            return ret;
        }
    }
}
