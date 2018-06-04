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
    public partial class MovieSearchPathsForm : Form
    {
        public MovieSearchPathsForm()
        {
            InitializeComponent();
        }

        public string[] SearchPaths { get; private set; }

        private List<SearchPath> getSearchPaths()
        {
            List<SearchPath> result;
            using(var svc = new MovieService())
            {
                result = svc.GetSearchPaths();
            }
            return result;
        }

        private int findPathIndexOnListBox(string path)
        {
            for (int i = 0; i < lstPaths.Items.Count; i++)
            {
                if (((string)lstPaths.Items[i]).ToLower() == path.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        private void movieSearchPathsForm_Load(object sender, EventArgs e)
        {
            var paths = getSearchPaths();
            foreach (var item in paths)
            {
                lstPaths.Items.Add(item.Path, item.IsActive);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var folder = new FolderBrowserDialog())
            {
                var result = folder.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var path = folder.SelectedPath;
                    var index = findPathIndexOnListBox(path);
                    if (index == -1)
                    {
                        lstPaths.Items.Add(path);
                        index = lstPaths.Items.Count - 1;
                    }
                    lstPaths.SetItemChecked(index, true);
                    lstPaths.SelectedIndex = index;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstPaths.SelectedIndex == -1)
                return;
            lstPaths.Items.RemoveAt(lstPaths.SelectedIndex);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var paths = new List<SearchPath>();
            for (int i = 0; i < lstPaths.Items.Count; i++)
            {
                paths.Add(new SearchPath
                {
                    IsActive = lstPaths.GetItemCheckState(i) == CheckState.Checked,
                    Path = (string)lstPaths.Items[0]
                });

            }
            var svc = new MovieService();
            svc.UpdateSelectedPath(paths);

            SearchPaths = new string[lstPaths.CheckedItems.Count];
            for (int i = 0; i < lstPaths.CheckedItems.Count; i++)
            {
                SearchPaths[i] = (string)lstPaths.CheckedItems[i];
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
