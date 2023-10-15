namespace Webpage_As_A_Background
{
    partial class PageHost
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            blazorWebView = new Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView();
            windowInitTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // blazorWebView
            // 
            blazorWebView.Dock = DockStyle.Fill;
            blazorWebView.Location = new Point(0, 0);
            blazorWebView.Name = "blazorWebView";
            blazorWebView.Size = new Size(10, 10);
            blazorWebView.TabIndex = 0;
            blazorWebView.Text = "blazorWebView";
            // 
            // windowInitTimer
            // 
            windowInitTimer.Interval = 1000;
            windowInitTimer.Tick += WindowInitTimer_Tick;
            // 
            // PageHost
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(10, 10);
            Controls.Add(blazorWebView);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PageHost";
            StartPosition = FormStartPosition.Manual;
            Text = "PageHost";
            WindowState = FormWindowState.Minimized;
            Shown += PageHost_Shown;
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.AspNetCore.Components.WebView.WindowsForms.BlazorWebView blazorWebView;
        private System.Windows.Forms.Timer windowInitTimer;
    }
}