using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedNet_10
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private string string_use;
        private string pages_opened;
        public static int num_use;
        public static int num_pages;


        public Form1()
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();

            // Initialize contextMenu1
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItem1, this.menuItem2, this.menuItem3 });

            // Initialize menuItem1
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Back to web";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Go to 'Google'";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);

            this.menuItem3.Index = 2;
            this.menuItem3.Text = "Exit";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit requested by notifyIcon");
            Application.Exit();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Home requested by notifyIcon");
            this.Show();
            webBrowser1.Navigate("https://google.com/");
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Showing requested by notifyIcon");
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit requested by button");
            this.Hide();

            string path = @"C:/Alexlegarnd/SpeedNet/SNStat.txt";
            Console.WriteLine("Modifying 'SNStat.txt'");

            File.Delete(path);
            System.Threading.Thread.Sleep(3000);

            File.Create(path).Dispose();
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine(num_use);
                tw.WriteLine(num_pages);
                tw.Close();
            }
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Done");
            Console.WriteLine("Closing...");
            Application.Exit();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
            Console.WriteLine("Download complete : " + textBox1.Text);
            if (webBrowser1.Document.Url.ToString().StartsWith("res:"))
            {
                Console.WriteLine("Url is not valid (Not Found)");
                webBrowser1.Navigate("C:/Alexlegarnd/SpeedNet/error.html");
                Console.WriteLine("Openning 'C:/Alexlegarnd/SpeedNet/error.html'");
            }

            if(num_pages != 0)
            {
                num_pages = num_pages + 1;
                Console.WriteLine(num_pages + " page(s) openned");
            }
            else
            {
                Console.WriteLine("Waiting openning stat file");
            }
            
            Console.WriteLine("Done");
            
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            Console.WriteLine("Downloading on " + textBox1.Text);
            label1.Text = webBrowser1.DocumentTitle;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Forward requested");
            webBrowser1.GoForward();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Back requested");
            webBrowser1.GoBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hiding...");
            this.Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ContextMenu = contextMenu1;
            notifyIcon1.BalloonTipText = "SpeedNet est actuellement dans la zone de notification";
            notifyIcon1.BalloonTipTitle = "SpeedNet 10";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Showing...");
            this.Show();
            notifyIcon1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Navigate to https://google.com/");
            webBrowser1.Navigate("https://google.com/");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "SpeedNet:Stat")
            {

                if (num_pages != 0)
                {
                    AboutBox1 secret_window = new AboutBox1();
                    secret_window.Show();
                }
                else
                {
                    textBox1.Text = "Wait a minute !";
                }

            }
            else
            {
                Console.WriteLine("Navigate to " + textBox1.Text);
                webBrowser1.Navigate(textBox1.Text);
            }
            
        }

         private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (textBox1.Text == "SpeedNet:Stat")
                {

                    if (num_pages != 0)
                    {
                        AboutBox1 secret_window = new AboutBox1();
                        secret_window.Show();
                    }
                    else
                    {
                        textBox1.Text = "Wait a minute !";
                    }

                }
                else
                {
                    Console.WriteLine("Navigate to " + textBox1.Text);
                    webBrowser1.Navigate(textBox1.Text);
                }
            }
        
         }

        public void Stat()
        {
            string path = @"C:/Alexlegarnd/SpeedNet/SNStat.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("Creating 'SNStat.txt'");
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    Console.WriteLine("");
                    tw.WriteLine("1");
                    Console.WriteLine("num_use is 1");
                    tw.WriteLine("1");
                    Console.WriteLine("pages_opened is 1");
                    this.Invoke((MethodInvoker)delegate {

                        button1.Enabled = false;

                    });
                    System.Threading.Thread.Sleep(2000);

                    this.Invoke((MethodInvoker)delegate {

                        button1.Enabled = true;
                        num_use = 1;
                        num_pages = 1;

                    });

                    tw.Close();
                }
               
            }
            else if (File.Exists(path))
            {

                    this.Invoke((MethodInvoker)delegate {

                        button1.Enabled = false;
                        Console.WriteLine("Getting 'num_use'");
                        string_use = File.ReadLines(path).First();

                    });

                System.Threading.Thread.Sleep(2000);

                    this.Invoke((MethodInvoker)delegate {

                        Console.WriteLine("Getting 'pages_opened'");
                        pages_opened = File.ReadLines(path).Last();

                    });

                System.Threading.Thread.Sleep(2000);

                this.Invoke((MethodInvoker)delegate {

                    num_use = Int32.Parse(string_use);
                    num_pages = Int32.Parse(pages_opened);

                });
                num_use = num_use + 1;

                this.Invoke((MethodInvoker)delegate {

                    button1.Enabled = true;

                });

                Console.WriteLine("");
                Console.WriteLine("num_use is " + num_use);
                Console.WriteLine("pages_opened is "+ pages_opened);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Thread FunStat = new System.Threading.Thread(Stat);
            FunStat.Start();
        }

        SHDocVw.WebBrowser nativeBrowser;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            nativeBrowser = (SHDocVw.WebBrowser)webBrowser1.ActiveXInstance;
            nativeBrowser.NewWindow2 += nativeBrowser_NewWindow2;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            nativeBrowser.NewWindow2 -= nativeBrowser_NewWindow2;
            base.OnFormClosing(e);
        }

        void nativeBrowser_NewWindow2(ref object ppDisp, ref bool Cancel)
        {
            var popup = new Form2();
            popup.Show(this);
            ppDisp = popup.Browser.ActiveXInstance;
        }
    }
}
