using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;
using ReazerJSON;

namespace UI
{
    public partial class Form1 : Form
    {
        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }
        public Form1()
        {
            InitializeComponent();
            
        }

        

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/settings.json"))
            {
                File.Delete(Application.StartupPath + "/settings.json");
            }
            JSONParser parser = new JSONParser();
            parser.createJson(Application.StartupPath + "/settings.json");
            parser.add("link", linkTextbox.Text);
            parser.add("path", pathTextbox.Text);

            if (convertCheckbox.Checked)
                parser.add("convert", 1);

            else
                parser.add("convert", 0);

            
            parser.save();
            string path = Application.StartupPath + "/converter.exe";
            Process.Start(path);
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                pathTextbox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!IsAdministrator())
            {
                if (MessageBox.Show("You need to run this Porgram as an Administrator! (Right click on File and choose \"Run as Administrator\"", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    Application.Exit();
            }
        }
    }
}
