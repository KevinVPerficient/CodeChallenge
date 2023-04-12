using CodeChallenge.Data.Models;

namespace CodeChallenge.Data.Services.Interfaces
{
    public interface IAccessRepository
    {
        bool ValidateAccess(Auth auth);
    }
}
