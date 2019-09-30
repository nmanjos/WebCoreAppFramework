using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebCoreAppFramework.Models
{
    public class PostalCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string Prefix { get; set; }
        [Required]
        public string Sufix { get; set; }
        public County County { get; set; }

        public bool Visible { get; set; } = true; // to maitain integrity Delete actions do not Delete, they set this field to false 
        public bool System { get; set; } = false; // this field Locks update and deletes   Sistem Records can only be changed in the database.
        public override string ToString()
        {
            return $"{this.Prefix}-{this.Sufix} {this.LocationName}";

        }

        public void FromString(string CPString)
        {
            string[] cp = new string[3];
            if (this.County.District.Country.Name == "Portugal") cp = CPString.Split('-', ' ');

            this.Prefix = string.IsNullOrEmpty(cp[0]) ? "" : cp[0];
            this.Sufix = string.IsNullOrEmpty(cp[1]) ? "" : cp[1];
            this.LocationName = string.IsNullOrEmpty(cp[2]) ? "" : cp[2];
        }
    }
}