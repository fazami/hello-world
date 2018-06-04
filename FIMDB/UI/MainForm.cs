using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIMDB.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnMovieList_Click(object sender, EventArgs e)
        {
            using(var form = new FIMDBForm())
            {
                form.ShowDialog();
            }
        }

        private void btnMovieInfoDownloader_Click(object sender, EventArgs e)
        {
            using (var form = new MovieInfoDownloaderForm())
            {
                form.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMovieResolver_Click(object sender, EventArgs e)
        {
            using(var form = new MovieResolverForm())
            {
                form.ShowDialog();
            }
        }
    }
}
