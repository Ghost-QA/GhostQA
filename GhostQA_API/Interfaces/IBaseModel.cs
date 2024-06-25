namespace GhostQA_API.Interfaces
{
    public interface IBaseModel
    {
        int? ApplicationId
        {
            get; set;
        }
        Guid? TenantId
        {
            get; set;
        }
        Guid? OrganizationId
        {
            get; set;
        }
    }
}
