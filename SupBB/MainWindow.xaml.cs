using System.Windows;
using SupBB.Controller;
using SupBB.Model;
using System.Net;
using System.Net.Mail;
using System;
using System.Text;
using System.Text.RegularExpressions;

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
                string oper = string.Format("Город {0}, {1}. {2}", user.CitySender, user.StreetSender , user.LoginSender);


                MailAddress from = new MailAddress(newMessage.Sender(), oper);
                MailAddress to = new MailAddress(newMessage.Recipient()); 

                MailMessage message = new MailMessage(from, to);
                var selectedItem = cmbItem.Text.ToString();


                message.Subject = string.Format("{0}", selectedItem); 
                     
                message.Body = string.Format("{0}. Имя ПК - ( {1} ) Телефон -{2}.", newMessage.TextMessage, myPC.PcName(), user.PhoneSender);
                    
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
                MessageBox.Show("Сообщение отправлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            } 
        }
        public bool CheckTxtBox()
        {
            if (String.IsNullOrEmpty(txtLoginSender.Text) || String.IsNullOrEmpty(txtPhoneSender.Text)
                || String.IsNullOrEmpty(txtCitySender.Text) || String.IsNullOrEmpty(txtStreetSender.Text)
                || String.IsNullOrEmpty(txtMessage.Text) || String.IsNullOrEmpty(cmbItem.Text))
            {
                MessageBox.Show("Заполните поля");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void txtCitySender_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            string inputSymbol = e.Text.ToString();

            if (!Regex.Match(inputSymbol, @"[а-яА-Я]").Success)
            {
                e.Handled = true;
            }
        }
    }
}
