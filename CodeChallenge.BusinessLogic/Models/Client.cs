using CodeChallenge.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Data.Models
{
    public class Client : ContactInformation
    {
        [Key]
        public Guid ClientId { get; set; }
        public ClientType ClientType { get; set; }
        public string? FullName { get; set; }
        public string? CompanyName { get; set; }
        public DocType DocType{ get; set; }
        public string DocNumber { get; set; }
        
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
