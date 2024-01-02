using AutoMapper;
using N90.Application.Common.Identity.Models;
using N90.Domain.Entities;

namespace N90.Infrastructure.Common.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<SignUpDetails, User>();
    }
}