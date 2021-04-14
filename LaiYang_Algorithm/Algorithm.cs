using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaiYang_Algorithm
{
    public class Algorithm
    {
        List<Process> Processes = new List<Process>();
        List<Channel> C12 = new List<Channel>();
        List<Channel> C21 = new List<Channel>();
        int totalTimeSlots;
        List<LocalState> LStates = new List<LocalState>();
        int RedInitiatorPId;
        int RedInitiatorTimeSlot;

        public void Execute()
        {
           for(int i=1;i<=totalTimeSlots;i++)
            {
                foreach(var channel in C12)
                {
                    if(channel.SentTime == i)
                    {
                        SendMessage(Processes.Where(x => x.ProcessId == 1).FirstOrDefault(), channel);
                    }
                    if(channel.ReceivedTime == i)
                    {
                        ReceiveMessage(Processes.Where(x => x.ProcessId == 1).FirstOrDefault(), channel);
                    }
                }
            }
        }

        public void SendMessage(Process process, Channel channel)
        {
            if(process.ColorCode == ConsoleColor.Red)
            {
                SaveState(process, channel);
            }
            else
            {
                process.Balance = process.Balance - channel.Value;
            }
        }

        public void SaveState(Process process, Channel channel)
        {
            LocalState state = new LocalState();
            state.balance = process.Balance;
            state.processid= process.ProcessId;
            LStates.Add(state);
        }

        public void ReceiveMessage(Process process, Channel channel)
        {
            if(process.ColorCode == ConsoleColor.Red)
            {
                SaveState(process, channel);
                
            }
            else
            {
                process.Balance = process.Balance + channel.Value;
            }
        }
    }
}
