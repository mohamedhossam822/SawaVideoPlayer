using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SawaVideoPlayer
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);
        public Form1(string link="",string error="")
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
                axWindowsMediaPlayer1.URL = link;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }

        }
    }
}
