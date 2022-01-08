using AutoMapper;
using GameTracker.Shared.Models;

namespace GameTracker.Server.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameDto, Game>();
            CreateMap<Game, GameDto>();
            CreateMap<GamesDto, Games>();
            CreateMap<Games, GamesDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
