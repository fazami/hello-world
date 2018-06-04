namespace FIMDB.UI
{
    partial class FIMDBForm
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDirectors = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActors = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmGenre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRating = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmVotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmGross = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMetaScore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMPAA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLanguage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataUpdaterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movieFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(1, 69);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(757, 353);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmTitle,
            this.clmDirectors,
            this.clmActors,
            this.clmGenre,
            this.clmRating,
            this.clmVotes,
            this.clmGross,
            this.clmMetaScore,
            this.clmMPAA,
            this.clmLanguage,
            this.clmAvailable,
            this.clmYear});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            // 
            // clmTitle
            // 
            this.clmTitle.Caption = "Title";
            this.clmTitle.FieldName = "Title";
            this.clmTitle.Name = "clmTitle";
            this.clmTitle.Visible = true;
            this.clmTitle.VisibleIndex = 0;
            this.clmTitle.Width = 158;
            // 
            // clmDirectors
            // 
            this.clmDirectors.Caption = "Directors";
            this.clmDirectors.FieldName = "Directors";
            this.clmDirectors.Name = "clmDirectors";
            this.clmDirectors.Visible = true;
            this.clmDirectors.VisibleIndex = 1;
            this.clmDirectors.Width = 178;
            // 
            // clmActors
            // 
            this.clmActors.Caption = "Actors";
            this.clmActors.FieldName = "Stars";
            this.clmActors.Name = "clmActors";
            this.clmActors.Visible = true;
            this.clmActors.VisibleIndex = 2;
            this.clmActors.Width = 354;
            // 
            // clmGenre
            // 
            this.clmGenre.Caption = "Genre";
            this.clmGenre.FieldName = "Genre";
            this.clmGenre.Name = "clmGenre";
            this.clmGenre.OptionsFilter.PopupExcelFilterTextFilters = DevExpress.XtraGrid.Columns.ExcelFilterTextFilters.TextFilters;
            this.clmGenre.Visible = true;
            this.clmGenre.VisibleIndex = 3;
            this.clmGenre.Width = 142;
            // 
            // clmRating
            // 
            this.clmRating.Caption = "Rating";
            this.clmRating.FieldName = "Rating";
            this.clmRating.Name = "clmRating";
            this.clmRating.Visible = true;
            this.clmRating.VisibleIndex = 4;
            this.clmRating.Width = 52;
            // 
            // clmVotes
            // 
            this.clmVotes.Caption = "Votes";
            this.clmVotes.DisplayFormat.FormatString = "N0";
            this.clmVotes.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clmVotes.FieldName = "Votes";
            this.clmVotes.Name = "clmVotes";
            this.clmVotes.Visible = true;
            this.clmVotes.VisibleIndex = 5;
            this.clmVotes.Width = 68;
            // 
            // clmGross
            // 
            this.clmGross.Caption = "Gross";
            this.clmGross.DisplayFormat.FormatString = "N0";
            this.clmGross.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clmGross.FieldName = "Gross";
            this.clmGross.Name = "clmGross";
            this.clmGross.Visible = true;
            this.clmGross.VisibleIndex = 6;
            this.clmGross.Width = 79;
            // 
            // clmMetaScore
            // 
            this.clmMetaScore.Caption = "Meta Score";
            this.clmMetaScore.FieldName = "MetaScore";
            this.clmMetaScore.Name = "clmMetaScore";
            this.clmMetaScore.Visible = true;
            this.clmMetaScore.VisibleIndex = 7;
            this.clmMetaScore.Width = 54;
            // 
            // clmMPAA
            // 
            this.clmMPAA.Caption = "MPAA";
            this.clmMPAA.FieldName = "MPAA";
            this.clmMPAA.Name = "clmMPAA";
            this.clmMPAA.Visible = true;
            this.clmMPAA.VisibleIndex = 8;
            this.clmMPAA.Width = 59;
            // 
            // clmLanguage
            // 
            this.clmLanguage.Caption = "Language";
            this.clmLanguage.FieldName = "Language";
            this.clmLanguage.Name = "clmLanguage";
            this.clmLanguage.Visible = true;
            this.clmLanguage.VisibleIndex = 9;
            // 
            // clmAvailable
            // 
            this.clmAvailable.Caption = "Available";
            this.clmAvailable.FieldName = "Available";
            this.clmAvailable.Name = "clmAvailable";
            this.clmAvailable.Visible = true;
            this.clmAvailable.VisibleIndex = 10;
            this.clmAvailable.Width = 51;
            // 
            // clmYear
            // 
            this.clmYear.Caption = "Year";
            this.clmYear.FieldName = "Year";
            this.clmYear.Name = "clmYear";
            this.clmYear.Visible = true;
            this.clmYear.VisibleIndex = 11;
            this.clmYear.Width = 39;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(758, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataUpdaterToolStripMenuItem,
            this.movieFinderToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // dataUpdaterToolStripMenuItem
            // 
            this.dataUpdaterToolStripMenuItem.Name = "dataUpdaterToolStripMenuItem";
            this.dataUpdaterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataUpdaterToolStripMenuItem.Text = "DataUpdater";
            // 
            // movieFinderToolStripMenuItem
            // 
            this.movieFinderToolStripMenuItem.Name = "movieFinderToolStripMenuItem";
            this.movieFinderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.movieFinderToolStripMenuItem.Text = "MovieFinder";
            // 
            // FIMDBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 422);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FIMDBForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIMDBForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FIMDBForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn clmTitle;
        private DevExpress.XtraGrid.Columns.GridColumn clmDirectors;
        private DevExpress.XtraGrid.Columns.GridColumn clmActors;
        private DevExpress.XtraGrid.Columns.GridColumn clmGenre;
        private DevExpress.XtraGrid.Columns.GridColumn clmRating;
        private DevExpress.XtraGrid.Columns.GridColumn clmVotes;
        private DevExpress.XtraGrid.Columns.GridColumn clmGross;
        private DevExpress.XtraGrid.Columns.GridColumn clmMetaScore;
        private DevExpress.XtraGrid.Columns.GridColumn clmMPAA;
        private DevExpress.XtraGrid.Columns.GridColumn clmLanguage;
        private DevExpress.XtraGrid.Columns.GridColumn clmAvailable;
        private DevExpress.XtraGrid.Columns.GridColumn clmYear;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataUpdaterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movieFinderToolStripMenuItem;
    }
}