using CodeChallenge.Data.Models;

namespace CodeChallenge.Data.DTOs
{
    public class BranchDto : ContactInformationDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SellerCode { get; set; }
        public int Credit { get; set; }
    }
}
