using CinemaAppServices.Entities;
using CinemaAppServices.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaAppPersistence.Repositories
{
    internal class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(DbContext context) : base(context)
        {
        }
    }
}
