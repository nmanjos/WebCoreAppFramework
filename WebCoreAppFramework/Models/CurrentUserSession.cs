using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Services;

namespace WebCoreAppFramework.Models
{
    public class CurrentUserSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public ApplicationTenant Tenant { get; set; }
        public GeoLocation GeoLocation { get; set; }

    }
}
