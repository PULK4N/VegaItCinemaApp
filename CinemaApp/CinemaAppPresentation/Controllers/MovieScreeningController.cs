using AutoMapper;
using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaAppContracts.DTO.MovieScreeningDTOs;
using CinemaAppServices.Filters;

namespace CinemaAppPresentation.Controllers
{
    [Route("api")]
    public class MovieScreeningController : ControllerBase
    {
        private readonly IMovieScreeningService _movieScreeningService;
        private readonly IMapper _mapper;

        public MovieScreeningController(IMovieScreeningService movieScreeningService, IMapper mapper)
        {
            _movieScreeningService = movieScreeningService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("admin/movieScreenings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] MovieScreeningFilter filter, CancellationToken cancellationToken)
        {
            var movieScreeningFilter = _mapper.Map<ServiceMovieScreeningFilter>(filter);
            var moviesWithCount = await _movieScreeningService.GetAllAsyncForAdmin(movieScreeningFilter);
            return Ok(new PagedResponse<ICollection<MovieScreeningForAdminDTO>>(_mapper.Map<ICollection<MovieScreeningForAdminDTO>>(moviesWithCount.Item1)
                , moviesWithCount.Item2 / movieScreeningFilter.PageSize + 1, movieScreeningFilter.PageSize, (int)Math.Ceiling((double)(moviesWithCount.Item2 / filter.PageSize)) + 1));
        }

        [HttpGet("movieScreenings/{movieScreeningId:guid}")]
        public async Task<IActionResult> GetById(Guid movieScreeningId, CancellationToken cancellationToken)
        {
            var movieScreening = await _movieScreeningService.GetByIdAsync(movieScreeningId, cancellationToken);
            return Ok(movieScreening);
        }

        [HttpPost]
        [Route("admin/movieScreenings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] MovieScreeningForCreatingDTO movieScreeningForCreatingDTO)
        {
            var createdMovieScreeningDto = await _movieScreeningService.CreateAsync(_mapper.Map<MovieScreeningCreateRequest>(movieScreeningForCreatingDTO));
            return CreatedAtAction(nameof(GetById), new { movieScreeningId = createdMovieScreeningDto.Id }, createdMovieScreeningDto);
        }

        [HttpPut("admin/movieScreenings/{movieScreeningId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid movieScreeningId, [FromBody] MovieScreeningForUpdatingDTO movieScreeningForUpdatingDTO, CancellationToken cancellationToken)
        {
            await _movieScreeningService.UpdateAsync(movieScreeningId, _mapper.Map<MovieScreeningUpdateRequest>(movieScreeningForUpdatingDTO), cancellationToken);
            return NoContent();
        }

        [HttpDelete("admin/movieScreenings/{movieScreeningId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid movieScreeningId, CancellationToken cancellationToken)
        {
            await _movieScreeningService.DeleteAsync(movieScreeningId, cancellationToken);
            return NoContent();
        }
    }
}
