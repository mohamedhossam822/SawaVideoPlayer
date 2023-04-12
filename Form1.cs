using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SawaVideoPlayer
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);
        public Form1(string link = "", string error = "")
        {
            const uint WDA_MONITOR = 1;
            SetWindowDisplayAffinity(this.Handle, WDA_MONITOR);
            InitializeComponent();
            if (error != "")
            {
                string message = "You are Unauthorized, Try again later or contact your system administrator!";
                string caption = "Unauthorized!";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                axWindowsMediaPlayer1.URL = Decocode(link);
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }

        }
        private string Endcode(string originalString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(originalString);
            string encodedString = Convert.ToBase64String(bytes);
            return encodedString;
        }
        private string Decocode(string encodedString)
        {
            byte[] bytes = Convert.FromBase64String(encodedString);
            string originalString = Encoding.UTF8.GetString(bytes);
            return originalString;
        }
    }


}
