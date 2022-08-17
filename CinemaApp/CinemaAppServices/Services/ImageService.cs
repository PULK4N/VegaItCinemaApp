using CinemaAppContracts.Request;
using CinemaAppServices.IRepositories;
using CinemaAppContracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CinemaAppContracts.Services
{
    internal class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ImageRequest> GetByIdAsync(Guid guid, CancellationToken cancellationToken)
        {
            var image = await _unitOfWork.ImageRepository.GetbyIdAsync(guid);
            return _mapper.Map<ImageRequest>(image);
        }
    }
}
