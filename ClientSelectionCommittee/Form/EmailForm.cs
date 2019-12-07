using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSelectionCommittee
{
    public partial class EmailForm : Form
    {
        public EmailForm(EnrolleeSend enrolleeSend)
        {
            InitializeComponent();
            this.enrolleeSend = enrolleeSend;
        }

        EnrolleeSend enrolleeSend = null;

        private void EmailForm_Load(object sender, EventArgs e)
        {
            toolStripLabel2.Text = $"{enrolleeSend.EnrolleeLastname} {enrolleeSend.EnrolleeFirstname[0]}. {enrolleeSend.EnrolleePatronymic[0]}.";
            toolStripLabel4.Text = enrolleeSend.EnrolleeEmail;
        }

        private void ButtonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                labelFile.Text = ofd.FileName;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            labelFile.Text = "Путь файла";
        }

        public string GetPass()
        {
            using (StreamReader streamReader = new StreamReader(@"D:\pass.txt"))
            {
                return streamReader.ReadLine();
            }
        }

        private void ButtonSend_Click(object sender, EventArgs e)
        {
            try
            {

                // отправитель - устанавливаем адрес и отображаемое в письме имя
                MailAddress from = new MailAddress("program.test@bk.ru", "Приёмная комиссия ПолесГУ");
                // кому отправляем
                MailAddress to = new MailAddress(enrolleeSend.EnrolleeEmail);
                // создаем объект сообщения
                MailMessage m = new MailMessage(from, to);
                // тема письма
                m.Subject = textBox1.Text;
                // текст письма
                m.Body = textBoxText.Text;
                // письмо представляет код html
                m.IsBodyHtml = true;

                if (labelFile.Text != "Путь файла" && labelFile.Text != "" && labelFile.Text != null)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(labelFile.Text);
                    m.Attachments.Add(attachment);
                }

                // адрес smtp-сервера и порт, с которого будем отправлять письмо
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
                // логин и пароль
                smtp.Credentials = new NetworkCredential("program.test@bk.ru", GetPass());
                smtp.EnableSsl = true;
                smtp.Send(m);

                MessageBox.Show("Сообщение успешно отправлено!", "Отправка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Все поля должны быть заполнены!\n " + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
