namespace GUI
{
    partial class Form1
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
            Input_发送内容 = new TextBox();
            button_发送 = new Button();
            label1 = new Label();
            Input_EmailAddress = new TextBox();
            button1 = new Button();
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
            // Input_发送内容
            // 
            Input_发送内容.AcceptsReturn = true;
            Input_发送内容.AcceptsTab = true;
            Input_发送内容.Location = new Point(12, 103);
            Input_发送内容.Multiline = true;
            Input_发送内容.Name = "Input_发送内容";
            Input_发送内容.Size = new Size(427, 377);
            Input_发送内容.TabIndex = 1;
            // 
            // button_发送
            // 
            button_发送.Location = new Point(12, 486);
            button_发送.Name = "button_发送";
            button_发送.Size = new Size(206, 54);
            button_发送.TabIndex = 2;
            button_发送.Text = "发送";
            button_发送.UseVisualStyleBackColor = true;
            button_发送.Click += button_发送_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 66);
            label1.Name = "label1";
            label1.Size = new Size(82, 24);
            label1.TabIndex = 3;
            label1.Text = "邮箱地址\r\n";
            // 
            // Input_EmailAddress
            // 
            Input_EmailAddress.Location = new Point(100, 66);
            Input_EmailAddress.Name = "Input_EmailAddress";
            Input_EmailAddress.Size = new Size(339, 30);
            Input_EmailAddress.TabIndex = 4;
            // 
            // button1
            // 
            button1.Location = new Point(224, 486);
            button1.Name = "button1";
            button1.Size = new Size(215, 57);
            button1.TabIndex = 5;
            button1.Text = "向所有的用户发送";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 552);
            Controls.Add(button1);
            Controls.Add(Input_EmailAddress);
            Controls.Add(label1);
            Controls.Add(button_发送);
            Controls.Add(Input_发送内容);
            Controls.Add(Label_当前连接数);
            Name = "Form1";
            Text = "Debug";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Label_当前连接数;
        private System.Windows.Forms.Timer timer1;
        private TextBox Input_发送内容;
        private Button button_发送;
        private Label label1;
        private TextBox Input_EmailAddress;
        private Button button1;
    }
}