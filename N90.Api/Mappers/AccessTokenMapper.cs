using AutoMapper;
using N90.Api.Models.Dtos;
using N90.Domain.Entities;

namespace N90.Api.Mappers;

public class AccessTokenMapper : Profile
{
    public AccessTokenMapper()
    {
        CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
    }
}