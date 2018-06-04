using IMDB_Downloader.DAL;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMDB_Downloader
{
    public partial class DownloaderForm : Form
    {
        private int _progressBarMaxValue = 200;
        private int _maxConnection = 8;
        public DownloaderForm()
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
            var downloaderService = new IMDBDownloaderService();
            var repository = new ImdbRepository();
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
        private async Task<bool> downloadAndSaveImdbPage(IMDBDownloaderService downloaderService, ImdbRepository repository, int page)
        {
            var movies = await downloaderService.GetMoviesInPage(page);
            repository.SaveImdbMovies(movies);
            return true;
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
            var imdbService = new IMDBDownloaderService();
            var allMovies = imdbService.LoadPreviouslySavedMoviesFromTextFile(@"D:\imdbmovies.log");
            var imdbRepository = new ImdbRepository();
            imdbRepository.SaveImdbMovies(allMovies);
            imdbRepository.CloseConnection();
        }


    }
}
