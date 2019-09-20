namespace WebCoreAppFramework.Models
{
    public class CountryLanguage
    {
        public long LanguageId { get; set; }
        public Language Language { get; set; }

        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}