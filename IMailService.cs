using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Services
{
    public interface IMailService
    {

        Task SendEmailAsync(string toEmail, string subject, string content);
    }

    public class SendGridMailService : IMailService
    {
        private IConfiguration _configuration;

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("zainolnabi88@gmail.com", "Triton Express");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);


            /*var apiKey = _configuration["SendGridKey"];
            var client = new SendGridClient(apiKey);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom("zainolnabi88@gmail.com", "Triton Express");
            sendGridMessage.AddTo(toEmail, "");
            sendGridMessage.SetTemplateId("d-bfc168d562374a7fb827953114b5b1be");
            sendGridMessage.SetTemplateData(new HelloEmail
            {
                UserId = toEmail,
                callbackUrl = content
            });

            var response = client.SendEmailAsync(sendGridMessage).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                String res = "Email Sent";
            }*/
        }

        /*private class HelloEmail
        {
            [JsonProperty("UserId")]
            public string UserId { get; set; }

            [JsonProperty("callbackUrl")]
            public string callbackUrl { get; set; }
        }*/
    }
}
