using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PSO.Utilities
{
    public class Mailing
    {
        private const string NOREPLY_EMAIL = "noreply@pso-pr.com",
            PSO_SMTP_SERVER = "relay-hosting.secureserver.net",
            CLIENT_EMAIL = "",
            CLIENT = "",
            INTERNAL_PHONE = "",
            SYSTEM_TYPE = "",
            PAGE_URI = "";

        private string _smtp = "";

        private int _port = 0;


        public Mailing(int port, string smtp)
        {
            _port = port;

            _smtp = smtp;
        }

        public static string GetPSOSMTPServer()
        {
            return PSO_SMTP_SERVER;
        }

        public int GetPort()
        {
            return _port;
        }

        public string GetSMTP()
        {
            return _smtp;
        }

        public static string GetNoReplyEmail()
        {
            return NOREPLY_EMAIL;
        }

        public static string GetInternalPhone()
        {
            return INTERNAL_PHONE;
        }

        public static string GetPageUri()
        {
            return PAGE_URI;
        }

        public MailMessage ComposeEmail(LinkedList<string> to, string subj, string body)
        {
            MailMessage msg = new MailMessage();

            for (int i = 0; i < to.Count; i++)
            {
                msg.To.Add(to.ElementAt(i));
            }

            msg.From = new MailAddress(NOREPLY_EMAIL);

            msg.Subject = subj;

            msg.Body = body;

            msg.Priority = MailPriority.High;

            //This is after demo shit
            //msg.Body = body.Replace("@CLIENTEMAIL", CLIENT_EMAIL).Replace("@CLIENT", CLIENT).Replace(
            //"@PHONE", INTERNAL_PHONE).Replace("@TYPE", SYSTEM_TYPE);

            msg.IsBodyHtml = true;

            return msg;

        }

        public Exception SendEmail(MailMessage email)
        {
            Exception error = null;

            using (SmtpClient client = new SmtpClient())
            {
                client.Port = _port;

                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                //client.UseDefaultCredentials = false;

                client.Host = _smtp;

                //client.EnableSsl = true;

                //client.Credentials = new System.Net.NetworkCredential(email.From.ToString(), @"PSOAdmin123!@#");

                try
                {
                    client.Send(email);                                     
                }

                catch (Exception ex)
                {
                    error = ex;
                }

                finally
                {
                    email.Dispose();                    
                }
            }

            return error;
        }
    }
}