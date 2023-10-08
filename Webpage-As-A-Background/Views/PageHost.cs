using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Webpage_As_A_Background.Utils;
using Webpage_As_A_Background.Views;

namespace Webpage_As_A_Background
{
    public partial class PageHost : Form
    {
        private const string SOURCES_DIR = "sources";
        private Screen _screen = null;
        private FileSystemWatcher _watcher = null;
        private string _sourcePath = string.Empty;

        public event EventHandler<EventArgs> OnRefreshFrame;
        public int Port { get; set; }
        public string _deviceName { get; set; }

        public PageHost(Screen screen)
        {
            InitializeComponent();

            _screen = screen;
            _deviceName = screen.GetDeviceNameSanitized();
            this.Text = $"PageHost_{_deviceName}";

            _sourcePath = Path.Combine(SOURCES_DIR, _deviceName);
            //SetSourceFolder(source);

            // Create resource directory
            if (Directory.Exists(SOURCES_DIR) == false)
            {
                Directory.CreateDirectory(SOURCES_DIR);
            }

            // Create specific directory for display
            if (Directory.Exists(_sourcePath) == false)
            {
                Directory.CreateDirectory(_sourcePath);
            }

            // Start content server - needed to serve static files to the PageHost
            SimpleHTTPServer server = new SimpleHTTPServer(_sourcePath);
            this.Port = server.Port;

            // Start FileSystemWatcher
            SetFileSystemWatcher();

            // Show window
            var services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
            blazorWebView.HostPage = "wwwroot\\index.html";
            blazorWebView.Services = services.BuildServiceProvider();
            blazorWebView.RootComponents.Add<PageHostView>("#app", new Dictionary<string, object?> {
                { "pageHost", this }
            });
        }

        /// <summary>
        /// Sets and enables a FileSystemWatcher such that an update in the source folder 
        /// results in the source files being copied to the resource folder and the PageHost is refreshed.
        /// </summary>
        public void SetFileSystemWatcher()
        {
            if (_watcher != null)
            {
                _watcher.EnableRaisingEvents = false;
                _watcher.Dispose();
                _watcher = null;
            }

            _watcher = new FileSystemWatcher(_sourcePath);

            _watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            _watcher.Changed += OnUpdate;
            _watcher.Created += OnUpdate;
            _watcher.Deleted += OnUpdate;
            _watcher.Renamed += OnUpdate;

            _watcher.IncludeSubdirectories = true;
            _watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Opens the source folder in the file explorer.
        /// </summary>
        public void OpenSourceFolder()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string absoluteSourcePath = Path.Combine(Path.GetDirectoryName(assembly.Location), SOURCES_DIR);
            Process.Start("explorer.exe", absoluteSourcePath);
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

        /// <summary>
        /// Triggers the resource updating workflow and emits an event to the relevant PageHost to force a refresh.
        /// </summary>
        private void OnUpdate(object sender, FileSystemEventArgs e)
        {
            OnRefreshFrame?.Invoke(this, null);
        }
    }
}