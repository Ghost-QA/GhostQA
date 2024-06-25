using GhostQA_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GhostQA_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(200)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string OrganizationName { get; set; }

        public int ApplicationId { get; set; }
        public Guid? TenantId { get; set; }
        public Guid? OrganizationId { get; set; }

        public bool? IsDisabled { get; set; }
    }
}