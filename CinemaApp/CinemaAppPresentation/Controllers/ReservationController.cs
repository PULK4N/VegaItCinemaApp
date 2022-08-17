using CinemaAppContracts.Request.ReservationRequests;
using Microsoft.AspNetCore.Mvc;
using CinemaAppServices.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CinemaAppContracts.Interfaces;
using CinemaAppContracts.Request.SeatRequests;
using AutoMapper;
using CinemaAppContracts.DTO.ReservationDTOs;

namespace CinemaAppPresentation.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public ReservationController(UserManager<User> userManager, IReservationService reservationService, IMapper mapper)
        {
            _userManager = userManager;
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("reserveTickets")]
        public async Task<IActionResult> ReserveTickets([FromBody] ReservationForCreatingDTO reservationDTOs)
        {
            Guid userId = await GetUserId();
            ICollection<SeatCreateRequest> seatRequests = GenerateSeatRequests(reservationDTOs);
            if (IsAuthenticated(userId))
            {
                await _reservationService.CreateAsync(userId, seatRequests);
            }
            else
            {
                await _reservationService.CreateAsync(reservationDTOs.Email, seatRequests);
            }
            return Ok();
        }

        private ICollection<SeatCreateRequest> GenerateSeatRequests(ReservationForCreatingDTO reservationDTOs)
        {
            ICollection<SeatCreateRequest> seatCreateRequests = new List<SeatCreateRequest>();
            foreach(var seat in reservationDTOs.Seats)
            {
                seatCreateRequests.Add(_mapper.Map<SeatCreateRequest>(seat));
            }
            return seatCreateRequests;
        }

        private bool IsAuthenticated(Guid userId)
        {
            return Guid.Empty != userId;
        }

        private async Task<Guid> GetUserId()
        {
            if (User?.Identity?.Name is not null)
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                return user.Id;
            }
            return Guid.Empty;
        }
        [Authorize]
        [HttpDelete("{reservationId:guid}")]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            await _reservationService.DeleteAsync(reservationId);
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [Route("my-reservations")]
        public async Task<IActionResult> GetMyReservations()
        {
            Guid userId = await GetUserId();
            return Ok(await _reservationService.GetMyReservationsAsync(userId));
        }

        [HttpPost]
        [Authorize]
        [Route("rate-reservation")]
        public async Task<IActionResult> RateReservation([FromBody] ReservationForRatingDTO reservationForRatingDTO)
        {
            Guid userId = await GetUserId();
            await _reservationService.RateReservation(userId, reservationForRatingDTO.Id, reservationForRatingDTO.Score);
            return Ok();
        }
    }
}
