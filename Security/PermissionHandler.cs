using Microsoft.AspNetCore.Authorization;

namespace BakeryOps.API.Security
{
    public class PermissionHandler(ILogger<PermissionHandler> logger): AuthorizationHandler<PermissionAttribute>
    {
       

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAttribute requirement)
        {

            var hasPermission = context.User.Claims.Any(c => c.Type == "Permission" && c.Value == requirement.Permission);
            if (hasPermission)
            {
                logger.LogInformation($"User {context.User.Identity.Name} has permission {requirement.Permission}");
                context.Succeed(requirement);
            }
            else
            {
                logger.LogWarning($"User {context.User.Identity.Name} does not have permission {requirement.Permission}");
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
