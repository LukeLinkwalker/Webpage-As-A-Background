using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Webpage_As_A_Background.Utils;
using Webpage_As_A_Background.Views;

namespace Webpage_As_A_Background
{
    public partial class Main : Form
    {
        private Screen[] _screens;
        private List<PageHost> _pageHosts;

        public Main()
        {
            InitializeComponent();
            this.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            _screens = Screen.AllScreens;
            _pageHosts = new List<PageHost>();

            foreach (Screen screen in _screens)
            {
                PageHost pageHost = new PageHost(screen);
                pageHost.Show();
                _pageHosts.Add(pageHost);
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip.Show(Cursor.Position);
        }

        private void openSourceLocationItem_Click(object sender, EventArgs e)
        {
            _pageHosts[0].OpenSourceFolder();
        }

        private void forceReloadItem_Click(object sender, EventArgs e)
        {
            foreach (PageHost pageHost in _pageHosts)
            {
                pageHost.Close();
            }

            _pageHosts.Clear();

            foreach (Screen screen in _screens)
            {
                PageHost pageHost = new PageHost(screen);
                pageHost.Show();
                _pageHosts.Add(pageHost);
            }
        }

        private void exitApplicationItem_Click(object sender, EventArgs e)
        {
            foreach (PageHost pageHost in _pageHosts)
            {
                pageHost.Close();
            }

            Application.ExitThread();
            Environment.Exit(0);
        }
    }
}