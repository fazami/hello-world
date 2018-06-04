namespace FIMDB.UI
{
    partial class MainForm
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
            this.btnMovieList = new DevExpress.XtraEditors.SimpleButton();
            this.btnMovieResolver = new DevExpress.XtraEditors.SimpleButton();
            this.btnMovieInfoDownloader = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnMovieList
            // 
            this.btnMovieList.Location = new System.Drawing.Point(12, 12);
            this.btnMovieList.Name = "btnMovieList";
            this.btnMovieList.Size = new System.Drawing.Size(260, 23);
            this.btnMovieList.TabIndex = 0;
            this.btnMovieList.Text = "Movie List";
            this.btnMovieList.Click += new System.EventHandler(this.btnMovieList_Click);
            // 
            // btnMovieResolver
            // 
            this.btnMovieResolver.Location = new System.Drawing.Point(12, 70);
            this.btnMovieResolver.Name = "btnMovieResolver";
            this.btnMovieResolver.Size = new System.Drawing.Size(260, 23);
            this.btnMovieResolver.TabIndex = 2;
            this.btnMovieResolver.Text = "Movie Files";
            this.btnMovieResolver.Click += new System.EventHandler(this.btnMovieResolver_Click);
            // 
            // btnMovieInfoDownloader
            // 
            this.btnMovieInfoDownloader.Location = new System.Drawing.Point(12, 41);
            this.btnMovieInfoDownloader.Name = "btnMovieInfoDownloader";
            this.btnMovieInfoDownloader.Size = new System.Drawing.Size(260, 23);
            this.btnMovieInfoDownloader.TabIndex = 3;
            this.btnMovieInfoDownloader.Text = "Info Downloader";
            this.btnMovieInfoDownloader.Click += new System.EventHandler(this.btnMovieInfoDownloader_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(12, 114);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(260, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 149);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMovieInfoDownloader);
            this.Controls.Add(this.btnMovieResolver);
            this.Controls.Add(this.btnMovieList);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnMovieList;
        private DevExpress.XtraEditors.SimpleButton btnMovieResolver;
        private DevExpress.XtraEditors.SimpleButton btnMovieInfoDownloader;
        private DevExpress.XtraEditors.SimpleButton btnExit;
    }
}