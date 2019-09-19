using System.ComponentModel.DataAnnotations;

namespace WebCoreAppFramework.ViewModels
{
    public class UserRolesView
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
