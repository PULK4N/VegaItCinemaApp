using CinemaAppContracts.Request.GenreRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CinemaAppServices.Exceptions.Genre;
using CinemaAppServices.Filters;
using AutoMapper;

namespace CinemaAppContracts.Services
{
    internal sealed class GenreService : IGenreService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GenreResponse> CreateAsync([FromBody] GenreCreateRequest genreDTO, CancellationToken cancellationToken = default)
        {
            Genre newGenre = new Genre();
            newGenre.Name = genreDTO.Name;
            await CheckIfGenreExists(genreDTO.Name);

            _unitOfWork.GenreRepository.Insert(newGenre);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<GenreResponse>(newGenre);
        }

        private async Task CheckIfGenreExists(string name)
        {
            if ((await _unitOfWork.GenreRepository.GetByName(name)) is not null)
                throw new GenreAlreadyExistsException(name);
        }

        public async Task DeleteAsync(Guid genreId, CancellationToken cancellationToken = default)
        {
            Genre genre = await _unitOfWork.GenreRepository.GetbyIdAsync(genreId, cancellationToken);
            if(genre is null)
            {
                throw new GenreNotFoundException(genreId);
            }
            _unitOfWork.GenreRepository.Remove(genre);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<(ICollection<GenreResponse>, int)> GetAllAsync(ServiceGenreFilter filter, CancellationToken cancellationToken = default)
        {
            var genresWithCounter = await _unitOfWork.GenreRepository.GetAllFilteredAsync(filter.FirstLetters,filter.Name,filter.PageNumber,
                filter.PageSize,cancellationToken);
            ICollection<GenreResponse> genreResponses = _mapper.Map<ICollection<GenreResponse>>(genresWithCounter.Item1);
            return (genreResponses,genresWithCounter.Item2);
        }

        public async Task<GenreResponse> GetByIdAsync(Guid genreId, CancellationToken cancellationToken = default)
        {
            Genre genre = await _unitOfWork.GenreRepository.GetbyIdAsync(genreId);
            if (genre is null)
            {
                throw new GenreNotFoundException(genreId);
            }
            return _mapper.Map<GenreResponse>(genre);
        }

        public async Task UpdateAsync(Guid genreId, [FromBody] GenreUpdateRequest genreDTO, CancellationToken cancellationToken = default)
        {
            Genre genre = await _unitOfWork.GenreRepository.GetbyIdAsync(genreId);
            if (genre is null)
            {
                throw new GenreNotFoundException(genreId);
            }
            genre.Name = genreDTO.Name;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
