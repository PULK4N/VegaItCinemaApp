using AutoMapper;
using CinemaAppContracts.Request.GenreRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CinemaAppContracts.DTO.GenreDTOs;
using Microsoft.AspNetCore.Authorization;
using CinemaAppServices.Filters;

namespace CinemaAppPresentation.Controllers
{
    [ApiController]
    [Route("api")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("genres")]
        public async Task<IActionResult> GetAll([FromQuery] GenreFilter filter, CancellationToken cancellationToken)
        {
            var genreFilter = _mapper.Map<ServiceGenreFilter>(filter);
            var genresWithCount = await _genreService.GetAllAsync(genreFilter);
            return Ok(new PagedResponse<ICollection<GenreForReturningDTO>>(_mapper.Map<ICollection<GenreForReturningDTO>>(genresWithCount.Item1),
               (genresWithCount.Item2 / filter.PageSize) + 1, filter.PageSize, (int)Math.Ceiling((double)(genresWithCount.Item2 / filter.PageSize)) + 1));
        }
        [HttpGet("genres/{genreId:guid}")]
        public async Task<IActionResult> GetById(Guid genreId,CancellationToken cancellationToken)
        {
            var genres = await _genreService.GetByIdAsync(genreId);
            return Ok(genres);
        }

        [HttpPost]
        [Route("admin/genres")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] GenreForCreatingDTO genreDTO)
        {
            var createdGenreDto = await _genreService.CreateAsync(_mapper.Map<GenreCreateRequest>(genreDTO));
            return CreatedAtAction(nameof(GetById), new { genreId = createdGenreDto.Id }, createdGenreDto);
        }

        [HttpPut("admin/genres/{genreId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid genreId, [FromBody] GenreForUpdatingDTO genreForUpdateDto, CancellationToken cancellationToken)
        {
            await _genreService.UpdateAsync(genreId, _mapper.Map<GenreUpdateRequest>(genreForUpdateDto), cancellationToken);
            return Ok();
        }

        [HttpDelete("admin/genres/{genreId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid genreId, CancellationToken cancellationToken)
        {
            await _genreService.DeleteAsync(genreId, cancellationToken);
            return Ok();
        }
    }
}
