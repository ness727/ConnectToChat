using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class ChatRoom : MetroFramework.Forms.MetroForm
    {
        private int MyFd { get; set; } = -1;  // 나의 fd를 저장하는 용도
        ChatManager ChatManager { get; set; } = ChatManager.Instance;  // 채팅 매니저
        public ChatRoom()
        {
            InitializeComponent();
            txtNickname.Text = ChatManager.MyNickname;  // 시작 시 채팅 관리자의 닉네임으로 설정
        }
        public void GetMessage(Message msg)
        {
            if (msg == null) return;  // 메시지가 없으면 리턴
            
            // 닉네임을 처음 설정할 때 채팅 매니저의 딕셔너리에 저장
            if (!ChatManager.NicknameMap.ContainsKey(msg.Fd)) {
                ChatManager.NicknameMap.Add(msg.Fd, txtNickname.Text);
                ChatManager.LabelMap.Add(msg.Fd, new List<Label>());
            }
            else  // 이미 채팅을 친 적이 있는 사용자일 때
            {
                if (!ChatManager.NicknameMap[msg.Fd].Equals(msg.Nickname))  // 닉네임이 변경되었을 때
                {
                    foreach (Label lbl in ChatManager.LabelMap[msg.Fd])  // 해당 사용자의 지난 메시지를 순회
                    {
                        if (MyFd == msg.Fd)  // 나의 메시지일 때
                        {
                            int tempWidth = lbl.Width;  // 이전 레이블의 가로 길이를 저장
                            lbl.Text = msg.Nickname;  // 이전 레이블의 별명 변경
                            lbl.Left += tempWidth - lbl.Width;  // 닉네임 위치를 바뀐 닉네임 기준으로 다시 조절
                        }
                        else // 다른 사람 메시지일 때
                        {
                            lbl.Text = msg.Nickname; // 이전 레이블의 별명 변경
                            lbl.Left = 0;  // 닉네임을 왼쪽으로 배치
                        }
                    }
                }
            }

            // 시간이 표시될 Label을 생성
            Label labelTime = new Label();
            labelTime.Text += DateTime.Now.ToString("tt hh:mm");
            labelTime.AutoSize = true;

            // 대화 내용이 표시될 Label을 설정
            Label label = new Label();
            label.Text += msg.Body;
            label.AutoSize = true;
            label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label.Padding = new Padding(5, 10, 5, 10);
            label.Margin = new Padding(5, 0, 5, 0);
            label.MaximumSize = new Size(150, 100);
            label.Font = new Font("경기천년제목 Bold", 10.2F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
            label.Top = 20;

            // 별병이 표시될 Label을 설정
            Label lblNickname = new Label();
            lblNickname.Text += msg.Nickname;
            lblNickname.AutoSize = true;
            // 닉네임 설정이 안 된 것들( ex) Welcome! )은 등록하지 않음
            if (msg.Nickname != null) ChatManager.LabelMap[msg.Fd].Add(lblNickname);

            // 위의 Label들을 모두 감쌀 Panel을 생성
            Panel panel = new Panel();  // 좌, 우로 나, 다른 사람의 메시지를 구분하기 위해 사용
            panel.Width = chatFlowBox.Width - chatFlowBox.Width / 10;

            // panel에 다른 Label을을 추가함
            panel.Controls.Add(label);
            panel.Controls.Add(labelTime);
            panel.Controls.Add(lblNickname);

            // 시간 label의 위치를 조정
            labelTime.Top =  label.Top + label.Height - labelTime.Height;

            using (Graphics g = label.CreateGraphics())  // 메시지가 길어지면 잘려 보이는 현상 해결
            {
                // 실제 화면에 그려지는 폰트의 크기를 가지고 계산
                SizeF stringSize = g.MeasureString(label.Text, label.Font, label.Width);
                int calculatedHeight = (int)Math.Ceiling(stringSize.Height);

                label.Height = calculatedHeight > label.Font.Height ? calculatedHeight : label.Font.Height;
                panel.Height = label.Height + 30;
            }

            MyFd = (MyFd == -1 ? msg.Fd : MyFd);  // 처음 Welcome 메시지를 받을 때 받은 fd로 자신인지 저장
            if (MyFd == msg.Fd)  // 나의 메시지일 때
            {
                // Left 관련은 모두 위치 조절용
                labelTime.Left = panel.Width - label.Width - labelTime.Width - 3;
                label.Left = panel.Width - label.Width;  // 왼쪽으로 공간을 두어 오른쪽으로 배치
                lblNickname.Left = panel.Width - lblNickname.Width;
                label.BackColor = Color.PowderBlue;
            }
            else  // 다른 사람의 메시지일 때
            {
                labelTime.Left = label.Width + 3;
                label.BackColor = Color.White;
            }
            chatFlowBox.Controls.Add(panel);  // 모든 panel을 관리하는 FlowBox에 panel을 추가함
            // flowBox의 마지막 요소로 이동 - 스크롤이 있을 때 입력한 메시지를 화면에 보이기 위함
            chatFlowBox.Controls[chatFlowBox.Controls.Count - 1].Focus();
        }
        public void Send()
        {
            if (ChatManager.Instance.TcpClient == null || ChatManager.Instance.TcpClient.Connected == false)
            {
                return;
            }

            // 서버 측에서 |를 기준으로 별명과 내용을 구분하게 하기 위하여 포함하여 보냄
            StringBuilder sb = new StringBuilder(txtNickname.Text);
            sb.Append("|").Append(txtSend.Text);

            byte[] data = Encoding.Default.GetBytes(sb.ToString());
            ChatManager.Instance.TcpClient.Client.Send(data);
            txtSend.Text = "";  // 보낸 후에는 입력했던 내용을 비움
        }
        private void btSend_Click(object sender, EventArgs e)
        {
            if (txtSend.Text != "") Send();  // 입력한 내용이 있어야 서버로 전달
        }

        private void ChatRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 채팅 폼을 직접 닫을 때 실행
            ChatManager.Instance.FormMain.Disconnect();
        }

        // 엔터키로 메시지 전송
        private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtSend.Text != "") Send();  // 입력한 내용이 있어야 서버로 전달
                e.Handled = true;  // 이벤트가 처리되었음을 알림
            }
        }

        private void txtNickname_TextChanged(object sender, EventArgs e)
        {
            // 별명 입력을 다르게 했을 때 채팅 매니저의 별명을 갱신
            ChatManager.MyNickname = txtNickname.Text;
        }
    }
}
