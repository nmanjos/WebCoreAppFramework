namespace WebCoreAppFramework.Models
{
    public class CountryLanguage
    {
        public long LanguageId { get; set; }
        public Language Language { get; set; }

        public long CountryId { get; set; }
        public Country Country { get; set; }

        public bool Visible { get; set; } = true; // to maitain integrity Delete actions do not Delete, they set this field to false 
        public bool System { get; set; } = false; // this field Locks update and deletes   Sistem Records can only be changed in the database.
    }
}