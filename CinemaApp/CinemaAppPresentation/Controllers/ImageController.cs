using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaAppPresentation.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [AllowAnonymous]
        [HttpGet("{imageId:guid}")]
        public async Task<IActionResult> GetById(Guid imageId, CancellationToken cancellationToken)
        {
            var image = await _imageService.GetByIdAsync(imageId,cancellationToken);
            if(image is not null)
                return File(image.Data, image.FileType, image.Name);
            return NotFound();
        }
    }
}
