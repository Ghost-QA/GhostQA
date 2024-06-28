namespace GhostQA_API.TenantInfra.EntityModal;
public class Dto_Tenant : Dto_UserDetails
{
    public Guid TenantId { get; set; }
    public string TenantName { get; set; }
    public string TenantDescription { get; set; }
}
