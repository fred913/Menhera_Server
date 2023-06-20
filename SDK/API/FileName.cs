using System.Runtime.InteropServices;
using System.Text;
namespace SDK
{
    public class INIFile
    {
        private string Path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString (string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString (string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public INIFile (string path)
        {
            this.Path = path;
        }

        public void Write (string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, this.Path);
        }

        public string Read (string section, string key, string defaultValue = "")
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, sb, 255, this.Path);
            return sb.ToString();
        }
    }

}

