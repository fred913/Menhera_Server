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
            Label_当前连接数.Text = $"当前连接数(TCP)：{ServerProgram.Program.clients.Count.ToString()}";
        }
    }
}