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
            Label_当前连接数.Text = $"当前连接数(TCP)：{ServerProgram.Program.clients.Count.ToString()}";
        }

        private void button_发送_Click (object sender, EventArgs e)
        {
            try
            {
                string _body = Input_发送内容.Text;
                string item = "花濑";
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
            string[] lines = input.Split(Environment.NewLine); // 使用新行进行分割
            StringBuilder result = new StringBuilder();

            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line.Trim()))
                {
                    result.Append("<p>").Append(line.Trim()).Append("</p>"); // 添加行头和行末的<p>和</p>
                }
                else
                {
                    result.Append("<br></br>"); // 对于空行添加<br></br>
                }
            }

            return result.ToString();
        }

        private void Input_发送内容_TextChanged (object sender, EventArgs e)
        {

        }

        private void Button_新建文章_Click (object sender, EventArgs e)
        {

            try
            {
                API_Article.AddArticle_tb_OfficiaNews(Input_作者名.Text, Convert.ToInt32(Input_作者UID.Text), DateTime.Now, Input_文章地址.Text, Input_文章名.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}