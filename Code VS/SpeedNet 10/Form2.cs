using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedNet_10
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public WebBrowser Browser
        {
            get { return webBrowser1; }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.Text = webBrowser1.DocumentTitle;
            if (webBrowser1.Document.Url.ToString().StartsWith("res:"))
            {
                Console.WriteLine("Url is not valid (Not Found)");
                webBrowser1.Navigate("C:/Alexlegarnd/SpeedNet/error.html");
                Console.WriteLine("Openning 'C:/Alexlegarnd/SpeedNet/error.html'");
            }
        }
    }
}
