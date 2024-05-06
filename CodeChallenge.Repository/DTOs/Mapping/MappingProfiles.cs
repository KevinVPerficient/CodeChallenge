using AutoMapper;
using CodeChallenge.Data.DTOs;
using CodeChallenge.Data.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace CodeChallenge.Business.DTOs.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Auth, AuthDto>().ReverseMap();
            CreateMap<Branch, BranchDto>().ForMember(dest => dest.DocNumber, opt => opt.MapFrom(src => src.Client.DocNumber));
            CreateMap<BranchDto, Branch>();
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
