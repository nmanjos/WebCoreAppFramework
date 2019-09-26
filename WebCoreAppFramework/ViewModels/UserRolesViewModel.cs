using System.ComponentModel.DataAnnotations;

namespace WebCoreAppFramework.ViewModels
{
    public class UserRolesViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
