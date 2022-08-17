using AutoMapper;
using CinemaAppContracts.Request;
using CinemaAppContracts.Request.GenreRequests;
using CinemaAppContracts.Request.MovieRequests;
using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppContracts.Request.ReservationRequests;
using CinemaAppContracts.Request.SeatRequests;
using CinemaAppContracts.Request.UserRequests;
using CinemaAppServices.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using CinemaAppServices.Request.MovieScreeningRequests;
using CinemaAppEntities.Requests;

namespace CinemaAppContracts.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            MapRoles();
            MapGenres();
            MapMovies();
            MapMovieScreenings();
            MapReservations();
            MapSeats();
            MapUsers();
            CreateMap<Image, ImageRequest>();
        }

        private void MapUsers()
        {
            CreateMap<UserRegisterRequest, User>();
            CreateMap<UserUpdateRequest, User>();
            CreateMap<User, UserResponse> ();
        }

        private void MapSeats()
        {
            CreateMap<SeatCreateRequest, Seat>();
            CreateMap<Seat, SeatResponse>();
        }

        private void MapReservations()
        {
            CreateMap<ReservationCreateRequest, Reservation>();
            CreateMap<ReservationUpdateRequest, Reservation>();
            CreateMap <Reservation, ReservationResponse > ();
        }

        private void MapRoles()
        {
            CreateMap<RoleRequest, IdentityRole>();
        }

        private void MapGenres()
        {
            CreateMap<GenreCreateRequest, Genre>();
            CreateMap<GenreUpdateRequest, Genre>();
            CreateMap<Genre, GenreResponse > ();
        }

        private void MapMovies()
        {
            CreateMap<MovieCreateRequest, Movie>();
            CreateMap<MovieUpdateRequest, Movie>();
            CreateMap<Movie, MovieResponse> ();
        }

        private void MapMovieScreenings()
        {
            CreateMap<MovieScreeningCreateRequest, MovieScreening>();
            CreateMap<MovieScreeningUpdateRequest, MovieScreening>();
            CreateMap<MovieScreening, MovieScreeningResponse> ();
            CreateMap<MovieScreeningForAdminResponseModel,MovieScreeningForAdminResponse>();
        }
    }
}
