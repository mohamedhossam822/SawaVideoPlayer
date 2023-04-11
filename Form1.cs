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

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog()
            {
                ValidateNames = true,
                Filter = "WMV|*.wmv|WAV|*.wav|MP3|*.mp3|MP4|*.mp4|MKV|*.mkv|All|*.*"
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file = new FileInfo(dialog.FileName);
                    axWindowsMediaPlayer1.URL = file.FullName;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = textBox1.Text;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
    }
}
