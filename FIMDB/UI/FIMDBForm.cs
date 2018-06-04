using FIMDB.DAL;
using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FIMDB.UI
{
    public partial class FIMDBForm : Form
    {
        private List<Movie> _movies;

        public FIMDBForm()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            var db = new FimdbRepository();
            _movies = db.GetMovieList();
            gridControl1.DataSource = _movies;
            //this.bandedGridView1.FilterPopupExcelData += BandedGridView1_FilterPopupExcelData;
        }

        //private void BandedGridView1_FilterPopupExcelData(object sender, DevExpress.XtraGrid.Views.Grid.FilterPopupExcelDataEventArgs e)
        //{
        //    if (e.Column == clmGenre)
        //    {
        //        var genres = _movies.Where(a => string.IsNullOrEmpty(a.Genre) == false)
        //            .SelectMany(a => a.Genre.Split(','))
        //            .Select(a => a.Trim())
        //            .Distinct()
        //            .OrderBy(a => a)
        //            .ToList();
        //        foreach (var g in genres)
        //        {
        //            e.AddFilter(g, "Contains([" + e.Column.FieldName + "], '" + g + "')");
        //        }
        //    }
        //}

        private void FIMDBForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
