using CodeChallenge.Data.Enums;
using CodeChallenge.Data.Models;

namespace CodeChallenge.Data.DTOs
{
    public class ClientDto : ContactInformationDto
    {
        public ClientType ClientType { get; set; }
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public DocType DocType { get; set; }
        public string DocNumber { get; set; }
        public ICollection<BranchDto>? Branches { get; set; }
    }
}
