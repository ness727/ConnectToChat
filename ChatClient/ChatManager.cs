using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows.Forms;
namespace ChatClient
{
    public class ChatManager
    {
        private ChatManager() { }  // 다른 곳에서 생성하지 못하도록 하기 위함

        private static ChatManager instance;
        public static ChatManager Instance  // 싱글턴으로 구현
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChatManager();
                }
                return instance;
            }
        }

        public CFormMain FormMain { get; set; }  // 메인 폼
        public ChatRoom Room { get; set; }  // 채팅방 폼
        public TcpClient TcpClient { get; set; }  // 메인 폼의 클라이언트 연결 상태
        public Boolean IsConnected { get; set; } = false;  // 연결 상태
        public String MyNickname { get; set; } = "익명의 사용자";  // 자신의 닉네임
        // fd를 key로 이용하여 닉네임을 관리하는 용도
        public Dictionary<int, String> NicknameMap { get; private set; } = new Dictionary<int, String>();
        // fd를 key로 이용하여 유저 별 Label 리스트를 관리하는 용도
        public Dictionary<int, List<Label>> LabelMap { get; private set; } = new Dictionary<int, List<Label>>();
    }
}
