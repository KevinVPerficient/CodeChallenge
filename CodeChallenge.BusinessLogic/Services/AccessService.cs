using CodeChallenge.Data.Models;
using CodeChallenge.Data.Services.Interfaces;

namespace CodeChallenge.Data.Services
{
    public class AccessService : IAccessRepository
    {
        private readonly List<Auth> _auth = new()
        {
            new Auth
            {
                AuthId = "Admin",
                Password = "1234",
            }
        };
            
        public bool ValidateAccess(Auth auth)
        {
            return (_auth.FirstOrDefault(x => x.AuthId.Equals(auth.AuthId) && x.Password.Equals(auth.Password))) != null;
        }
    }
}
