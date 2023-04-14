using CodeChallenge.Data.Enums;

namespace CodeChallenge.Data.DTOs
{
    public class BranchDto : ContactInformationDto
    {
        public ClientType ClientType { get; set; }
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public DocType DocType { get; set; }
        public string DocNumber { get; set; }
        public ICollection<BranchDto>? Branches { get; set; }
    }
}
