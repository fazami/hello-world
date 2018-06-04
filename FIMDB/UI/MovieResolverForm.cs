using FIMDB.Core;
using FIMDB.DAL;
using FIMDB.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIMDB.UI
{
    public partial class MovieResolverForm : Form
    {
        string[] _searchPaths;
        List<MovieFile> _movieFiles = new List<MovieFile>();
        List<MovieBrief> _allMovies = new List<MovieBrief>();
        Dictionary<MovieBrief, string> _searcherDictionary;
        public MovieResolverForm()
        {
            InitializeComponent();

        }

        #region events
        private void MovieFinderForm_Load(object sender, EventArgs e)
        {
            loadData();
            refreshDataGrid();
            //gridControl1.MainView.ActiveEditor.ContextMenu = new ContextMenu(new[] { new MenuItem {  Text = "Delete"} });
        }

        private void search_Click(object sender, EventArgs e)
        {
            using (var form = new MovieSearchPathsForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                    _searchPaths = form.SearchPaths;
            }

            if (_searchPaths == null || _searchPaths.Length == 0)
                return;

            List<FoundMovieFile> movies;
            using (var svc = new MovieService())
            {
                movies = svc.SearchMoviesInHDD(_searchPaths);
            }
            foreach (var m in movies)
            {
                if (_movieFiles.Any(a => a.DirName.ToLower() == m.DirectoryName.ToLower() && a.FileName.ToLower() == m.FileName.ToLower()) == false)
                {
                    _movieFiles.Add(new MovieFile
                    {
                        DirName = m.DirectoryName,
                        FileName = m.FileName,
                        Size = m.Size
                    });
                }
            }
            this.gridControl1.RefreshDataSource();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var db = new FimdbRepository();
            db.SaveMovieFiles(_movieFiles);
            db.CloseConnection();
            MessageBox.Show("Done!");
        }

        private void btnDuplicateFiles_Click(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = _movieFiles
                .Where(a => a.IsDuplicate)
                .OrderBy(a => a.IsDuplicate)
                .ThenBy(a => a.ImdbId)
                .ToList();
        }

        private void btnResolveAll_Click(object sender, EventArgs e)
        {
            resolveAllMovies();
            this.gridControl1.RefreshDataSource();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshDataGrid();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);
                MessageBox.Show(row[0].ToString());
            }
        }
        #endregion

        #region private methods
        private void loadData()
        {
            var db = new FimdbRepository();
            _movieFiles = db.GetMovieFileList();
            _movieFiles.ForEach(a => a.FileName = a.FileName.ToLower());
            _allMovies = db.GetMovieBriefList();
            _allMovies.ForEach(a => a.Title = a.Title.ToLower());
            _searcherDictionary = new Dictionary<MovieBrief, string>();
        }

        private void resolveAllMovies()
        {
            string title;
            int year;
            foreach (var m in _movieFiles.Where(a => a.Probability == 0))
            {
                //getCluesByFileName(m.FileName, out title, out year);

                //if (string.IsNullOrEmpty(title))
                //    continue;

                //var resolveResult = tryResolveLevel1(title, year);
                //if (resolveResult.Success)
                //{
                //    setToResult(resolveResult, m);
                //    continue;
                //}
                //resolveResult = tryResolveLevel2(title, year);
                //if (resolveResult.Success)
                //{
                //    setToResult(resolveResult, m);
                //    continue;
                //}
                //resolveResult = tryResolveLevel3(title, year);
                //if (resolveResult.Success)
                //{
                //    setToResult(resolveResult, m);
                //    continue;
                //}
            }
        }

        //private void getCluesByFileName(string movieFileName, out string title, out int year)
        //{
        //    var fileName = Path.GetFileNameWithoutExtension(movieFileName);
        //    var fileNameSplitted = fileName.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);

        //    year = 0;
        //    title = "";
        //    var thisYear = DateTime.Today.Year;
        //    var yrIndex = 0;

        //    for (int i = 0; i < fileNameSplitted.Length; i++)
        //    {
        //        if (int.TryParse(fileNameSplitted[i], out year) &&
        //            year > 1800 && year <= thisYear)
        //        {
        //            yrIndex = i;
        //            break;
        //        }

        //        if (fileNameSplitted[i].StartsWith("720p") ||
        //            fileNameSplitted[i].StartsWith("480p") ||
        //            fileNameSplitted[i].StartsWith("1080p") ||
        //            fileNameSplitted[i] == "bdrip" ||
        //            fileNameSplitted[i] == "dvdrip" ||
        //            fileNameSplitted[i] == "xvid" ||
        //            fileNameSplitted[i] == "subs")
        //            break;

        //        title += fileNameSplitted[i] + " ";
        //    }

        //    title = title.Trim();

        //}

        //private ResolveResult tryResolveLevel3(string title, int year)
        //{
        //    var result = new ResolveResult();
        //    var splitedSearchTitle = title.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);
        //    List<KeyValuePair<MovieBrief, int>> movies1 = new List<KeyValuePair<MovieBrief, int>>();
        //    var charCount = splitedSearchTitle.Select(a => a.Length).Sum();
        //    foreach (var movie in _allMovies)
        //    {
        //        var splitedTitle = movie.Title.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries);
        //        var simmilarWords = splitedSearchTitle.Where(st => splitedTitle.Contains(st)).ToList();
        //        var cnt = simmilarWords.Select(a => a.Length).Sum();
        //        if (cnt * 100 / charCount > 75)
        //            movies1.Add(new KeyValuePair<MovieBrief, int>(movie, cnt));
        //    }
        //    movies1 = movies1.Where(a => a.Value == movies1.Max(m1 => m1.Value)).ToList();
        //    if (year != 0)
        //        movies1 = movies1.Where(a => a.Key.Year == year).ToList();

        //    if (movies1.Count == 1)
        //    {
        //        result.Movies = new List<MovieBrief> { movies1[0].Key };
        //        result.Probability = 80;
        //    }
        //    else if (movies1.Count > 0)
        //    {
        //        result.Movies = new List<MovieBrief>();
        //        result.Movies.AddRange(movies1.Select(a => a.Key));
        //        result.Probability = 80 / movies1.Count();
        //    }
        //    return result;
        //}

        //private ResolveResult tryResolveLevel2(string title, int year)
        //{
        //    var result = new ResolveResult();
        //    if (title.StartsWith("the "))
        //        title = title.Substring(4);
        //    var searcher = string.Join("", title.Split(_invalidChars, StringSplitOptions.RemoveEmptyEntries));
        //    var movies1 = _searcherDictionary.Where(a => a.Value == searcher).ToList();
        //    if (year != 0)
        //        movies1 = movies1.Where(a => a.Key.Year == year).ToList();

        //    if (movies1.Count == 1)
        //    {
        //        result.Movies = new List<MovieBrief> { movies1[0].Key };
        //        result.Probability = 90;
        //    }
        //    else if (movies1.Count > 0)
        //    {
        //        result.Movies = new List<MovieBrief>();
        //        result.Movies.AddRange(movies1.Select(a => a.Key));
        //        result.Probability = 90 / movies1.Count();
        //    }
        //    return result;
        //}

        //private ResolveResult tryResolveLevel1(string title, int year)
        //{
        //    var result = new ResolveResult();
        //    var moviesq = _allMovies.Where(a => string.Compare(a.Title, title) == 0);
        //    var movies1 = moviesq.ToList();
        //    if (year != 0)
        //        moviesq = moviesq.Where(a => a.Year == year);
        //    movies1 = moviesq.ToList();

        //    if (movies1.Count == 1)
        //    {
        //        result.Movies = new List<MovieBrief> { movies1[0] };
        //        result.Probability = 100;
        //    }
        //    else if (movies1.Count > 0)
        //    {
        //        result.Movies = new List<MovieBrief>();
        //        result.Movies.AddRange(movies1);
        //        result.Probability = 100 / movies1.Count();
        //    }

        //    return result;
        //}

        private void setToResult(ResolveResult resolveResult, MovieFile m)
        {
            if (resolveResult.Movies.Count == 1)
            {
                m.MovieId = resolveResult.Movies[0].Id;
                m.ImdbId = resolveResult.Movies[0].ImdbId;
                m.Title = resolveResult.Movies[0].Title;
                m.ProbableImdbIds = resolveResult.Movies[0].ImdbId;
                m.Probability = resolveResult.Probability;
            }
            else if (resolveResult.Movies.Any())
            {
                var imdbIds = resolveResult.Movies.Select(a => a.ImdbId).ToList();
                m.ProbableImdbIds = string.Join(",", imdbIds);
                m.Probability = resolveResult.Probability;
            }

        }

        private void refreshDataGrid()
        {
            this.gridControl1.DataSource = _movieFiles;
        }

        #endregion

    }
}
