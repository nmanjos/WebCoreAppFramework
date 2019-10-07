using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreAppFramework.Models
{
    public class GeoLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        public bool isCity { get; set; } = false;
        public ICollection<Address> Addresses { get; set; }
        public Country Country { get; set; }
        public bool Visible { get; set; } = true; // to maitain integrity Delete actions do not Delete, they set this field to false 
        public bool System { get; set; } = false; // this field Locks update and deletes   Sistem Records can only be changed in the database.
    }
}