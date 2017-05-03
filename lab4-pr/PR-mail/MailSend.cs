using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR_mail
{
    public partial class MailSend : Form
    {
        private Attachment at;
        private MailMessage mail;
        private bool setAttachment = false;

        public MailSend()
        {
            InitializeComponent();

            pictureBox1.AutoSize = true;
            pictureBox1.Image = Properties.Resources.attach;

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(textBox2.Text);

                mail.From = new MailAddress(textBox1.Text);
                mail.To.Add(textBox3.Text);
                mail.Subject = richTextBox1.Text;
                mail.Body = richTextBox2.Text;

                if(checkBox1.Checked)
                    mail.IsBodyHtml = true;
                else
                    mail.IsBodyHtml = false;

                if (setAttachment)
                {
                    mail.Attachments.Add(at);
                }
                
                SmtpServer.Port = 23;

                SmtpServer.Send(mail);
                
                MessageBox.Show("Mail Send!");

                SmtpServer.Dispose();
                mail.Dispose();

                if (setAttachment)
                {
                    at.Dispose();
                    setAttachment = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Attach_file(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    at = new Attachment(open.FileName);
                    setAttachment = true; 
                }
            }
        }
    }
}
