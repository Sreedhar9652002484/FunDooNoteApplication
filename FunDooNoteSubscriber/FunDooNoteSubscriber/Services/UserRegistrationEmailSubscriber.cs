using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
//using CommonLayer.Model;
using MassTransit;
using FunDooNoteSubscriber.Models;

namespace NoteAppSubscriber.Services
{
    public class UserRegistrationEmailSubscriber : IConsumer<UserRegistrationMessage>
    {
        public async Task Consume(ConsumeContext<UserRegistrationMessage> context)
        {
            var userRegistrationMessage = context.Message;

            // Send a welcome email to the registered user using an email service
            await SendWelcomeEmail(userRegistrationMessage.Email);
        }

        private async Task SendWelcomeEmail(string email)
        {
            // Implement your logic to send a welcome email here
            // You can use an SMTP client or any email service API
            // Example using SendGrid:
            // await _sendGridService.SendWelcomeEmailAsync(email);
            //

            try
            {

                // Configure SMTP settings
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("nsridhary2k@gmail.com", "jtlicbixeacrsept");
                    smtpClient.EnableSsl = true;

                    // Create the email message
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("nsridhary2k@gmail.com");
                        mailMessage.To.Add(email);
                        mailMessage.Subject = "Welcome to Our App!";
                        mailMessage.Body = "Thank you for registering. Welcome to our app!";
                        mailMessage.IsBodyHtml = true;

                        // Send the email
                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
