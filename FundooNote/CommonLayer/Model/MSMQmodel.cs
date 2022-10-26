using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQmodel
    {
        MessageQueue messageQ = new MessageQueue();

        public void sendData2Queue(string Token)
        {
            messageQ.Path = @".\private$\Token";
            if (!MessageQueue.Exists(messageQ.Path))
            {
                //Exists
                MessageQueue.Create(messageQ.Path);
            }

            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            messageQ.Send(Token);
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string Token = msg.Body.ToString();
            string subject = "FundooNotes Reset Link";
            string body = Token;
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("joybiswas1911@gmail.com", "pvtnvosghvelnpex"),
                EnableSsl = true,
            };

            SMTP.Send("joybiswas1911@gmail.com", "joybiswas1911@gmail.com", subject, body);

            messageQ.BeginReceive();
        }
    }
}
