using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;
using Webpage_As_A_Background.Utils;
using Webpage_As_A_Background.Views;

namespace Webpage_As_A_Background
{
    public partial class Main : Form
    {
        private const string ROOT_DIRECTORY = "source";
        private Screen[] _screens;
        private List<Process> _processes;

        public Main()
        {
            InitializeComponent();
            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            _screens = Screen.AllScreens;
            _processes = new List<Process>();

            string StartupPath = Application.StartupPath;
            string ApplicationName = $"{Process.GetCurrentProcess().ProcessName}.exe";
            string ExecutablePath = Path.Combine(StartupPath, ApplicationName);

            foreach (Screen screen in _screens)
            {
                Process process = Process.Start(ExecutablePath, $"screen:{screen.DeviceName} pid:{Process.GetCurrentProcess().Id}");
                _processes.Add(process);
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip.Show(Cursor.Position);
        }

        private void openSourceLocationItem_Click(object sender, EventArgs e)
        {
            OpenSourceFolder();
        }

        private void exitApplicationItem_Click(object sender, EventArgs e)
        {
            foreach (Process process in _processes)
            {
                process.Kill();
            }

            Application.ExitThread();
            Environment.Exit(0);
        }

        /// <summary>
        /// Opens the source folder in the file explorer.
        /// </summary>
        private void OpenSourceFolder()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string absoluteSourcePath = Path.Combine(Path.GetDirectoryName(assembly.Location), ROOT_DIRECTORY);
            Process.Start("explorer.exe", absoluteSourcePath);
        }
    }
}