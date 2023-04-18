using AutoMapper;
using CodeChallenge.Business.DTOs;
using CodeChallenge.Business.Services.Interfaces;
using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeChallenge.Business.Services
{
    public class AuthtorizationService : IAuthorizationService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccessRepository _accessService;
        private readonly IMapper _mapper;

        public AuthtorizationService(IConfiguration configuration, IAccessRepository accessService, IMapper mapper)
        {
            _configuration = configuration;
            _accessService = accessService;
            _mapper = mapper;
        }

        public string GenerateToken(AuthDto auth)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, auth.AuthId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public bool ValidateAccess(AuthDto auth)
        {
            var authModel = _mapper.Map<Auth>(auth);
            return _accessService.ValidateAccess(authModel);
        }
    }
}
