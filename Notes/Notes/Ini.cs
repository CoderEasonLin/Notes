using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    class IniFile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int lpDefault, string lpFileName);

        private string File = null;
        public IniFile(string file)
        {
            File = file;
        }

        public string Read(string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, string.Empty, sb, 255, File);
            return sb.ToString();
        }

        public int ReadInt(string section, string key)
        {
            int i = GetPrivateProfileInt(section, key, -1, File);
            return i;
        }

        public bool ReadBool(string section, string key)
        {
            string s = Read(section, key).ToLower();
            return (s == "true");
        }

        public void Write(string section, string key, string value)
        {
            long l = WritePrivateProfileString(section, key, value, File);
        }

        public void WriteInt(string section, string key, int value)
        {
            Write(section, key, value.ToString());
        }
    }
}
