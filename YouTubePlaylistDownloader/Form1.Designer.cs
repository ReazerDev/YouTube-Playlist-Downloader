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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.startBtn = new System.Windows.Forms.Button();
            this.linkTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pathTextbox = new System.Windows.Forms.TextBox();
            this.convertCheckbox = new System.Windows.Forms.CheckBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.downloadPathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.albumPicture = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.browseImageBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.artistTextbox = new System.Windows.Forms.TextBox();
            this.albumTextbox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.yearTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.imageDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.albumPicture)).BeginInit();
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
            // albumPicture
            // 
            this.albumPicture.Location = new System.Drawing.Point(634, 65);
            this.albumPicture.Name = "albumPicture";
            this.albumPicture.Size = new System.Drawing.Size(127, 127);
            this.albumPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.albumPicture.TabIndex = 9;
            this.albumPicture.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Playlist Settings:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(631, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Video Settings:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(635, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Thumbnail:";
            // 
            // browseImageBtn
            // 
            this.browseImageBtn.Location = new System.Drawing.Point(634, 199);
            this.browseImageBtn.Name = "browseImageBtn";
            this.browseImageBtn.Size = new System.Drawing.Size(127, 38);
            this.browseImageBtn.TabIndex = 13;
            this.browseImageBtn.Text = "Browse Image";
            this.browseImageBtn.UseVisualStyleBackColor = true;
            this.browseImageBtn.Click += new System.EventHandler(this.browseImageBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(767, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Artist:";
            // 
            // artistTextbox
            // 
            this.artistTextbox.Location = new System.Drawing.Point(767, 65);
            this.artistTextbox.Name = "artistTextbox";
            this.artistTextbox.Size = new System.Drawing.Size(300, 22);
            this.artistTextbox.TabIndex = 15;
            // 
            // albumTextbox
            // 
            this.albumTextbox.Location = new System.Drawing.Point(767, 110);
            this.albumTextbox.Name = "albumTextbox";
            this.albumTextbox.Size = new System.Drawing.Size(300, 22);
            this.albumTextbox.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(767, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Album:";
            // 
            // yearTextbox
            // 
            this.yearTextbox.Location = new System.Drawing.Point(767, 154);
            this.yearTextbox.Name = "yearTextbox";
            this.yearTextbox.Size = new System.Drawing.Size(300, 22);
            this.yearTextbox.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(767, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Year:";
            // 
            // imageDialog
            // 
            this.imageDialog.Filter = "Images | *.png; *.jpg; *.jpeg;";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 248);
            this.Controls.Add(this.yearTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.albumTextbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.artistTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.browseImageBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.albumPicture);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.convertCheckbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pathTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkTextbox);
            this.Controls.Add(this.startBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "YouTube Playlist Downloader";
            ((System.ComponentModel.ISupportInitialize)(this.albumPicture)).EndInit();
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
        private System.Windows.Forms.FolderBrowserDialog downloadPathDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.PictureBox albumPicture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button browseImageBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox artistTextbox;
        private System.Windows.Forms.TextBox albumTextbox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox yearTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog imageDialog;
    }
}

