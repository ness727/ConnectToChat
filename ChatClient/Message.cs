using System;

namespace ChatClient
{
    public class Message
    {
        public int Fd { get; private set; }
        public string Nickname { get; private set; }
        public String IpAddr { get; private set; }
        public int Port { get; private set; }
        public String Body { get; private set; }  // 대화 내용

        public Message(int fd, string nickname, string ipAddr, int port, string body)
        {
            Fd = fd;
            Nickname = nickname;
            IpAddr = ipAddr;
            Port = port;
            Body = body;
        }
    }
}
