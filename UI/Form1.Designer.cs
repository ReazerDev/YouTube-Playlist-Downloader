namespace UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startBtn = new System.Windows.Forms.Button();
            this.linkTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pathTextbox = new System.Windows.Forms.TextBox();
            this.convertCheckbox = new System.Windows.Forms.CheckBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(24, 126);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(580, 33);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // linkTextbox
            // 
            this.linkTextbox.Location = new System.Drawing.Point(134, 33);
            this.linkTextbox.Name = "linkTextbox";
            this.linkTextbox.Size = new System.Drawing.Size(470, 22);
            this.linkTextbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Playlist Link:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Download Path:";
            // 
            // pathTextbox
            // 
            this.pathTextbox.Location = new System.Drawing.Point(134, 65);
            this.pathTextbox.Name = "pathTextbox";
            this.pathTextbox.Size = new System.Drawing.Size(377, 22);
            this.pathTextbox.TabIndex = 3;
            // 
            // convertCheckbox
            // 
            this.convertCheckbox.AutoSize = true;
            this.convertCheckbox.Location = new System.Drawing.Point(24, 99);
            this.convertCheckbox.Name = "convertCheckbox";
            this.convertCheckbox.Size = new System.Drawing.Size(160, 21);
            this.convertCheckbox.TabIndex = 5;
            this.convertCheckbox.Text = "Convert to .mp 3 File";
            this.convertCheckbox.UseVisualStyleBackColor = true;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(517, 64);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(87, 28);
            this.browseBtn.TabIndex = 6;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(24, 194);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(580, 42);
            this.progressBar.TabIndex = 7;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(21, 174);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(66, 17);
            this.progressLabel.TabIndex = 8;
            this.progressLabel.Text = "0/0 Done";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 248);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.convertCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pathTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkTextbox);
            this.Controls.Add(this.startBtn);
            this.Name = "Form1";
            this.Text = "YouTube Playlist Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox linkTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pathTextbox;
        private System.Windows.Forms.CheckBox convertCheckbox;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressLabel;
    }
}

