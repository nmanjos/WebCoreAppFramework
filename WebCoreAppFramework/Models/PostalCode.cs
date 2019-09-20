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
        public string StreetName { get; set; }
        [Required]
        public string Prefix { get; set; }
        [Required]
        public string Sufix { get; set; }
        public County County { get; set; }


        public override string ToString()
        {
            return $"{this.Prefix}-{this.Sufix} {this.StreetName}";

        }
        
        public void FromString(string CPString)
        {
            string[] cp = CPString.Split('-',' ');
        }
    }
}