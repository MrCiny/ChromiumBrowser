using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromiumBrowser
{
    public partial class Browser : Form
    {
        private ChromiumWebBrowser browser;
        int tabCount = 0;

        public Browser()
        {
            InitializeComponent();
            InitializeBrowser();
            InitializeForm();
        }

        private void InitializeForm()
        {

            BrowserTabs.Height = ClientRectangle.Height - 25;
        }

        private void InitializeBrowser()
        {
            Cef.Initialize(new CefSettings());
            AddBrowser();
        }

        private void AddBrowser()
        {
            browser = new ChromiumWebBrowser("https://datorium.eu/");
            browser.Dock = DockStyle.Fill;
            BrowserTabs.TabPages[tabCount].Controls.Add(browser);
            browser.AddressChanged += Browser_AddressChanged;
            browser.TitleChanged += Browser_TitleChanged;
            tabCount++;
        }

        private void NewTab()
        {
            var newTabPage = new TabPage();

            newTabPage.Text = "New Tab";
            BrowserTabs.TabPages.Add(newTabPage);
            BrowserTabs.SelectedTab = newTabPage;
            AddBrowser();
        }

        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            try
            {
                browser.Load("https://google.com");

            }
            catch
            {

            }
        }

        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            var selectedBrowser = (ChromiumWebBrowser)sender;
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripTextBoxAddressBar.Text = e.Address;
            }));
        }

        private void toolStripTextBoxAddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Navigate(toolStripTextBoxAddressBar.Text);
            }
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            browser.Reload();
        }

        private void Navigate(string address)
        {
            try
            {
                var selectedBrowser = (ChromiumWebBrowser)BrowserTabs.SelectedTab.Controls[0];

                selectedBrowser.Load(address);
                selectedBrowser.TitleChanged += Browser_TitleChanged;
                
            }
            catch
            {

            }
        }

        private void toolStripButtonAddTab_Click(object sender, EventArgs e)
        {
            NewTab();
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            var selectedBrowser = (ChromiumWebBrowser)sender;
            this.Invoke(new MethodInvoker(() =>
            {
                selectedBrowser.Parent.Text = e.Title;
            }));
        }
    }
}
