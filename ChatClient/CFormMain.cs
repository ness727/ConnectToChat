using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace ChatClient
{
    public partial class CFormMain : MetroFramework.Forms.MetroForm
    {
        public TcpClient MainTcpClient { get; private set; }
        public NetworkStream MainStream { get; private set; }
        private ChatManager ChatManager { get; set; } = ChatManager.Instance;  // 채팅 관리자
        public CFormMain()
        {
            InitializeComponent();
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";

            if (MainTcpClient == null)
            {
                MainTcpClient = new TcpClient(AddressFamily.InterNetwork);
                IPAddress iPAddress = IPAddress.Parse(txtIpAddress.Text);
                int port = Convert.ToInt32(txtPort.Text);

                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);
                
                try
                {
                    MainTcpClient.Connect(iPEndPoint);
                    btConnect.Text = "Disconnect";
                    btConnect.BackColor = Color.DarkSalmon;

                    timerReceive.Enabled = true;
                    MainStream = MainTcpClient.GetStream();

                    // 연결 처리
                    ChatManager.TcpClient = MainTcpClient;
                    // 새로운 채팅방 form을 생성
                    ChatManager.Room = new ChatRoom();
                    // 채팅 매니저의 메인 폼을 현재 폼으로 설정
                    ChatManager.FormMain = this;
                    // 채팅방의 위치를 현재 폼 옆으로 설정
                    ChatManager.Room.StartPosition = FormStartPosition.Manual;
                    ChatManager.Room.Location = new Point(this.Location.X + this.Width + 10, this.Location.Y);
                    ChatManager.Room.WindowState = FormWindowState.Normal;
                    // 채팅방을 사용자에게 보여줌
                    ChatManager.Room.Show();
                    // 연결 상태를 true로 설정
                    ChatManager.IsConnected = true;
                }
                catch (Exception err)
                {
                    Console.WriteLine("연결 실패 -> " + err);
                    lblStatus.Text = "연결 실패";  // 연결 실패 시 보여줄 문자열
                    MainTcpClient = null;
                }
            }
            else
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            // 채팅 매니저의 IsConnected의 값을 기준으로 종료를 판별
            if (ChatManager.Instance.IsConnected)
            {
                MainStream.Close();
                MainStream.Dispose();
                MainStream = null;

                MainTcpClient.Close();
                MainTcpClient.Dispose();
                MainTcpClient = null;

                btConnect.Text = "Connect";
                btConnect.BackColor = Color.PowderBlue;
                timerReceive.Enabled = false;

                ChatManager.TcpClient = null;
                ChatManager.IsConnected = false;
                ChatManager.Room.Close();
                ChatManager.Room = null;
                ChatManager.NicknameMap.Clear();
                ChatManager.LabelMap.Clear();
            }
        }

        private void timerReceive_Tick(object sender, EventArgs e)
        {
            if (MainTcpClient == null || MainTcpClient.Connected == false || MainStream == null)
            {
                return;
            }

            if (MainStream.DataAvailable == true)
            {
                byte[] bzReceive = new byte[1024];

                MainStream.ReadAsync(bzReceive, 0, 1024);

                string strReceive = Encoding.Default.GetString(bzReceive);

                // 채팅 방으로 보낼 메시지를 설정함
                Message msg = null;
                try
                {
                    // 서버에서 받은 Json을 파싱
                    dynamic parsedStr = JsonConvert.DeserializeObject(strReceive);
                    msg = new Message(
                            Convert.ToInt32(parsedStr.fd),
                            Convert.ToString(parsedStr.nickname),
                            Convert.ToString(parsedStr.ipAddr),
                            Convert.ToInt32(parsedStr.port),
                            Convert.ToString(parsedStr.body)
                        );
                }
                catch (Exception err)
                {
                    Console.WriteLine("Json 파싱 오류 -> " + err);
                }
                // 파싱한 메시지를 채팅방 폼으로 전달
                ChatManager.Room.GetMessage(msg);
            }
        }
    }
}
