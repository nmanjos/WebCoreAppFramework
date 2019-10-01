using System.ComponentModel.DataAnnotations;

namespace WebCoreAppFramework.ViewModels
{
    public class TenantViewModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LogoURL { get; set; }
        public string EmailAddress { get; set; }
        public string WebSite { get; set; }
        public string PhoneContact { get; set; }
        public bool Visible { get; set; } = true; // to maitain integrity Delete actions do not Delete, they set this field to false 
        public bool System { get; set; } = false; // this field Locks update and deletes   Sistem Records can only be changed in the database.
    }
}