using System.Diagnostics;
using System.Windows.Forms;

namespace Webpage_As_A_Background
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            string[] arguments = Environment.GetCommandLineArgs();

            string screenArg = arguments.Where<string>(str => str.Contains("screen:") == true).FirstOrDefault();
            string pidArg = arguments.Where<string>(str => str.Contains("pid:") == true).FirstOrDefault();

            if (screenArg == null || pidArg == null)
            {
                Application.Run(new Main());
            }
            else
            {
                screenArg = screenArg.Replace("screen:", "");
                pidArg = pidArg.Replace("pid:", "");

                Screen screen = Screen.AllScreens.Where<Screen>(scr => scr.DeviceName == screenArg).FirstOrDefault();
                int pid = int.Parse(pidArg);

                if(screen != null)
                {
                    Application.Run(new PageHost(screen));
                }
            }
        }
    }
}