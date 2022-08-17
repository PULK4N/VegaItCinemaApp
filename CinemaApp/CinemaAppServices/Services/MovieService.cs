using CinemaAppContracts.Request.MovieRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using CinemaAppContracts.Interfaces;
using CinemaAppServices.Exceptions.Movie;
using AutoMapper;
using CinemaAppServices.Filters;

namespace CinemaAppContracts.Services
{
    internal sealed class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MovieResponse> CreateAsync(MovieCreateRequest movieDTO, CancellationToken cancellationToken = default)
        {
            Movie movie = await CreateMovieFromDTO(movieDTO);
            _unitOfWork.MovieRepository.Insert(movie);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<MovieResponse>(movie);
        }

        private async Task<Movie> CreateMovieFromDTO(MovieCreateRequest movieDTO)
        {
            Movie movie = new Movie();
            movie.DurationMinutes = movieDTO.DurationMinutes;
            movie.OriginalName = movieDTO.OriginalName;
            movie.Name = movieDTO.Name;
            await AddMovieGenres(movie,movieDTO.GenreIds);
            movie.PosterImageId = await AddPosterImage(movieDTO);
            return movie;
        }

        private async Task<Guid> AddPosterImage(MovieCreateRequest movieDTO)
        {
            var fileName = Path.GetFileNameWithoutExtension(movieDTO.PosterImage.FileName);
            var extension = Path.GetExtension(movieDTO.PosterImage.FileName);
            var image = new Image
            {
                CreatedOn = DateTime.UtcNow,
                FileType = movieDTO.PosterImage.ContentType,
                Extension = extension,
                Name = fileName
            };

            using (var stream = new MemoryStream())
            {
                await movieDTO.PosterImage.CopyToAsync(stream);
                image.Data = stream.ToArray();
                _unitOfWork.ImageRepository.Insert(image);
            }

            return image.Id;
        }

        private async Task AddMovieGenres(Movie movie, ICollection<Guid> genreIds)
        {
            if (movie.Genres is null)
                movie.Genres = new List<Genre>();
            foreach(Guid genreId in genreIds)
            {
                movie.Genres.Add(await _unitOfWork.GenreRepository.GetbyIdAsync(genreId));
            }
        }

        public async Task DeleteAsync(Guid movieId, CancellationToken cancellationToken = default)
        {
            var movie = await _unitOfWork.MovieRepository.GetbyIdAsync(movieId);
            _unitOfWork.MovieRepository.Remove(movie);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<(ICollection<MovieResponse>, int)> GetAllAsync(ServiceMovieCustomerFilter movieCustomerFilter, CancellationToken cancellationToken = default)
        {
            ICollection<Genre> genres = await _unitOfWork.GenreRepository.GetAllByIds(movieCustomerFilter.GenreIds);
            if (MoviesAfter7Days(movieCustomerFilter.MovieDay))
                throw new MoviesForbidenSearchPeriodException();
            (ICollection<Movie>, int) moviesWithCounter = await _unitOfWork.MovieRepository.GetAllForCustomer(movieCustomerFilter.MovieDay, 
                genres, movieCustomerFilter.SortAlphabetically, movieCustomerFilter.SortMovieScreeningsChronologically,
                movieCustomerFilter.PageNumber,movieCustomerFilter.PageSize
                );
            return (_mapper.Map<ICollection<MovieResponse>>(moviesWithCounter.Item1), moviesWithCounter.Item2);
        }
        public async Task<(ICollection<MovieResponse>,int)> GetAllAsync(ServiceMovieFilter filter, CancellationToken cancellationToken = default)
        {
            (ICollection<Movie>,int) moviesWithCounter = await _unitOfWork.MovieRepository.GetAllFilteredAsync(filter.FirstLetters, filter.Name, filter.PageNumber, 
                filter.PageSize, cancellationToken);
            return (_mapper.Map<ICollection<MovieResponse>>(moviesWithCounter.Item1), moviesWithCounter.Item2);
        }


        public async Task<MovieResponse> GetByIdAsync(Guid guid, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<MovieResponse>(await _unitOfWork.MovieRepository.GetbyIdAsync(guid));
        }

        public async Task UpdateAsync(Guid movieId, MovieUpdateRequest movieForUpdatingRequest, CancellationToken cancellationToken = default)
        {
            var movie = await _unitOfWork.MovieRepository.GetbyIdAsync(movieId);
            if (movie is null)
                throw new MovieNotFoundException(movieId);

            await UpdateMovieFromDTO(movie, movieForUpdatingRequest);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdateMovieFromDTO(Movie movie, MovieUpdateRequest movieUpdateRequest)
        {
            if(movieUpdateRequest.Name != string.Empty)
                movie.Name = movieUpdateRequest.Name;
            if(movieUpdateRequest.OriginalName != string.Empty)
                movie.OriginalName = movieUpdateRequest.OriginalName;
            if(movieUpdateRequest.DurationMinutes > 0)
                movie.DurationMinutes = movieUpdateRequest.DurationMinutes;
            if (movieUpdateRequest.PosterImage is not null)
            {
                var image = await _unitOfWork.ImageRepository.GetbyIdAsync(movie.PosterImageId);
                if(image is not null) _unitOfWork.ImageRepository.Remove(image);
                movie.PosterImageId = await AddPosterImage(movieUpdateRequest);
            }
            if(movieUpdateRequest.GenreIdsToAdd.Count() != 0)
            {
                await AddMovieGenres(movie,movieUpdateRequest.GenreIdsToAdd);
            }
            if (movieUpdateRequest.GenreIdsToRemove.Count() != 0)
            {
                await RemoveMovieGenres(movie, movieUpdateRequest.GenreIdsToRemove);
            }
            _unitOfWork.MovieRepository.Update(movie);
            //TODO: videti zasto ovde krashuje
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task RemoveMovieGenres(Movie movie, ICollection<Guid> genreIds)
        {
            if (movie.Genres is null)
                throw new ArgumentNullException(nameof(movie));
            foreach (Guid genreId in genreIds)
            {
                var removed = movie.Genres.Remove(await _unitOfWork.GenreRepository.GetbyIdAsync(genreId));
                if (removed == false)
                    throw new MovieDoesNotContainGenreException(movie.Id, genreId);
            }
        }
        private bool MoviesAfter7Days(DateTime dateTime)
        {

            if(dateTime.Date > DateTime.Now.AddDays(7).Date)
                return true;
            return false;
        }

        public async Task UpdateScore(Guid movieId)
        {
            Movie movie = await _unitOfWork.MovieRepository.GetbyIdAsync(movieId);
            if (movie is null)
                throw new MovieNotFoundException(movieId);
            movie.Rating = await _unitOfWork.MovieRepository.GetMovieScore(movieId);
            _unitOfWork.MovieRepository.Update(movie);
        }
    }
}
