using System;
using System.Windows.Forms;
using ZScream_Exporter.GUI;

namespace ZeldaFullEditor
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
            Application.Run(new ZscreamForm());
        }
    }
}
