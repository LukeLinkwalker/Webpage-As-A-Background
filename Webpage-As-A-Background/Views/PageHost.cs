using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Webpage_As_A_Background.Utils;
using Webpage_As_A_Background.Views;

namespace Webpage_As_A_Background
{
    public partial class PageHost : Form
    {
        private const string RESOURCE_DIR = "resources";
        private Screen _screen { get; set; }
        public string _deviceName { get; set; }

        public PageHost(Screen screen, string source)
        {
            InitializeComponent();

            _screen = screen;
            _deviceName = screen.GetDeviceNameSanitized();
            this.Text = $"PageHost_{_deviceName}";

            // Create resource directory
            if (Directory.Exists(RESOURCE_DIR) == false)
            {
                Directory.CreateDirectory(RESOURCE_DIR);
            }

            // Create specific directory for display
            if (Directory.Exists(Path.Combine(RESOURCE_DIR, _deviceName)) == false)
            {
                Directory.CreateDirectory(Path.Combine(RESOURCE_DIR, _deviceName));
            }

            // Start content server
            SimpleHTTPServer server = new SimpleHTTPServer(Path.Combine(RESOURCE_DIR, _deviceName));
            int port = server.Port;

            // Clear content from source folder
            DeleteContent(Path.Combine(RESOURCE_DIR, _deviceName));

            // Copy content from source folder
            CopyContent(source, Path.Combine(RESOURCE_DIR, _deviceName));

            // Monitor source folder
            // FileSystemWatcher -> Callback -> Copy Files -> Force update of UI

            // Show window
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();
            blazorWebView.RootComponents.Add<PageHostView>("#app", new Dictionary<string, object?> {
                { "Port", port }
            });
        }

        public void ChangeSourceFolder(string source)
        {

        }

        private void CopyContent(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        private void DeleteContent(string resourcePath)
        {
            string[] directoryPaths = Directory.GetDirectories(resourcePath);
            string[] filePaths = Directory.GetFiles(resourcePath);

            foreach (string directoryPath in directoryPaths)
            {
                Directory.Delete(directoryPath, true);
            }

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        private void WindowInitTimer_Tick(object? sender, EventArgs e)
        {
            IntPtr hWnd = this.Handle;
            Window.Move(hWnd);
            this.Location = _screen.WorkingArea.Location;
            this.Size = _screen.WorkingArea.Size;
            this.WindowState = FormWindowState.Maximized;
            windowInitTimer.Stop();
        }
    }
}
