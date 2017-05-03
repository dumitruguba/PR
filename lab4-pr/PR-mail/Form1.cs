
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR_mail
{
    public partial class Form1 : Form
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamReader reader, retrReader;
        private byte[] bytes;
        private int nr_of_mails = 1;

        public Form1()
        {
            InitializeComponent();
            MailSend send = new MailSend();
            send.Show();
            textBox2.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient("127.0.0.1", 110);
                networkStream = tcpClient.GetStream();
                reader = new StreamReader(networkStream);
                retrReader = new StreamReader(networkStream);
                reader.ReadLine();

                string command = "User " + textBox1.Text + "\r\n";
                WriteBytes(command);

                command = "Pass " + textBox2.Text + "\r\n";
                WriteBytes(command);

                command = "List\r\n";
                WriteBytes(command);

                listBox1.Items.Clear();

                string output;

                while ((output = reader.ReadLine()) != ".")
                {
                    listBox1.Items.Add(output);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string command = "Retr " + (listBox1.SelectedIndex + 1) + "\r\n";
                bytes = Encoding.ASCII.GetBytes(command);
                networkStream.Write(bytes, 0, bytes.Length);
                retrReader.ReadLine();

                Form mailForm = new Form();
                mailForm.AutoSize = true;
                RichTextBox r = new RichTextBox();
                r.Size = new Size(400, 300);
                mailForm.Controls.Add(r);
                mailForm.Show();

                string output;
                while ((output = retrReader.ReadLine()) != ".")
                {
                    r.Text += output + "\r\n";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string command = "Dele " + (listBox1.SelectedIndex + 1) + "\r\n";
                WriteBytes(command);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string qcommand = "Quit" + "\r\n";
                WriteBytes(qcommand);

                listBox1.Items.Clear();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string qcommand = "Quit" + "\r\n";
                WriteBytes(qcommand);

                tcpClient = new TcpClient("127.0.0.1", 110);
                networkStream = tcpClient.GetStream();
                reader = new StreamReader(networkStream);
                retrReader = new StreamReader(networkStream);
                reader.ReadLine();

                string command = "User " + textBox1.Text + "\r\n";
                WriteBytes(command);

                command = "Pass " + textBox2.Text + "\r\n";
                WriteBytes(command);

                command = "List\r\n";
                WriteBytes(command);

                listBox1.Items.Clear();

                string output;

                while ((output = reader.ReadLine()) != ".")
                {
                    listBox1.Items.Add(output);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private void WriteBytes(string s)
        {
            bytes = Encoding.ASCII.GetBytes(s);
            networkStream.Write(bytes, 0, bytes.Length);
            reader.ReadLine();
        }
    }
}
