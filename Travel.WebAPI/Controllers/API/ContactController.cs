using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Mail;
using System.Web.Script.Serialization;

public class ContactMessageModel
{
   public string name { get; set; }
    public string email { get; set; }
    public string messagetext { get; set; }
    public string subject { get; set; }
}

namespace Travel.WebAPI.Controllers.API
{
    public class ContactController : ApiController
    {

        private JavaScriptSerializer jss = new JavaScriptSerializer();

        // POST api/<controller>
        public string Post( ContactMessageModel message)
        {
            try
            {
                            
                MailMessage msg = new MailMessage();
                msg.To.Add("nomads@intrepidnomads.com");

                MailAddress address = new MailAddress(message.email);

                msg.Subject = message.name + " : " + message.subject;
                msg.Body = message.messagetext;

                //Configure an SmtpClient to send the mail.
                SmtpClient client = new SmtpClient("relay.hostinguk.net");
                client.EnableSsl = false;

                //Send the msg
                client.Send(msg);

                return "Message Sent";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}