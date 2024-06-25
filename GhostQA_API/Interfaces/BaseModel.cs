using System.ComponentModel.DataAnnotations.Schema;

namespace GhostQA_API.Interfaces
{
    public class BaseModel : IBaseModel
    {
        [Column("ApplicationId", TypeName = "INT")]
        public int? ApplicationId { get; set; }
        [Column("TenantId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? TenantId { get; set; }
        [Column("OrganizationId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? OrganizationId { get; set; }
    }
}
