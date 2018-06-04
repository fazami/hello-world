using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FIMDB.Core;
using FIMDB.Model;

namespace FIMDB.UI
{
    public partial class MoviesForm : Form
    {
        public MoviesForm()
        {
            InitializeComponent();
        }

        private void Movies_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            List<Movie> movies;
            using (var service = new MovieService())
            {
                movies = service.GetAllMoviesInDatabase();
            }
            dgvMovies.DataSource = movies;
        }

        private List<Movie> getDataFromWeb(DirectoryInfo dir)
        {
            var oldData = (List<Movie>)dgvMovies.DataSource;
            List<Movie> movies = new List<Movie>();

            using (var service = new MovieServiceWeb())
            {
                foreach (var file in dir.GetFiles("*", SearchOption.AllDirectories))
                {
                    if (file.Length > 100000000)
                    {
                        var movie = service.FindMovieInWebByFileName(file.FullName);
                        var old = oldData.FirstOrDefault(a => a.FullFileName == movie.FullFileName);
                        if (old != null)
                        {
                            movie.Id = old.Id;
                        }
                        movies.Add(movie);
                    }
                }
            }
            return movies;
        }

        private void sync_Click(object sender, EventArgs e)
        {
            var dirName = txtPath1.Text.Trim();
            var dir = new DirectoryInfo(dirName);
            var movies = getDataFromWeb(dir);

            using (var service = new MovieService())
            {
                var added = movies.Where(a => a.Id == 0).ToList();
                var updated = movies.Except(added).ToList();

                service.AddMovies(added);
                service.UpdateMovies(updated);
            }

            loadData();
        }

        private void addNew_Click(object sender, EventArgs e)
        {
            var dirName = txtPath1.Text.Trim();
            var dir = new DirectoryInfo(dirName);
            var movies = getDataFromWeb(dir);

            using (var service = new MovieService())
            {
                var added = movies.Where(a => a.Id == 0).ToList();
                service.AddMovies(added);
            }

            loadData();
        }
    }
}
