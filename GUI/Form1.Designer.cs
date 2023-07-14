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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(929, 552);
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
    }
}