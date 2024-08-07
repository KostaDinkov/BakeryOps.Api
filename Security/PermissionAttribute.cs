using Microsoft.AspNetCore.Authorization;

namespace BakeryOps.API.Security
{
    public class PermissionAttribute (string permission): AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
    {
        public string Permission { get; } = permission;
        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return this;
        }
    }
}
