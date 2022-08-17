using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using CinemaAppContracts.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CinemaAppServices.Exceptions.MovieScreening;
using CinemaAppServices.Exceptions.Movie;
using CinemaAppServices.Filters;
using CinemaAppServices.Request.MovieScreeningRequests;

namespace CinemaAppContracts.Services
{
    internal sealed class MovieScreeningService : IMovieScreeningService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MovieScreeningService> _logger;
        private readonly IMapper _mapper;

        public MovieScreeningService(IUnitOfWork unitOfWork, ILogger<MovieScreeningService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<MovieScreeningResponse> CreateAsync(MovieScreeningCreateRequest movieScreeningCreateRequest, CancellationToken cancellationToken = default)
        {
            MovieScreening movieScreening = _mapper.Map<MovieScreening>(movieScreeningCreateRequest);
            await Validate(movieScreeningCreateRequest);
            _unitOfWork.MovieScreeningRepository.Insert(movieScreening);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<MovieScreeningResponse>(movieScreening);
        }

        private async Task Validate(MovieScreeningCreateRequest movieScreeningCreateRequest)
        {
            if (movieScreeningCreateRequest.StartTime < DateTime.Now)
                throw new MovieScreeningBadTimeSchedulingException();
            if (movieScreeningCreateRequest.NumOfRows < 1 || movieScreeningCreateRequest.NumOfColumns < 1)
                throw new MovieScreeningBadNumberOfRowsOrColumnsException();
            if (await _unitOfWork.MovieRepository.GetbyIdAsync(movieScreeningCreateRequest.MovieId) is null)
                throw new MovieNotFoundException(movieScreeningCreateRequest.MovieId);
            if (movieScreeningCreateRequest.TicketPrice < 0)
                throw new TicketPriceLessThanZeroException();
        }
        private async Task Validate(MovieScreeningUpdateRequest movieScreeningCreateRequest)
        {
            if (movieScreeningCreateRequest.StartTime < DateTime.Now)
                throw new MovieScreeningBadTimeSchedulingException();
            if (movieScreeningCreateRequest.NumOfRows < 1 || movieScreeningCreateRequest.NumOfColumns < 1)
                throw new MovieScreeningBadNumberOfRowsOrColumnsException();
            if (movieScreeningCreateRequest.TicketPrice < 0)
                throw new TicketPriceLessThanZeroException();
        }

        public async Task DeleteAsync(Guid movieScreeningId, CancellationToken cancellationToken = default)
        {
            var movieScreening = await _unitOfWork.MovieScreeningRepository.GetbyIdAsync(movieScreeningId);
            if (movieScreening.Seats.Count > 0)
                throw new MovieScreeningContainsReservationException(movieScreeningId);
            _unitOfWork.MovieScreeningRepository.Remove(movieScreening);

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<(ICollection<MovieScreeningForAdminResponse>, int)> GetAllAsyncForAdmin(ServiceMovieScreeningFilter movieScreeningFilter, CancellationToken cancellationToken = default)
        {
            var movieScreeningsWithCounter = await _unitOfWork.MovieScreeningRepository.GetAllFilteredAsync(movieScreeningFilter.FirstLetters, movieScreeningFilter.Name,
                movieScreeningFilter.PageNumber, movieScreeningFilter.PageSize, cancellationToken);
            var movieScreeningsForAdmin = _mapper.Map<ICollection<MovieScreeningForAdminResponse>>(movieScreeningsWithCounter.Item1);
            return (movieScreeningsForAdmin, movieScreeningsWithCounter.Item2);
        }

        public async Task<MovieScreeningResponse> GetByIdAsync(Guid movieScreeningId, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<MovieScreeningResponse>(await _unitOfWork.MovieScreeningRepository.GetbyIdAsync(movieScreeningId, cancellationToken));
        }

        public async Task UpdateAsync(Guid guid, MovieScreeningUpdateRequest movieScreeningUpdateRequest, CancellationToken cancellationToken = default)
        {
            MovieScreening movieScreening = await _unitOfWork.MovieScreeningRepository.GetbyIdAsync(guid, cancellationToken);
            await Validate(movieScreeningUpdateRequest);
            movieScreening.NumOfColumns = movieScreeningUpdateRequest.NumOfColumns;
            movieScreening.NumOfRows = movieScreeningUpdateRequest.NumOfRows;
            movieScreening.TicketPrice = movieScreeningUpdateRequest.TicketPrice;
            movieScreening.StartTime = movieScreeningUpdateRequest.StartTime;
            _unitOfWork.MovieScreeningRepository.Update(movieScreening);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<(ICollection<MovieScreeningResponse>, int)> GetAllAsync(ServiceMovieScreeningFilter filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
