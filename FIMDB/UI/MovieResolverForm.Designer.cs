namespace FIMDB.UI
{
    partial class MovieResolverForm
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
            this.clmDirName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMovieName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmImdbId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmPropability = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMissing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDuplicated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnResolveAll = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDuplicateFiles = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(0, 41);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(639, 220);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmDirName,
            this.clmFileName,
            this.clmMovieName,
            this.clmYear,
            this.clmImdbId,
            this.clmSize,
            this.clmPropability,
            this.clmMissing,
            this.clmDuplicated});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // clmDirName
            // 
            this.clmDirName.Caption = "Directory";
            this.clmDirName.FieldName = "DirName";
            this.clmDirName.Name = "clmDirName";
            this.clmDirName.Visible = true;
            this.clmDirName.VisibleIndex = 0;
            this.clmDirName.Width = 307;
            // 
            // clmFileName
            // 
            this.clmFileName.Caption = "File Name";
            this.clmFileName.FieldName = "FileName";
            this.clmFileName.Name = "clmFileName";
            this.clmFileName.Visible = true;
            this.clmFileName.VisibleIndex = 1;
            this.clmFileName.Width = 254;
            // 
            // clmMovieName
            // 
            this.clmMovieName.Caption = "Movie Name";
            this.clmMovieName.FieldName = "MovieName";
            this.clmMovieName.Name = "clmMovieName";
            this.clmMovieName.Visible = true;
            this.clmMovieName.VisibleIndex = 2;
            this.clmMovieName.Width = 147;
            // 
            // clmYear
            // 
            this.clmYear.Caption = "Year";
            this.clmYear.FieldName = "Year";
            this.clmYear.Name = "clmYear";
            this.clmYear.Visible = true;
            this.clmYear.VisibleIndex = 4;
            // 
            // clmImdbId
            // 
            this.clmImdbId.Caption = "ImdbId";
            this.clmImdbId.FieldName = "ImdbId";
            this.clmImdbId.Name = "clmImdbId";
            this.clmImdbId.Visible = true;
            this.clmImdbId.VisibleIndex = 5;
            // 
            // clmSize
            // 
            this.clmSize.Caption = "Size";
            this.clmSize.FieldName = "Size";
            this.clmSize.Name = "clmSize";
            this.clmSize.Visible = true;
            this.clmSize.VisibleIndex = 3;
            // 
            // clmPropability
            // 
            this.clmPropability.Caption = "Probability";
            this.clmPropability.FieldName = "Probability";
            this.clmPropability.Name = "clmPropability";
            this.clmPropability.Visible = true;
            this.clmPropability.VisibleIndex = 6;
            // 
            // clmMissing
            // 
            this.clmMissing.Caption = "Missing";
            this.clmMissing.FieldName = "IsMissing";
            this.clmMissing.Name = "clmMissing";
            this.clmMissing.Visible = true;
            this.clmMissing.VisibleIndex = 7;
            // 
            // clmDuplicated
            // 
            this.clmDuplicated.Caption = "Duplicated";
            this.clmDuplicated.FieldName = "IsDuplicate";
            this.clmDuplicated.Name = "clmDuplicated";
            this.clmDuplicated.Visible = true;
            this.clmDuplicated.VisibleIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(131, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Configure Search Paths";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.search_Click);
            // 
            // btnResolveAll
            // 
            this.btnResolveAll.Location = new System.Drawing.Point(149, 12);
            this.btnResolveAll.Name = "btnResolveAll";
            this.btnResolveAll.Size = new System.Drawing.Size(78, 23);
            this.btnResolveAll.TabIndex = 2;
            this.btnResolveAll.Text = "Resolve All";
            this.btnResolveAll.UseVisualStyleBackColor = true;
            this.btnResolveAll.Click += new System.EventHandler(this.btnResolveAll_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(233, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDuplicateFiles
            // 
            this.btnDuplicateFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDuplicateFiles.Location = new System.Drawing.Point(529, 12);
            this.btnDuplicateFiles.Name = "btnDuplicateFiles";
            this.btnDuplicateFiles.Size = new System.Drawing.Size(98, 23);
            this.btnDuplicateFiles.TabIndex = 4;
            this.btnDuplicateFiles.Text = "Duplicate Files";
            this.btnDuplicateFiles.UseVisualStyleBackColor = true;
            this.btnDuplicateFiles.Click += new System.EventHandler(this.btnDuplicateFiles_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(451, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // MovieResolverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 261);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDuplicateFiles);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnResolveAll);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.gridControl1);
            this.Name = "MovieResolverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MovieFinderForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MovieFinderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn clmDirName;
        private DevExpress.XtraGrid.Columns.GridColumn clmFileName;
        private DevExpress.XtraGrid.Columns.GridColumn clmMovieName;
        private DevExpress.XtraGrid.Columns.GridColumn clmYear;
        private DevExpress.XtraGrid.Columns.GridColumn clmImdbId;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnResolveAll;
        private DevExpress.XtraGrid.Columns.GridColumn clmSize;
        private DevExpress.XtraGrid.Columns.GridColumn clmPropability;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDuplicateFiles;
        private DevExpress.XtraGrid.Columns.GridColumn clmMissing;
        private DevExpress.XtraGrid.Columns.GridColumn clmDuplicated;
        private System.Windows.Forms.Button btnRefresh;
    }
}