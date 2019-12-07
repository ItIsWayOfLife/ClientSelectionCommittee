using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSelectionCommittee
{
    class GetTcpClient
    {
        public static TcpClient GetTcpClient_
        {
            get
            {
                return new TcpClient("127.0.0.1", 1234);
            }
        }
            
    }
}
