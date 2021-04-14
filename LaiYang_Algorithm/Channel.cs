using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaiYang_Algorithm
{
    public class Channel
    {
        public int MessageId { get; set; }

        public SentReceived SentReceived { get; set; }
        public int Value { get; set; }

        public int time { get; set; }
    }
}
