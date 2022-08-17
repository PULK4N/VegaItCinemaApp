using CinemaAppContracts.DTO.MovieScreeningDTOs;
using CinemaAppContracts.DTO.ReservationDTOs;
using CinemaAppDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppServiceAbstractions.Interfaces
{
    public interface IEmailService
    {
        Task SendReservationConfirmationMailToUser(string email, MovieScreeningForEmailingDTO movieScreeningDTO, ReservationForReturningDTO reservation);
        Task SendEmailConfirmationToken(string email, string token);
        Task SendEmailResetPasswordLink(string email, string token);
    }
}
