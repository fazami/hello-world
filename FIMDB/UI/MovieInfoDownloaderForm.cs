using Farzin.Infrastructure.CrossCutting.Audit;
using FIMDB.Core;
using FIMDB.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIMDB.UI
{
    public partial class MovieInfoDownloaderForm : Form
    {
        private int _progressBarMaxValue = 200;
        private int _maxConnection = 4;
        public MovieInfoDownloaderForm()
        {
            InitializeComponent();
            progressBar1.Maximum = _progressBarMaxValue;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            //MovePreviousSavedMoviesFromTextFileToDatabase();
            btnStart.Enabled = false;
            panel1.Visible = true;
            setCount(0, 200);
            var result = await DownloadIMDBData();
            setProgressbarValue(_progressBarMaxValue);
            btnStart.Enabled = true;
        }
        private async Task<bool> DownloadIMDBData()
        {
            var downloaderService = new DownloaderService();
            var repository = new FimdbRepository();
            var page = 1;
            var pageMax = 200;
            while (page <= pageMax)
            {
                var tasks = new List<Task<bool>>();
                for (int i = 0; i < _maxConnection; i++)
                {
                    tasks.Add(downloadAndSaveImdbPage(downloaderService, repository, page + i));
                }
                await Task.WhenAll(tasks);
                page += _maxConnection;
                setCount((page - 1), pageMax);
                setProgressbarValue((page - 1) * _progressBarMaxValue / (double)pageMax);
            }
            repository.CloseConnection();
            return true;
        }
        private async Task<bool> downloadAndSaveImdbPage(DownloaderService downloaderService, FimdbRepository repository, int page)
        {
            try
            {
                var movies = await downloaderService.GetMoviesInPage_MultiUrl(page);
                repository.SaveMovieInfos(movies);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Cannot download and save movies at page " + page);
                Logger.Log(ex);
                return false;
            }
        }
        private void setProgressbarValue(double v)
        {
            if (v > _progressBarMaxValue)
                v = _progressBarMaxValue;

            if (this.InvokeRequired)
                this.Invoke(new Action(() => progressBar1.Value = (int)v));
            else
                progressBar1.Value = (int)v;
        }
        private void setCount(int successfulCount, int totalCount)
        {
            var str = successfulCount.ToString() + " / " + totalCount.ToString();
            if (this.InvokeRequired)
                this.Invoke(new Action(() => lblCount.Text = str));
            else
                lblCount.Text = str;
        }
        private void ConvertPreviousSavedMoviesFromTextFileToDatabase()
        {
            var imdbService = new DownloaderService();
            var allMovies = imdbService.LoadPreviouslySavedMoviesFromTextFile(@"D:\imdbmovies.log");
            var imdbRepository = new FimdbRepository();
            imdbRepository.SaveMovieInfos(allMovies);
            imdbRepository.CloseConnection();
        }


    }
}
