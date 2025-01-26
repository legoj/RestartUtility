using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace RestartUtility
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static Mutex mutex = new Mutex(true, "{5d00cc30-4c78-4dfc-9ca3-5d71787550f9}");        
        [STAThread]
        static void Main()
        {            
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                RestartDialog m = new RestartDialog();
                m.ShowDialog();
            }
        }
    }
}
