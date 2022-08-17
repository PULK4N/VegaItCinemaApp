using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppContracts.Request.ReservationRequests;
using CinemaAppServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppContracts.Interfaces
{
    public interface IEmailService
    {
        Task SendReservationConfirmationMailToUser(string email, MovieScreeningForEmailRequest movieScreeningDTO, ReservationResponse reservation);
        Task SendEmailConfirmationToken(string email, string token);
        Task SendEmailResetPasswordLink(string email, string token);
    }
}
