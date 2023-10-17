namespace Webpage_As_A_Background
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            blazorWebView = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
            notifyIcon = new NotifyIcon(components);
            contextMenuStrip = new ContextMenuStrip(components);
            openSourceLocationItem = new ToolStripMenuItem();
            exitApplicationItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // blazorWebView
            // 
            blazorWebView.Dock = DockStyle.Fill;
            blazorWebView.Location = new Point(0, 0);
            blazorWebView.Name = "blazorWebView";
            blazorWebView.Size = new Size(368, 400);
            blazorWebView.TabIndex = 0;
            blazorWebView.Text = "blazorWebView";
            // 
            // notifyIcon
            // 
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Webpage-As-A-Background";
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += notifyIcon_MouseClick;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { openSourceLocationItem, exitApplicationItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(192, 48);
            // 
            // openSourceLocationItem
            // 
            openSourceLocationItem.Name = "openSourceLocationItem";
            openSourceLocationItem.Size = new Size(191, 22);
            openSourceLocationItem.Text = "Open Source Location";
            openSourceLocationItem.Click += openSourceLocationItem_Click;
            // 
            // exitApplicationItem
            // 
            exitApplicationItem.Name = "exitApplicationItem";
            exitApplicationItem.Size = new Size(191, 22);
            exitApplicationItem.Text = "Exit Application";
            exitApplicationItem.Click += exitApplicationItem_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(368, 400);
            Controls.Add(blazorWebView);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Webpage-As-A-Background";
            WindowState = FormWindowState.Minimized;
            Load += Main_Load;
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorWebView;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem openSourceLocationItem;
        private ToolStripMenuItem exitApplicationItem;
    }
}