namespace GUI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            components = new System.ComponentModel.Container();
            Label_当前连接数 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            Group_Email = new GroupBox();
            button1 = new Button();
            Input_EmailAddress = new TextBox();
            label1 = new Label();
            button_发送 = new Button();
            Input_发送内容 = new TextBox();
            Group_Article = new GroupBox();
            Button_新建文章 = new Button();
            Input_文章地址 = new TextBox();
            label_文章地址 = new Label();
            Input_作者名 = new TextBox();
            label_作者名 = new Label();
            Input_作者UID = new TextBox();
            label_作者UID = new Label();
            Input_文章名 = new TextBox();
            label_文章名 = new Label();
            Group_Email.SuspendLayout();
            Group_Article.SuspendLayout();
            SuspendLayout();
            // 
            // Label_当前连接数
            // 
            Label_当前连接数.AutoSize = true;
            Label_当前连接数.Location = new Point(12, 20);
            Label_当前连接数.Name = "Label_当前连接数";
            Label_当前连接数.Size = new Size(100, 24);
            Label_当前连接数.TabIndex = 0;
            Label_当前连接数.Text = "当前连接：\r\n";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Group_Email
            // 
            Group_Email.Controls.Add(button1);
            Group_Email.Controls.Add(Input_EmailAddress);
            Group_Email.Controls.Add(label1);
            Group_Email.Controls.Add(button_发送);
            Group_Email.Controls.Add(Input_发送内容);
            Group_Email.Location = new Point(12, 62);
            Group_Email.Name = "Group_Email";
            Group_Email.Size = new Size(457, 528);
            Group_Email.TabIndex = 6;
            Group_Email.TabStop = false;
            Group_Email.Text = "发送自定义邮件";
            // 
            // button1
            // 
            button1.Location = new Point(229, 458);
            button1.Name = "button1";
            button1.Size = new Size(215, 57);
            button1.TabIndex = 10;
            button1.Text = "向所有的用户发送";
            button1.UseVisualStyleBackColor = true;
            // 
            // Input_EmailAddress
            // 
            Input_EmailAddress.Location = new Point(105, 38);
            Input_EmailAddress.Name = "Input_EmailAddress";
            Input_EmailAddress.Size = new Size(339, 30);
            Input_EmailAddress.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 38);
            label1.Name = "label1";
            label1.Size = new Size(82, 24);
            label1.TabIndex = 8;
            label1.Text = "邮箱地址\r\n";
            // 
            // button_发送
            // 
            button_发送.Location = new Point(17, 458);
            button_发送.Name = "button_发送";
            button_发送.Size = new Size(206, 54);
            button_发送.TabIndex = 7;
            button_发送.Text = "发送";
            button_发送.UseVisualStyleBackColor = true;
            // 
            // Input_发送内容
            // 
            Input_发送内容.AcceptsReturn = true;
            Input_发送内容.AcceptsTab = true;
            Input_发送内容.Location = new Point(17, 75);
            Input_发送内容.Multiline = true;
            Input_发送内容.Name = "Input_发送内容";
            Input_发送内容.Size = new Size(427, 377);
            Input_发送内容.TabIndex = 6;
            Input_发送内容.TextChanged += Input_发送内容_TextChanged;
            // 
            // Group_Article
            // 
            Group_Article.Controls.Add(Input_文章名);
            Group_Article.Controls.Add(label_文章名);
            Group_Article.Controls.Add(Button_新建文章);
            Group_Article.Controls.Add(Input_文章地址);
            Group_Article.Controls.Add(label_文章地址);
            Group_Article.Controls.Add(Input_作者名);
            Group_Article.Controls.Add(label_作者名);
            Group_Article.Controls.Add(Input_作者UID);
            Group_Article.Controls.Add(label_作者UID);
            Group_Article.Location = new Point(501, 62);
            Group_Article.Name = "Group_Article";
            Group_Article.Size = new Size(606, 279);
            Group_Article.TabIndex = 7;
            Group_Article.TabStop = false;
            Group_Article.Text = "新建动态";
            // 
            // Button_新建文章
            // 
            Button_新建文章.Location = new Point(22, 202);
            Button_新建文章.Name = "Button_新建文章";
            Button_新建文章.Size = new Size(561, 54);
            Button_新建文章.TabIndex = 16;
            Button_新建文章.Text = "新建";
            Button_新建文章.UseVisualStyleBackColor = true;
            Button_新建文章.Click += Button_新建文章_Click;
            // 
            // Input_文章地址
            // 
            Input_文章地址.Location = new Point(110, 148);
            Input_文章地址.Name = "Input_文章地址";
            Input_文章地址.Size = new Size(473, 30);
            Input_文章地址.TabIndex = 15;
            // 
            // label_文章地址
            // 
            label_文章地址.AutoSize = true;
            label_文章地址.Location = new Point(22, 148);
            label_文章地址.Name = "label_文章地址";
            label_文章地址.Size = new Size(82, 24);
            label_文章地址.TabIndex = 14;
            label_文章地址.Text = "文章地址";
            // 
            // Input_作者名
            // 
            Input_作者名.Location = new Point(110, 75);
            Input_作者名.Name = "Input_作者名";
            Input_作者名.Size = new Size(473, 30);
            Input_作者名.TabIndex = 13;
            // 
            // label_作者名
            // 
            label_作者名.AutoSize = true;
            label_作者名.Location = new Point(22, 78);
            label_作者名.Name = "label_作者名";
            label_作者名.Size = new Size(64, 24);
            label_作者名.TabIndex = 12;
            label_作者名.Text = "作者名";
            // 
            // Input_作者UID
            // 
            Input_作者UID.Location = new Point(110, 112);
            Input_作者UID.Name = "Input_作者UID";
            Input_作者UID.Size = new Size(473, 30);
            Input_作者UID.TabIndex = 11;
            // 
            // label_作者UID
            // 
            label_作者UID.AutoSize = true;
            label_作者UID.Location = new Point(22, 112);
            label_作者UID.Name = "label_作者UID";
            label_作者UID.Size = new Size(78, 24);
            label_作者UID.TabIndex = 10;
            label_作者UID.Text = "作者UID";
            // 
            // Input_文章名
            // 
            Input_文章名.Location = new Point(110, 38);
            Input_文章名.Name = "Input_文章名";
            Input_文章名.Size = new Size(473, 30);
            Input_文章名.TabIndex = 18;
            // 
            // label_文章名
            // 
            label_文章名.AutoSize = true;
            label_文章名.Location = new Point(22, 41);
            label_文章名.Name = "label_文章名";
            label_文章名.Size = new Size(64, 24);
            label_文章名.TabIndex = 17;
            label_文章名.Text = "文章名";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1119, 604);
            Controls.Add(Group_Article);
            Controls.Add(Group_Email);
            Controls.Add(Label_当前连接数);
            Name = "MainForm";
            Text = "花濑社后台程序 - 测试 - Debug";
            Group_Email.ResumeLayout(false);
            Group_Email.PerformLayout();
            Group_Article.ResumeLayout(false);
            Group_Article.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Label_当前连接数;
        private System.Windows.Forms.Timer timer1;
        private GroupBox Group_Email;
        private Button button1;
        private TextBox Input_EmailAddress;
        private Label label1;
        private Button button_发送;
        private TextBox Input_发送内容;
        private GroupBox Group_Article;
        private TextBox Input_文章地址;
        private Label label_文章地址;
        private TextBox Input_作者名;
        private Label label_作者名;
        private TextBox Input_作者UID;
        private Label label_作者UID;
        private Button Button_新建文章;
        private TextBox Input_文章名;
        private Label label_文章名;
    }
}