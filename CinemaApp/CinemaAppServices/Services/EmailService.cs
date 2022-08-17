using System.Text;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using CinemaAppContracts.Request.ReservationRequests;
using CinemaAppContracts.Request.SeatRequests;
using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppContracts.Interfaces;
using System.Web;

namespace CinemaAppContracts.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailConfirmationToken(string email, string token)
        {
            string subject = "Confirm your account";
            //TODO: encode token
            string content = $"To confirm your account click on link " +
                $"{_configuration["FrontSettings:Host"]}confirm-email?email={HttpUtility.UrlEncode(email)}&token={HttpUtility.UrlEncode(token)}";
            SendMessage(subject, content, email);
        }

        public async Task SendEmailResetPasswordLink(string email, string token)
        {
            //TODO: encode token
            string subject = "Reset your password";
            string content = $"To reset your password go to a link {_configuration["FrontSettings:Host"]}reset-password?email={HttpUtility.UrlEncode(email)}&token={HttpUtility.UrlEncode(token)}";
            SendMessage(subject, content, email);
        }

        public async Task SendReservationConfirmationMailToUser(string email, MovieScreeningForEmailRequest movieScreeningForEmailRequest, ReservationResponse reservationResponse)
        {
            string content = GetReservationMessageContent(movieScreeningForEmailRequest, reservationResponse);
            string subject = "You have successfully made a reservation!";
            SendMessage(subject, content, email);
        }

        private string GetReservationMessageContent(MovieScreeningForEmailRequest movieScreeningForEmailRequest, ReservationResponse reservationResponse)
        {
            StringBuilder reservationContent = new StringBuilder();
            reservationContent.Append("You have successfully made a reservation!")
                .Append("\nReservation information:")
                .Append($"\nMovie name:{movieScreeningForEmailRequest.MovieName}")
                .Append($"\nMovie start time:{movieScreeningForEmailRequest.StartTime}")
                .Append($"\n\tPrice: {reservationResponse.Price}");
            foreach (SeatResponse seatDTO in reservationResponse.Seats)
            {
                reservationContent.Append($"\n\tSeat:");
                reservationContent.Append($"\n\t\tRow: {seatDTO.Row}");
                reservationContent.Append($"\n\t\tColumn: {seatDTO.Column}");
            }
            return reservationContent.ToString();
        }

        private void SendMessage(string subject, string content, string toEmail)
        {
            var emailMessage = new MimeMessage();
            emailMessage.To.Add(MailboxAddress.Parse(toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = content };

            using (var smtp = new SmtpClient())
            {
                var host = _configuration["MailSettings:Host"];
                var port = int.Parse(_configuration["MailSettings:Port"]);
                var tsl = SecureSocketOptions.StartTls;
                emailMessage.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
                string senderMail = _configuration["MailSettings:Mail"];
                string password = _configuration["MailSettings:Password"];
                smtp.Connect(host, port, tsl);
                smtp.Authenticate(senderMail, password);
                smtp.Send(emailMessage);
            }
        }

    }
}