using System.Windows;
using SupBB.Controller;
using SupBB.Model;
using System.Net;
using System.Net.Mail;
using System;
using System.Text;

namespace SupBB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void bntSend_Click(object sender, RoutedEventArgs e)
        {
            Messages newMessage = new Messages();
            MyPC myPC = new MyPC();
            User user = new User();
            GmailSettings settings = new GmailSettings();

            if(CheckTxtBox() == true)
            {
                user.LoginSender = txtLoginSender.Text;
                user.PhoneSender = txtPhoneSender.Text;
                user.CitySender = txtCitySender.Text;
                user.StreetSender = txtStreetSender.Text;
                newMessage.TextMessage = txtMessage.Text;

                MailAddress from = new MailAddress(newMessage.Sender(), user.LoginSender);
                MailAddress to = new MailAddress(newMessage.Recipient()); 

                MailMessage message = new MailMessage(from, to);

                message.Subject = newMessage.TextMessage; 
                message.Body = string.Format("Адрес ППС - {0} {1}. Имя ПК - {2}",user.CitySender, user.StreetSender, myPC.PcName());
                    
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(settings.Host(), Convert.ToInt32(settings.Port()));

                smtp.Credentials = new NetworkCredential(newMessage.Sender(), settings.Password());
                smtp.EnableSsl = true;

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                    ex.ToString());
                }
            } 
            else
            {
                MessageBox.Show("Что то пошло не так!");
            }
            MessageBox.Show("Сообщение отправлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
        public bool CheckTxtBox()
        {
            if (String.IsNullOrEmpty(txtLoginSender.Text) || String.IsNullOrEmpty(txtPhoneSender.Text)
                || String.IsNullOrEmpty(txtCitySender.Text) || String.IsNullOrEmpty(txtStreetSender.Text)
                || String.IsNullOrEmpty(txtMessage.Text))
            {
                MessageBox.Show("Заполните поля");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
