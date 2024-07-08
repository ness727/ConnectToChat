namespace ChatClient
{
    partial class ChatRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatRoom));
            this.chatFlowBox = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNickname = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chatFlowBox
            // 
            this.chatFlowBox.AutoScroll = true;
            this.chatFlowBox.BackColor = System.Drawing.Color.Transparent;
            this.chatFlowBox.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.chatFlowBox.Location = new System.Drawing.Point(11, 101);
            this.chatFlowBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chatFlowBox.Name = "chatFlowBox";
            this.chatFlowBox.Padding = new System.Windows.Forms.Padding(0, 0, 9, 0);
            this.chatFlowBox.Size = new System.Drawing.Size(380, 489);
            this.chatFlowBox.TabIndex = 4;
            this.chatFlowBox.WrapContents = false;
            // 
            // txtSend
            // 
            this.txtSend.Font = new System.Drawing.Font("경기천년제목 Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSend.Location = new System.Drawing.Point(0, 599);
            this.txtSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(298, 62);
            this.txtSend.TabIndex = 2;
            this.txtSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSend_KeyPress);
            // 
            // txtNickname
            // 
            this.txtNickname.Font = new System.Drawing.Font("Spoqa Han Sans Neo Bold", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtNickname.Location = new System.Drawing.Point(269, 62);
            this.txtNickname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNickname.Multiline = true;
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(88, 26);
            this.txtNickname.TabIndex = 2;
            this.txtNickname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNickname.TextChanged += new System.EventHandler(this.txtNickname_TextChanged);
            this.txtNickname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSend_KeyPress);
            // 
            // btSend
            // 
            this.btSend.BackColor = System.Drawing.Color.PowderBlue;
            this.btSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSend.Font = new System.Drawing.Font("Spoqa Han Sans Neo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btSend.ForeColor = System.Drawing.Color.Black;
            this.btSend.Location = new System.Drawing.Point(297, 599);
            this.btSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(73, 62);
            this.btSend.TabIndex = 3;
            this.btSend.Text = "Send";
            this.btSend.UseVisualStyleBackColor = false;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(138, 57);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblNickname
            // 
            this.lblNickname.AutoSize = true;
            this.lblNickname.Font = new System.Drawing.Font("Spoqa Han Sans Neo Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblNickname.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNickname.Location = new System.Drawing.Point(175, 63);
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(94, 23);
            this.lblNickname.TabIndex = 6;
            this.lblNickname.Text = "Nickname :";
            // 
            // ChatRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(370, 661);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.lblNickname);
            this.Controls.Add(this.chatFlowBox);
            this.Controls.Add(this.txtNickname);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ChatRoom";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Brown;
            this.Text = "ChatRoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatRoom_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel chatFlowBox;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNickname;
    }
}