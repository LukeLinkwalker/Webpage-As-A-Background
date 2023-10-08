using Microsoft.AspNetCore.Components;
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
        private Screen _screen = null;
        private FileSystemWatcher _watcher = null;
        private string _sourcePath = string.Empty;
        private string _resourcePath = string.Empty;

        public event EventHandler<EventArgs> OnRefreshFrame;
        public int Port { get; set; }
        public string _deviceName { get; set; }

        public PageHost(Screen screen, string source)
        {
            InitializeComponent();

            _screen = screen;
            _deviceName = screen.GetDeviceNameSanitized();
            this.Text = $"PageHost_{_deviceName}";

            _resourcePath = Path.Combine(RESOURCE_DIR, _deviceName);
            SetSourceFolder(source);

            // Create resource directory
            if (Directory.Exists(RESOURCE_DIR) == false)
            {
                Directory.CreateDirectory(RESOURCE_DIR);
            }

            // Create specific directory for display
            if (Directory.Exists(_resourcePath) == false)
            {
                Directory.CreateDirectory(_resourcePath);
            }

            // Start content server - needed to serve static files to the PageHost
            SimpleHTTPServer server = new SimpleHTTPServer(_resourcePath);
            this.Port = server.Port;

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
        /// Sets / updates the source path and updates FileSystemWatcher to reflect the change.
        /// </summary>
        /// <param name="source"></param>
        public void SetSourceFolder(string source)
        {
            _sourcePath = source;

            // Clear and copy content from source path to resource path
            UpdateResources();

            // Monitor source folder for changes
            SetFileSystemWatcher();
        }

        /// <summary>
        /// Clears resource folder and copies content from source folder to the resource folder.
        /// </summary>
        private void UpdateResources()
        {
            // Clear content from resource folder
            DeleteContent(_resourcePath);

            // Copy content from source folder
            CopyContent(_sourcePath, _resourcePath);

            // Refresh frame
        }

        private void CopyContent(string sourcePath, string resourcePath)
        {
            foreach (string directoryPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(directoryPath.Replace(sourcePath, resourcePath));
            }

            foreach (string path in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(path, path.Replace(sourcePath, resourcePath), true);
            }
        }

        private void DeleteContent(string resourcePath)
        {
            foreach (string directoryPath in Directory.GetDirectories(resourcePath))
            {
                Directory.Delete(directoryPath, true);
            }

            foreach (string filePath in Directory.GetFiles(resourcePath))
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

        /// <summary>
        /// Triggers the resource updating workflow and emits an event to the relevant PageHost to force a refresh.
        /// </summary>
        private void OnUpdate(object sender, FileSystemEventArgs e)
        {
            UpdateResources();
            OnRefreshFrame?.Invoke(this, null);
        }
    }
}