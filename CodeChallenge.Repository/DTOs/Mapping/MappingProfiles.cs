using AutoMapper;
using CodeChallenge.Data.DTOs;
using CodeChallenge.Data.Models;

namespace CodeChallenge.Business.DTOs.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Auth, AuthDto>().ReverseMap();
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<Client, BranchDto>().ReverseMap();
        }
    }
}
