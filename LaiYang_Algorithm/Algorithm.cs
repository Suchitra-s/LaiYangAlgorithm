using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LaiYang_Algorithm
{
    public class Algorithm
    {
        public List<Process> Processes = new List<Process>();
        private List<Channel> C12 = new List<Channel>();
        private List<Channel> C21 = new List<Channel>();
        public int totalTimeSlots;
        public List<LocalState> LStates = new List<LocalState>();
        public int RedInitiatorPId;
        public int RedInitiatorTimeSlot;
        public int TotalP1Sent, TotalP1Received, TotalP2Sent, TotalP2Received, globalState;
        public int RedMessageId = -1;

        //public void Initialize()
        //{
        //    int count = 0;
        //    // Process Values

        //    Process p1 = new Process();
        //    p1.ProcessId = 1;
        //    p1.InitialValue = 800;
        //    p1.Balance = 800;
        //    p1.ColorCode = ConsoleColor.White;

        //    Process p2 = new Process();
        //    p2.ProcessId = 2;
        //    p2.InitialValue = 200;
        //    p2.Balance = 200;
        //    p2.ColorCode = ConsoleColor.White;

        //    Processes.Add(p1);
        //    Processes.Add(p2);


        //    totalTimeSlots = 7;
        //    RedInitiatorPId = 1;
        //    RedInitiatorTimeSlot = 4;

        //    // Message Values

        //    //m1
        //    Message m1 = new Message();
        //    m1.MessageId = count++;
        //    m1.SendingProcessId = 1;
        //    m1.ReceivingProcessId = 2;
        //    m1.Value = 20;
        //    m1.SentTime = 2;
        //    m1.ReceivedTime = 3;

        //    //m2
        //    Message m2 = new Message();
        //    m2.MessageId = count++;
        //    m2.SendingProcessId = 1;
        //    m2.ReceivingProcessId = 2;
        //    m2.Value = 30;
        //    m2.SentTime = 3;
        //    m2.ReceivedTime = 7;

        //    //m3
        //    Message m3 = new Message();
        //    m3.MessageId = count++;
        //    m3.SendingProcessId = 1;
        //    m3.ReceivingProcessId = 2;
        //    m3.Value = 10;
        //    m3.SentTime = 4;
        //    m3.ReceivedTime = 6;

        //    //m4
        //    Message m4 = new Message();
        //    m4.MessageId = count++;
        //    m4.SendingProcessId = 2;
        //    m4.ReceivingProcessId = 1;
        //    m4.Value = 30;
        //    m4.SentTime = 5;
        //    m4.ReceivedTime = 7;

        //    List<Message> AllMessages = new List<Message>();
        //    AllMessages.Add(m1);
        //    AllMessages.Add(m2);
        //    AllMessages.Add(m3);
        //    AllMessages.Add(m4);
        //    InitializeChannels(AllMessages);

        //}

        public void InitializeChannels(List<Message> messages)
        {
            foreach(var message in messages)
            {
                Channel channel1 = new Channel();
                channel1.MessageId = message.MessageId;
                channel1.Value = message.Value;
                
                if(message.SendingProcessId == 1)
                {
                    channel1.SentReceived = SentReceived.Sent;
                    channel1.time = message.SentTime;
                    C12.Add(channel1);
                }
                if (message.ReceivingProcessId == 1)
                {
                    channel1.SentReceived = SentReceived.Received;
                    channel1.time = message.ReceivedTime;
                    C12.Add(channel1);
                }

                Channel channel2 = new Channel();
                channel2.MessageId = message.MessageId;
                channel2.Value = message.Value;

                if (message.SendingProcessId == 2)
                {
                    channel2.SentReceived = SentReceived.Sent;
                    channel2.time = message.SentTime;
                    C21.Add(channel2);
                }
                if (message.ReceivingProcessId == 2)
                {
                    channel2.SentReceived = SentReceived.Received;
                    channel2.time = message.ReceivedTime;
                    C21.Add(channel2);
                }
            }
        }


        public void Execute()
        {
            try
            {
                for (int i = 1; i <= totalTimeSlots; i++)
                {

                    foreach (var channel in C12)
                    {
                        Process process = Processes.Where(x => x.ProcessId == 1).FirstOrDefault();
                        if (process.ColorCode != ConsoleColor.Red)
                        {
                            if (channel.time == i)
                            {
                                if (RedInitiatorPId == 1 && RedInitiatorTimeSlot == i)
                                {
                                    process.ColorCode = ConsoleColor.Red;
                                    RedMessageId = channel.MessageId;
                                }
                                else
                                if (RedMessageId == channel.MessageId)
                                {
                                    process.ColorCode = ConsoleColor.Red;
                                }
                                if (channel.SentReceived == SentReceived.Sent)
                                {
                                    SendMessage(process, channel);
                                }
                                else
                                {
                                    ReceiveMessage(process, channel);
                                }
                            }
                        }
                    }
                    foreach (var channel in C21)
                    {
                        Process process = Processes.Where(x => x.ProcessId == 2).FirstOrDefault();
                        if (process.ColorCode != ConsoleColor.Red)
                        {
                            if (channel.time == i)
                            {
                                if (RedInitiatorPId == 2 && RedInitiatorTimeSlot == i)
                                {
                                    process.ColorCode = ConsoleColor.Red;
                                    RedMessageId = channel.MessageId;
                                }
                                else
                                if (RedMessageId == channel.MessageId)
                                {
                                    process.ColorCode = ConsoleColor.Red;
                                }
                                if (channel.SentReceived == SentReceived.Sent)
                                {
                                    SendMessage(process, channel);
                                }
                                else
                                {
                                    ReceiveMessage(process, channel);
                                }
                            }
                        }
                    }
                }
                Process p1 = Processes.Where(x => x.ProcessId == 1).FirstOrDefault();
                Console.WriteLine("P1 Local State = " + p1.Balance);
                Process p2 = Processes.Where(x => x.ProcessId == 2).FirstOrDefault();
                Console.WriteLine("P2 Local State = " + p2.Balance);

                Console.WriteLine("P1 Sent - P2 Received = " + (TotalP1Sent - TotalP2Received).ToString());
                Console.WriteLine("P2 Sent - P1 Received = " + (TotalP2Sent - TotalP1Received).ToString());
            }
            catch(Exception)
            {
                MessageBox.Show("unexpected error occured in program. Try again later!");
            }
        }

        private void SendMessage(Process process, Channel channel)
        {
            if(process.ColorCode == ConsoleColor.Red)
            {
                SaveState(process, channel);
            }
            else
            {
                process.Balance = process.Balance - channel.Value;
                UpdateSentReceived(process,channel.Value,SentReceived.Sent);
            }
        }

        private void SaveState(Process process, Channel channel)
        {
            LocalState state = new LocalState();
            state.balance = process.Balance;
            state.processid= process.ProcessId;
            LStates.Add(state);
        }

        private void ReceiveMessage(Process process, Channel channel)
        {
            if(process.ColorCode == ConsoleColor.Red)
            {
                SaveState(process, channel);
                
            }
            else
            {
                process.Balance = process.Balance + channel.Value;
                UpdateSentReceived(process, channel.Value, SentReceived.Received);
            }
        }

        private void UpdateSentReceived(Process process, int value, SentReceived state)
        {
            if(state == SentReceived.Sent && process.ProcessId == 1)
            {
                TotalP1Sent = TotalP1Sent + value;
            }
            else
            if (state == SentReceived.Received && process.ProcessId == 1)
            {
                TotalP1Received = TotalP1Received + value;
            }
            else
            if (state == SentReceived.Sent && process.ProcessId == 2)
            {
                TotalP2Sent = TotalP2Sent + value;
            }
            else
            if (state == SentReceived.Received && process.ProcessId == 2)
            {
                TotalP2Received = TotalP2Received + value;
            }
        }
    }

    public enum SentReceived
    {
        Sent,
        Received
    }
}
