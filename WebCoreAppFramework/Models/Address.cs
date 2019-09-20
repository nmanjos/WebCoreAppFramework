using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreAppFramework.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public PostalCode PostalCode { get; set; }
        [Required]
        public string PCLocation { get; set; }
        public GeoLocation GeoLocation { get; set; } // many address can have the same GeoLocation (Complying with Redundancy rules)

    }
}