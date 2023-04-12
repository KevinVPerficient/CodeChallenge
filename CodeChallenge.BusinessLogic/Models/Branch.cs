using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Data.Models
{
    public class Branch : ContactInformation
    {
        [Key]
        public Guid BranchId { get; set; } 
        public string Code { get; set; }
        public string Name { get; set; }
        public string SellerCode { get; set; }
        public int Credit { get; set; }
        public Guid ClientGuid { get; set; }

        [ForeignKey(nameof(ClientGuid))]
        public virtual Client Client { get; set; }

    }
}
