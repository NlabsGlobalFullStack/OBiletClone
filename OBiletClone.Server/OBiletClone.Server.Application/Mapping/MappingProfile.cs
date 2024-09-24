using AutoMapper;
using OBiletClone.Server.Application.Features.Auth;
using OBiletClone.Server.Domain.Entities;

namespace OBiletClone.Server.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, LoginCommand>().ReverseMap();
    }
}
