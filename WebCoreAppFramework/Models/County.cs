using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreAppFramework.Models
{
    public class County
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public District District { get; set; }
        public ICollection<Address> Addresses { get; set; }

        public bool Visible { get; set; } = true; // to maitain integrity Delete actions do not Delete, they set this field to false 
        public bool System { get; set; } = false; // this field Locks update and deletes   Sistem Records can only be changed in the database.
    }
}