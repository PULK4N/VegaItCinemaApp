using AutoMapper;
using CinemaAppContracts.Request;
using CinemaAppContracts.Request.GenreRequests;
using CinemaAppContracts.DTO.GenreDTOs;
using CinemaAppContracts.DTO.MovieDTOs;
using CinemaAppContracts.Request.MovieRequests;
using CinemaAppContracts.Request.MovieScreeningRequests;
using CinemaAppContracts.DTO.MovieScreeningDTOs;
using CinemaAppContracts.DTO.ReservationDTOs;
using CinemaAppContracts.Request.ReservationRequests;
using CinemaAppContracts.Request.SeatRequests;
using CinemaAppContracts.DTO.SeatDTOs;
using CinemaAppContracts.DTO.UserDTOs;
using CinemaAppContracts.Request.UserRequests;
using CinemaAppContracts.Wrappers;
using CinemaAppServices.Filters;
using CinemaAppServices.Request.MovieScreeningRequests;

namespace CinemaAppPresentation.Profiles
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

            CreateMap<ImageRequest, ImageDTO>();
            CreateMap<PaginationFilter, ServicePaginationFilter>();
        }

        private void MapUsers()
        {
            CreateMap<UserRegisterDTO, UserRegisterRequest>();
            CreateMap<UserLoginDTO, UserLoginRequest>();
            CreateMap<UserForUpdatingDTO, UserUpdateRequest>();
            CreateMap<UserResponse, UserForReturningDTO>();
            CreateMap<UserEmailDTO, UserEmailRequest>();
            CreateMap<UserForResetPasswordDTO, UserResetPasswordRequest>();

            CreateMap<UserFilter, ServiceUserFilter>();
        }

        private void MapSeats()
        {
            CreateMap<SeatForCreatingDTO, SeatCreateRequest>();
            CreateMap<SeatDTO, SeatResponse>();
        }

        private void MapReservations()
        {
            CreateMap<ReservationForCreatingDTO, ReservationCreateRequest>()
                .ForMember(mDTO => mDTO.Seats, opt => opt.MapFrom(mReq => mReq.Seats));
            CreateMap<ReservationForUpdatingDTO, ReservationUpdateRequest>()
                .ForMember(mDTO => mDTO.Seats, opt => opt.MapFrom(mReq => mReq.Seats));
            CreateMap<ReservationResponse, ReservationForReturningDTO>()
                .ForMember(mReq => mReq.Seats , opt => opt.MapFrom(mDTO => mDTO.Seats));
        }

        private void MapRoles()
        {
            CreateMap<RoleDTO, RoleRequest>();
        }

        private void MapGenres()
        {
            CreateMap<GenreForCreatingDTO, GenreCreateRequest>();
            CreateMap<GenreForUpdatingDTO, GenreUpdateRequest>();
            CreateMap<GenreResponse, GenreForReturningDTO>();
            CreateMap<GenreForReturningDTO, GenreResponse>();

            CreateMap<GenreFilter, ServiceGenreFilter>();
        }

        private void MapMovies()
        {
            CreateMap<MovieForCreatingDTO, MovieCreateRequest>().ForMember(mDTO => mDTO.GenreIds, opt => opt.MapFrom(mReq => mReq.GenreIds));
            CreateMap<MovieForUpdatingDTO, MovieUpdateRequest>()
                .ForMember(mDTO => mDTO.GenreIdsToAdd, opt => opt.MapFrom(mReq => mReq.GenreIdsToAdd))
                .ForMember(mDTO => mDTO.GenreIdsToRemove, opt => opt.MapFrom(mReq => mReq.GenreIdsToRemove));
            CreateMap<MovieResponse, MovieForReturningDTO>()
                .ForMember(mDTO => mDTO.Genres, opt => opt.MapFrom(mReq => mReq.Genres))
                .ForMember(mReq => mReq.MovieScreenings , opt => opt.MapFrom(mDTO => mDTO.MovieScreenings));

            CreateMap<MovieFilter, ServiceMovieFilter>();
            CreateMap<MovieCustomerFilter, ServiceMovieCustomerFilter>();
        }

        private void MapMovieScreenings()
        {
            CreateMap<MovieScreeningForCreatingDTO, MovieScreeningCreateRequest>();
            CreateMap<MovieScreeningForUpdatingDTO, MovieScreeningUpdateRequest>();
            CreateMap<MovieScreeningResponse, MovieScreeningForReturningDTO>();

            CreateMap<MovieScreeningFilter, ServiceMovieScreeningFilter>();
            CreateMap<MovieScreeningForAdminResponse,MovieScreeningForAdminDTO>();
        }
    }
}
