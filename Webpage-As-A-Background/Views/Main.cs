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

        public Main()
        {
            InitializeComponent();

            _screens = Screen.AllScreens;

            foreach(Screen screen in Screen.AllScreens)
            {
                Debug.WriteLine("SCREEN: " + screen.GetDeviceNameSanitized());
            }

            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();

            // For debugging - Show window by pressing - Ctrl + Shift + i
            services.AddBlazorWebViewDeveloperTools();
            services.AddLogging();
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();
            blazorWebView.RootComponents.Add<MainView>("#app");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            foreach(Screen screen in _screens)
            {
                PageHost pageHost = new PageHost(screen, "D:\\Documents\\My Web Sites\\WebpageBackground");
                pageHost.Show();
            }
        }
    }
}