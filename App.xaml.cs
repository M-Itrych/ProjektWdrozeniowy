using System;
using System.IO;
using System.Windows;

namespace ProjektWdrozeniowy
{
    public partial class App : Application
    {
        private void AppExit (object sender, EventArgs e)
        {
            if (Directory.Exists("./temp"))
            {
                Directory.Delete("./temp", true);
            }
        }
    }

}
