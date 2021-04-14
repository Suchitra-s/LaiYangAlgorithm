using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaiYang_Algorithm
{
    public class Message
    {
        public int MessageId { get; set; }

        public int SendingProcessId { get; set; }

        public int ReceivingProcessId { get; set; }
        public int SentTime { get; set; }
        public int Value { get; set; }
        public int ReceivedTime { get; set; }
    }
}
