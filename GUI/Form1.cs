using SDK;
using System.Text;

namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1 ()
        {
            InitializeComponent();
        }

        private void Form1_Load (object sender, EventArgs e)
        {

        }



        private void timer1_Tick (object sender, EventArgs e)
        {
            Label_��ǰ������.Text = $"��ǰ������(TCP)��{ServerProgram.Program.clients.Count.ToString()}";
        }

        private void button_����_Click (object sender, EventArgs e)
        {
            try
            {
                string _body = Input_��������.Text;
                string item = "����";
                SDK.API.SendMail_Html("zhangzijian@menherachan.cn", "Cat.jiuzhong0910", Input_EmailAddress.Text, item, $"{ConstData.EmailTxt_First}{FormatText_email(_body)}{ConstData.EmailTxt_End}");
                //Thread t = new Thread(() => { SDK.API.SendMail_Html("zhangzijian@menherachan.cn", "Cat.jiuzhong0910", Input_EmailAddress.Text, item, $"{ConstData.EmailTxt_First}{FormatText_email(_body)}{ConstData.EmailTxt_End}"); });
                //t.Start();

            }
            catch
            {

            }
        }
        public string FormatText_email (string input)
        {
            string[] lines = input.Split(Environment.NewLine); // ʹ�����н��зָ�
            StringBuilder result = new StringBuilder();

            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line.Trim()))
                {
                    result.Append("<p>").Append(line.Trim()).Append("</p>"); // �����ͷ����ĩ��<p>��</p>
                }
                else
                {
                    result.Append("<br></br>"); // ���ڿ������<br></br>
                }
            }

            return result.ToString();
        }

    }
}