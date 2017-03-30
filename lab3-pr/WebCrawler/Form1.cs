using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using HtmlAgilityPack;
using NUglify;

namespace WebCrawler
{
    public partial class Form1 : Form
    {
        private List<string> links;
        private List<string> auxList;

        private ManualResetEvent allThreads = new ManualResetEvent(false);

        private int contor = 0;
        private int nrOfSearch = 0;
        private bool contains;

        private String initialLink = null;

        public Form1()
        {
            InitializeComponent();
            links = new List<string>();
            auxList = new List<string>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nrOfSearch++;
            
            allThreads.Reset();

            richTextBox1.Text = null;
            richTextBox2.Text = null;


            setLinks();
            
            contor = links.Count;

            foreach (var link in links)
            {
                Console.WriteLine(link);
                richTextBox1.Text += link;
                richTextBox1.Text += "\n\n";
            }
            
            foreach (var link in links)
            {
                Thread t = new Thread(() => checkWords(link));
                t.Start();
            }

            Thread fin = new Thread(this.finish);
            fin.Start();

        }

        public void setLinks()
        {
            Console.WriteLine(nrOfSearch);

            if (nrOfSearch == 1)
                initialLink = textBox1.Text;
            else
            {
                var result = links.Except(auxList);

                initialLink = result.First();
                textBox1.Text = initialLink;
                links.Clear();
            }

            auxList.Add(initialLink);
            

            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = hw.Load(initialLink);

            links.Add(initialLink);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                string hrefValue = link.GetAttributeValue("href", string.Empty);
                if (hrefValue != "javascript:void(0);" && hrefValue != "javascript:void(0)")
                {
                    if (!hrefValue.StartsWith("http") && !hrefValue.StartsWith(" http") &&
						!hrefValue.StartsWith("https") && !hrefValue.StartsWith(" https"))
                        links.Add(textBox1.Text + hrefValue);
                    else
                        links.Add(hrefValue);
                }
            }
        }
        

        public void checkWords(string strLink)
        {
            string pageContent = null;

            try
            {
                HttpWebRequest myReq = (HttpWebRequest) WebRequest.Create(strLink);
                myReq.Method = "Get";
                myReq.Timeout = 360000;

                HttpWebResponse myres = (HttpWebResponse) myReq.GetResponse();

                using (StreamReader sr = new StreamReader(myres.GetResponseStream()))
                {
                    pageContent = sr.ReadToEnd();
                }

                string text = null;

         

                text = Uglify.HtmlToText(pageContent).ToString();

                MethodInvoker inv = delegate
                {
                    richTextBox2.Text += "\n" + strLink + " --> " + Regex.Matches(text, textBox2.Text).Count + "\n";
                };

                try
                {
                    Invoke(inv);
                }
                catch (Exception exception)
                {
                }

                myres.Close();
                myres.Dispose();


            }
            catch (System.Net.WebException exception)
            {
                MethodInvoker inv = delegate
                {
                    richTextBox2.Text += "\n" + strLink + " --> " + exception + "\n";
                };

                try
                {
                    Invoke(inv);
                }
                catch (Exception exception2)
                {
                }
            }

            Console.WriteLine(contor);

            if (Interlocked.Decrement(ref contor) == 0)
            {
                allThreads.Set();
            }

        }

        public void finish()
        {
            allThreads.WaitOne();
            MethodInvoker inv = delegate
            {
                richTextBox2.Text += "\n\n\t\t\t\t\tFinish!\n";
            };

            try
            {
                Invoke(inv);
            }
            catch (Exception exception2)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
