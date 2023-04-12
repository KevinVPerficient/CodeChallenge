using CodeChallenge.Business.DTOs;

namespace CodeChallenge.Business.Services.Interfaces
{
    public interface IAuthorizationService
    {
        string GenerateToken(AuthDto auth);
        bool ValidateAccess(AuthDto auth);
    }
}
