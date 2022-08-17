using AutoMapper;
using CinemaAppContracts.Request.MovieRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaAppContracts.DTO.MovieDTOs;
using CinemaAppServices.Filters;

namespace CinemaAppPresentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [Route("admin/movies")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MovieFilter filter)
        {
            var movieFilter = _mapper.Map<ServiceMovieFilter>(filter);
            var moviesWithCount = await _movieService.GetAllAsync(movieFilter);
            return Ok(new PagedResponse<ICollection<MovieForReturningDTO>>(_mapper.Map<ICollection<MovieForReturningDTO>>(moviesWithCount.Item1)
                , moviesWithCount.Item2 / movieFilter.PageSize + 1, movieFilter.PageSize, (int) Math.Ceiling((double) (moviesWithCount.Item2 / movieFilter.PageSize))+1));
        }

        [AllowAnonymous]
        [Route("movies")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MovieCustomerFilter filter)
        {
            var movieFilter = _mapper.Map<ServiceMovieCustomerFilter>(filter);
            var moviesWithCount = await _movieService.GetAllAsync(movieFilter);
            var movies = _mapper.Map<ICollection<MovieForReturningDTO>>(moviesWithCount.Item1);
            return Ok(new PagedResponse<ICollection<MovieForReturningDTO>>(movies,
                moviesWithCount.Item2 / movieFilter.PageSize + 1, 1 + movieFilter.PageSize, (int)Math.Ceiling((double)(moviesWithCount.Item2 / movieFilter.PageSize))));
        }

        [AllowAnonymous]
        [HttpGet("movies/{movieId:guid}")]
        public async Task<IActionResult> GetById(Guid movieId)
        {
            return Ok(await _movieService.GetByIdAsync(movieId));
        }


        [Authorize(Roles = "Admin")]
        [Route("admin/movies")]
        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public async Task<IActionResult> Create([FromForm] MovieForCreatingDTO movieForCreatingDTO)
        {
            var createdMovieDto = await _movieService.CreateAsync(_mapper.Map<MovieCreateRequest>(movieForCreatingDTO));
            return Ok(createdMovieDto);
        }

        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        [HttpPut("admin/movies/{movieId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid movieId, [FromForm] MovieForUpdatingDTO movieForUpdatingDTO, CancellationToken cancellationToken)
        {
            await _movieService.UpdateAsync(movieId, _mapper.Map<MovieUpdateRequest>(movieForUpdatingDTO), cancellationToken);
            return NoContent();
        }

        [HttpDelete("admin/movies/{movieId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid movieId, CancellationToken cancellationToken)
        {
            await _movieService.DeleteAsync(movieId, cancellationToken);
            return NoContent();
        }
    }
}
