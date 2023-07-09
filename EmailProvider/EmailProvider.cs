using System.Net.Mail;
using TrainingAlex.Containers;

namespace TrainingAlex.EmailProvider
{
    public class EmailProvider
    {
        public void SendEmail (TransportContainer transportContainer)
        {
            MailMessage mail = CreateEmail(transportContainer);

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "XXXXXX@gmail.com",
                Password = "YYYYYYYY"
            };

            smtpClient.EnableSsl = true;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smtpClient.Send(mail);
        }

        private MailMessage CreateEmail (TransportContainer transportContainer)
        {

            if (transportContainer == null || transportContainer.MyEmailContainer == null || transportContainer.MyOptionsContainer == null) return;
            
            MailMessage mail = new MailMessage("fromm@gmail.com", "toaddress@gmail.com");
            mail.Subject = transportContainer.MyEmailContainer.Subject;
            mail.Body = transportContainer.MyEmailContainer.Body;

            //My email provider only sends email to me. To other recipients, add here. 
            //foreach (string st in transportContainer.MyEmailContainer.ToRecipients)
            //{
            //    mail.To.Add(st);
            //}

            return mail;
        }
    }
}
