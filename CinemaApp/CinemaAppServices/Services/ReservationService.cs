using CinemaAppContracts.Request.ReservationRequests;
using CinemaAppContracts.Request.SeatRequests;
using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using CinemaAppContracts.Interfaces;
using System.Linq.Expressions;
using CinemaAppServices.Exceptions.Reservation;
using CinemaAppServices.Exceptions.MovieScreening;
using AutoMapper;

namespace CinemaAppContracts.Services
{
    internal sealed class ReservationService : IReservationService
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMovieService _movieScreeningService;

        public ReservationService(IUnitOfWork unitOfWork, IEmailService emailService, IMovieService movieService,IMapper mapper)
        {
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _movieScreeningService = movieService;
        }

        public async Task<ReservationResponse> CreateAsync(Guid buyerId, ICollection<SeatCreateRequest> seatForCreatingDTOs, CancellationToken cancellationToken = default)
        {
            if (NoSeatsToReserve(seatForCreatingDTOs))
                throw new NoSeatsToReserveException();

            MovieScreening movieScreening = await FindMovieScreening(seatForCreatingDTOs);
            string movieName = await GetMovieName(movieScreening);
            Reservation reservation = await CreateReservation(buyerId, movieScreening, seatForCreatingDTOs);
            User user = await _unitOfWork.UserRepository.GetbyIdAsync(buyerId);
            _emailService.SendReservationConfirmationMailToUser(user.Email, new MovieScreeningForEmailRequest(movieScreening, movieName), _mapper.Map<ReservationResponse>(reservation));
            return _mapper.Map<ReservationResponse>(reservation);
        }

        private async Task<string> GetMovieName(MovieScreening movieScreening)
        {
            return (await _unitOfWork.MovieRepository.GetbyIdAsync(movieScreening.MovieId)).Name;
        }

        public async Task<ReservationResponse> CreateAsync(string email, ICollection<SeatCreateRequest> seatForCreatingDTOs, CancellationToken cancellationToken = default)
        {
            if (NoSeatsToReserve(seatForCreatingDTOs))
                throw new NoSeatsToReserveException();
            MovieScreening movieScreening = await FindMovieScreening(seatForCreatingDTOs);
            string movieName = await GetMovieName(movieScreening);
            Reservation reservation = await CreateReservation(Guid.Empty, movieScreening, seatForCreatingDTOs);
            _emailService.SendReservationConfirmationMailToUser(email, new MovieScreeningForEmailRequest(movieScreening, movieName), _mapper.Map<ReservationResponse>(reservation));
            return _mapper.Map<ReservationResponse>(reservation);
        }

        private async Task<Reservation> CreateReservation(Guid buyerId,MovieScreening movieScreening, ICollection<SeatCreateRequest> seatForCreatingDTOs)
        {
            await ValidateReservation(movieScreening, seatForCreatingDTOs);
            var reservation = new Reservation();
            reservation.Price = CalculateReservationPrice(IsAuthenticated(buyerId), movieScreening, seatForCreatingDTOs.Count);
            reservation.TimeOfBuying = DateTime.Now;
            reservation.UserId = buyerId;
            _unitOfWork.ReservationRepository.Insert(reservation);
            InsertSeats(seatForCreatingDTOs,reservation);
            await _unitOfWork.SaveChangesAsync();
            return reservation;
        }
        private async Task ValidateReservation(MovieScreening movieScreening, ICollection<SeatCreateRequest> seatForCreatingDTOs)
        {
            if(movieScreening.NumOfColumns*movieScreening.NumOfRows < seatForCreatingDTOs.Count)
            {
                throw new TooManySeatsException();
            }
            await ValidateSeats(movieScreening, seatForCreatingDTOs);
        }

        private async Task ValidateSeats(MovieScreening movieScreening, ICollection<SeatCreateRequest> seatCreateRequests)
        {
            foreach (SeatCreateRequest seatRequest in seatCreateRequests)
            {
                if(SeatColumnIsNotValid(seatRequest,movieScreening))
                {
                    throw new BadSeatRowNumberException(seatRequest.Column, movieScreening.NumOfColumns);
                }
                else if(SeatRowIsNotValid(seatRequest,movieScreening))
                {
                    throw new BadSeatColumnNumberException(seatRequest.Row, movieScreening.NumOfRows);
                }
                else if (await SeatAlreadyExists(seatRequest))
                {
                    throw new SeatIsTakenException();
                }
            }
        }

        private async Task<bool> SeatAlreadyExists(SeatCreateRequest seatDto)
        {
            Expression<Func<Seat, bool>> seatExpr = seat => seat.Row == seatDto.Row && seat.Column == seatDto.Column && seat.MovieScreeningId == seatDto.MovieScreeningId;
            return (await _unitOfWork.SeatRepository.GetAllAsync(filter: seatExpr)).Count() > 0;
        }

        private bool SeatRowIsNotValid(SeatCreateRequest seatCreateRequest, MovieScreening movieScreening)
        {
            return seatCreateRequest.Row > movieScreening.NumOfRows || seatCreateRequest.Row <= 0;
        }

        private bool SeatColumnIsNotValid(SeatCreateRequest seatCreateRequest, MovieScreening movieScreening)
        {
            return seatCreateRequest.Column > movieScreening.NumOfColumns || seatCreateRequest.Column <= 0;
        }

        private bool IsAuthenticated(Guid buyerId)
        {
            if (buyerId == Guid.Empty)
                return false;
            return true;
        }

        private float CalculateReservationPrice(bool isAuthenticated, MovieScreening movieScreening, int numOfSeats)
        {
            if (isAuthenticated)
            {
                return numOfSeats * movieScreening.TicketPrice - (numOfSeats * movieScreening.TicketPrice) / 20;
            }
            return numOfSeats * movieScreening.TicketPrice;
        }

        private void InsertSeats(ICollection<SeatCreateRequest> seatForCreatingDTOs, Reservation reservation)
        {
            foreach (var seatDTO in seatForCreatingDTOs)
            {
                Seat seat = new Seat();
                seat.Row = seatDTO.Row;
                seat.Column = seatDTO.Column;
                seat.MovieScreeningId = seatDTO.MovieScreeningId;
                seat.ReservationId = reservation.Id;
                _unitOfWork.SeatRepository.Insert(seat);
                reservation.Seats.Add(seat);
            }
        }

        private async Task<MovieScreening> FindMovieScreening(ICollection<SeatCreateRequest> seatForCreatingDTOs)
        {
            var seatDTO = seatForCreatingDTOs.First();
            MovieScreening movieScreening = await _unitOfWork.MovieScreeningRepository.GetbyIdAsync(seatDTO.MovieScreeningId);
            if (movieScreening == null)
                throw new MovieScreeningNotFoundException(seatDTO.MovieScreeningId);
            if (DateTime.Now > movieScreening.StartTime)
                throw new MovieScreeningStartedException();
            return movieScreening;
        }

        private bool NoSeatsToReserve(ICollection<SeatCreateRequest> seatForCreatingDTOs)
        {
            if (seatForCreatingDTOs == null)
                return true;
            if (seatForCreatingDTOs.Count() == 0)
                return true;
            return false;
        }

        public async Task DeleteAsync(Guid reservationId, CancellationToken cancellationToken = default)
        {
            Reservation reservation = await _unitOfWork.ReservationRepository.GetbyIdAsync(reservationId);
            RemoveAllSeats(reservation);
            _unitOfWork.ReservationRepository.Remove(reservation);
            await _unitOfWork.SaveChangesAsync();
        }
        private void RemoveAllSeats(Reservation reservation)
        {
            foreach(Seat seat in reservation.Seats)
            {
                _unitOfWork.SeatRepository.Remove(seat);
            }
        }

        public async Task<ICollection<ReservationResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            ICollection<ReservationResponse> reservationDTOs = new List<ReservationResponse>();
            var reservations = await _unitOfWork.ReservationRepository.GetAllAsync();
            foreach(var reservation in reservations)
            {
                reservationDTOs.Add(_mapper.Map<ReservationResponse>(reservation));
            }
            return reservationDTOs;
        }

        public async Task<ReservationResponse> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<ReservationResponse>(await _unitOfWork.ReservationRepository.GetbyIdAsync(guid));
        }

        public Task UpdateAsync(Guid guid, ReservationUpdateRequest tDTO, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ReservationResponse>> GetMyReservationsAsync(Guid userId,CancellationToken cancellationToken = default)
        {
            ICollection<ReservationResponse> reservationDTOs = new List<ReservationResponse>();
            Expression<Func<Reservation, bool>> myReservationsExpr = reservation => reservation.UserId == userId;;

            var reservations = await _unitOfWork.ReservationRepository.GetAllAsync(filter:myReservationsExpr,includeProperties:"Seats");
            foreach (var reservation in reservations)
            {
                reservationDTOs.Add(_mapper.Map<ReservationResponse>(reservation));
            }
            return reservationDTOs;
        }

        public async Task RateReservation(Guid userId, Guid reservationId, int score)
        {
            Reservation reservation = await _unitOfWork.ReservationRepository.GetbyIdAsync(reservationId);
            if (reservation is null)
                throw new ReservationNotFoundException(reservationId);
            ValidateReservation(userId, score, reservation);
            reservation.Score = score;
            _unitOfWork.ReservationRepository.Update(reservation);
            Seat seat = reservation.Seats.First();
            MovieScreening movieScreening = await _unitOfWork.MovieScreeningRepository.GetbyIdAsync(seat.MovieScreeningId);
            await _movieScreeningService.UpdateScore(movieScreening.MovieId);
            await _unitOfWork.SaveChangesAsync();
            
        }

        private void ValidateReservation(Guid userId, int score, Reservation reservation)
        {
            if (ReservationDoesNotBelongToUser(userId, reservation))
                throw new ReservationDoesNotBelongToUserException();
            if (score < 1 || score > 5)
                throw new ReservationScoreException();
            if (reservation.Score >= 1 || reservation.Score <= 5)
                throw new ReservationAlreadyScoredException();
        }

        private bool ReservationDoesNotBelongToUser(Guid userId, Reservation reservation)
        {
            return reservation.UserId != userId;
        }
    }
}
