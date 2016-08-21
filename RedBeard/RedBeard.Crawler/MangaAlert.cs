using RedBeard.Crawler.Credential;
using RedBeard.Crawler.Model;
using System;
using System.Net.Mail;
using System.Text;

namespace MangaReminder.Crawler
{
    public sealed class MangaAlert
    {
        public string TargetMail { get; set; }

        public MangaAlert(string target)
        {
            this.TargetMail = target;
        }

        public void SendIt (Manga manga)
        {
            // Instancia um novo email
            MailMessage mailMessage = new MailMessage(LoginConfig.Login, this.TargetMail, "Nova edição - " + manga.Name, this.GetMailBody(manga));

            // Instancia o cliente SMTP para envio de email e suas propriedades
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(LoginConfig.Login, LoginConfig.Password);

            // Envia o email
            client.Send(mailMessage);
        }

        internal string GetMailBody (Manga manga)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Nome: ");
            builder.Append(manga.Name);
            builder.Append(Environment.NewLine);

            builder.Append("Preço: ");
            builder.Append(manga.Price.ToString("C"));
            builder.Append(Environment.NewLine);

            builder.Append("Data de lançamento: ");
            builder.Append(manga.ReleaseDate.ToShortDateString());
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
    }
}
