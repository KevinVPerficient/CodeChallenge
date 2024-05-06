using CodeChallenge.Business.DTOs;

namespace CodeChallenge.Data.DTOs
{
    public class BranchDto : ContactInformationDto
    {
        public string DocNumber { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SellerCode { get; set; }
        public int Credit { get; set; }
    }
}
