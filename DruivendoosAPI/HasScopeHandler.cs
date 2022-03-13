using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DruivendoosAPI
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var claims = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer);
            var scopes = new List<string>();
            foreach(var claim in claims)
            {
                scopes.Add(claim.Value.ToString());
            }
        //    var scopes = claim.Value.Split(" ");

            // Succeed if the scope array contains the required scope
              if (scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
