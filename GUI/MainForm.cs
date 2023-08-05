using SDK;
using SDK.HOILAI_Community;
using System.Text;

namespace GUI
{
    public partial class MainForm : Form
    {
        public MainForm ()
        {
            InitializeComponent();
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

        private void Input_��������_TextChanged (object sender, EventArgs e)
        {

        }

        private void Button_�½�����_Click (object sender, EventArgs e)
        {

            try
            {
                API_Article.AddArticle_tb_OfficiaNews(Input_������.Text, Convert.ToInt32(Input_����UID.Text), DateTime.Now, Input_���µ�ַ.Text, Input_������.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}