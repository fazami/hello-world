using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FIMDB.UI;
using Farzin.Infrastructure.CrossCuttings.Helpers;

namespace FIMDB
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AutoMapperConfigurations.AutoMapperConfiguration.Configure();
            Application.Run(new MainForm());
        }
    }
}
