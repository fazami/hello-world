namespace FIMDB.UI
{
    partial class MoviesForm
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
            this.dgvMovies = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmImdbRating = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmImdbTotalVotes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmGenres = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmActors = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmDirectors = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmWriters = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmLanguages = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmCountries = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmAwards = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmRated = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMetaScore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmFileSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtPath1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSync = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMovies
            // 
            this.dgvMovies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMovies.Location = new System.Drawing.Point(-1, 77);
            this.dgvMovies.MainView = this.gridView1;
            this.dgvMovies.Name = "dgvMovies";
            this.dgvMovies.Size = new System.Drawing.Size(755, 379);
            this.dgvMovies.TabIndex = 1;
            this.dgvMovies.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmTitle,
            this.clmYear,
            this.clmImdbRating,
            this.clmImdbTotalVotes,
            this.clmGenres,
            this.clmActors,
            this.clmDirectors,
            this.clmWriters,
            this.clmLanguages,
            this.clmCountries,
            this.clmAwards,
            this.clmRated,
            this.clmMetaScore,
            this.clmFileName,
            this.clmFileSize});
            this.gridView1.GridControl = this.dgvMovies;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            // 
            // clmTitle
            // 
            this.clmTitle.Caption = "Title";
            this.clmTitle.FieldName = "Title";
            this.clmTitle.Name = "clmTitle";
            this.clmTitle.Visible = true;
            this.clmTitle.VisibleIndex = 0;
            // 
            // clmYear
            // 
            this.clmYear.Caption = "Year";
            this.clmYear.FieldName = "Year";
            this.clmYear.Name = "clmYear";
            this.clmYear.Visible = true;
            this.clmYear.VisibleIndex = 1;
            // 
            // clmImdbRating
            // 
            this.clmImdbRating.Caption = "ImdbRating";
            this.clmImdbRating.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clmImdbRating.FieldName = "ImdbRating";
            this.clmImdbRating.Name = "clmImdbRating";
            this.clmImdbRating.Visible = true;
            this.clmImdbRating.VisibleIndex = 2;
            // 
            // clmImdbTotalVotes
            // 
            this.clmImdbTotalVotes.Caption = "ImdbTotalVotes";
            this.clmImdbTotalVotes.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clmImdbTotalVotes.FieldName = "ImdbTotalVotes";
            this.clmImdbTotalVotes.Name = "clmImdbTotalVotes";
            this.clmImdbTotalVotes.Visible = true;
            this.clmImdbTotalVotes.VisibleIndex = 3;
            // 
            // clmGenres
            // 
            this.clmGenres.Caption = "Genres";
            this.clmGenres.FieldName = "GenresFormatted";
            this.clmGenres.Name = "clmGenres";
            this.clmGenres.Visible = true;
            this.clmGenres.VisibleIndex = 4;
            // 
            // clmActors
            // 
            this.clmActors.Caption = "Actors";
            this.clmActors.FieldName = "ActorsFormatted";
            this.clmActors.Name = "clmActors";
            this.clmActors.Visible = true;
            this.clmActors.VisibleIndex = 5;
            // 
            // clmDirectors
            // 
            this.clmDirectors.Caption = "Directors";
            this.clmDirectors.FieldName = "DirectorsFormatted";
            this.clmDirectors.Name = "clmDirectors";
            this.clmDirectors.Visible = true;
            this.clmDirectors.VisibleIndex = 6;
            // 
            // clmWriters
            // 
            this.clmWriters.Caption = "Writers";
            this.clmWriters.FieldName = "WritersFormatted";
            this.clmWriters.Name = "clmWriters";
            this.clmWriters.Visible = true;
            this.clmWriters.VisibleIndex = 7;
            // 
            // clmLanguages
            // 
            this.clmLanguages.Caption = "Languages";
            this.clmLanguages.FieldName = "LanguagesFormatted";
            this.clmLanguages.Name = "clmLanguages";
            this.clmLanguages.Visible = true;
            this.clmLanguages.VisibleIndex = 8;
            // 
            // clmCountries
            // 
            this.clmCountries.Caption = "Countries";
            this.clmCountries.FieldName = "CountriesFormatted";
            this.clmCountries.Name = "clmCountries";
            this.clmCountries.Visible = true;
            this.clmCountries.VisibleIndex = 9;
            // 
            // clmAwards
            // 
            this.clmAwards.Caption = "Awards";
            this.clmAwards.FieldName = "AwardsFormatted";
            this.clmAwards.Name = "clmAwards";
            this.clmAwards.Visible = true;
            this.clmAwards.VisibleIndex = 10;
            // 
            // clmRated
            // 
            this.clmRated.Caption = "Rated";
            this.clmRated.FieldName = "Rated";
            this.clmRated.Name = "clmRated";
            this.clmRated.Visible = true;
            this.clmRated.VisibleIndex = 11;
            // 
            // clmMetaScore
            // 
            this.clmMetaScore.Caption = "MetaScore";
            this.clmMetaScore.FieldName = "MetaScore";
            this.clmMetaScore.Name = "clmMetaScore";
            this.clmMetaScore.Visible = true;
            this.clmMetaScore.VisibleIndex = 12;
            // 
            // clmFileName
            // 
            this.clmFileName.Caption = "FileName";
            this.clmFileName.FieldName = "FileName";
            this.clmFileName.Name = "clmFileName";
            this.clmFileName.Visible = true;
            this.clmFileName.VisibleIndex = 13;
            // 
            // clmFileSize
            // 
            this.clmFileSize.Caption = "FileSize";
            this.clmFileSize.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.clmFileSize.FieldName = "FileSize";
            this.clmFileSize.Name = "clmFileSize";
            this.clmFileSize.Visible = true;
            this.clmFileSize.VisibleIndex = 14;
            // 
            // txtPath1
            // 
            this.txtPath1.Location = new System.Drawing.Point(56, 12);
            this.txtPath1.Name = "txtPath1";
            this.txtPath1.Size = new System.Drawing.Size(485, 20);
            this.txtPath1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Path:";
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(547, 12);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(44, 20);
            this.btnSync.TabIndex = 4;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.sync_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(597, 12);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(59, 20);
            this.btnAddNew.TabIndex = 5;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.addNew_Click);
            // 
            // MoviesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 456);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath1);
            this.Controls.Add(this.dgvMovies);
            this.Name = "MoviesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Movies";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Movies_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dgvMovies;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn clmTitle;
        private DevExpress.XtraGrid.Columns.GridColumn clmYear;
        private DevExpress.XtraGrid.Columns.GridColumn clmImdbRating;
        private DevExpress.XtraGrid.Columns.GridColumn clmImdbTotalVotes;
        private DevExpress.XtraGrid.Columns.GridColumn clmGenres;
        private DevExpress.XtraGrid.Columns.GridColumn clmActors;
        private DevExpress.XtraGrid.Columns.GridColumn clmDirectors;
        private DevExpress.XtraGrid.Columns.GridColumn clmWriters;
        private DevExpress.XtraGrid.Columns.GridColumn clmLanguages;
        private DevExpress.XtraGrid.Columns.GridColumn clmCountries;
        private DevExpress.XtraGrid.Columns.GridColumn clmAwards;
        private DevExpress.XtraGrid.Columns.GridColumn clmRated;
        private DevExpress.XtraGrid.Columns.GridColumn clmMetaScore;
        private DevExpress.XtraGrid.Columns.GridColumn clmFileName;
        private DevExpress.XtraGrid.Columns.GridColumn clmFileSize;
        private System.Windows.Forms.TextBox txtPath1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnAddNew;
    }
}