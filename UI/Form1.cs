using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReazerJSON;

namespace UI
{
    public partial class Form1 : Form
    {
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
    }
}
