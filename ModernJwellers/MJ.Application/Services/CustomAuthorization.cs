namespace MJ.Application.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

public class CustomAuthorization : Attribute, IAuthorizationFilter
{
    
    public void OnAuthorization(AuthorizationFilterContext authorizationFilterContext)
    {
        var expiryClaim = authorizationFilterContext.HttpContext.User.FindFirst("exp")?.Value;
        if (string.IsNullOrEmpty(expiryClaim))
        {
            authorizationFilterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Auth" },
                    { "action", "Index" }
                });
            return;
        }

        if (long.TryParse(expiryClaim, out var expiryTime))
        {
            var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expiryTime).UtcDateTime;
            if (expiryDate < DateTime.UtcNow)
            {
                authorizationFilterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Auth" },
                    { "action", "Index" }
                });
                return;
            }
        }

        // var requestedUrl = authorizationFilterContext.HttpContext.Request.Path.Value;
        // Console.WriteLine(IsAuthorized(authorizationFilterContext.HttpContext.User, requestedUrl));
        // if (requestedUrl == null || !IsAuthorized(authorizationFilterContext.HttpContext.User, requestedUrl))
        // {
        //     SetUnauthorizedResponse(authorizationFilterContext, "Access denied: You do not have permission to access this resource.");
        // }
    }

    // private bool IsAuthorized(ClaimsPrincipal user, string requestedUrl)
    // {
    //     if (!user.Identity?.IsAuthenticated ?? true)
    //     {
    //         return false;
    //     }

    //     var role = user.FindFirst(ClaimTypes.Role)?.Value;

    //     if (string.IsNullOrEmpty(role))
    //     {
    //         return false;
    //     }

    //     var controller = requestedUrl.Split('/')[1];


    //     var rolePermissions = new Dictionary<string, List<string>>
    //     {
    //         { "Admin", new List<string> { "Admin","Home", "User","Book" } },
    //         { "User", new List<string> { "User","Home","Book" } },
    //     };

    //     Console.WriteLine($"Role: {role}, Controller: {controller}");
    //     return rolePermissions.ContainsKey(role) && rolePermissions[role].Contains(controller);
    // }
    // private void SetUnauthorizedResponse(AuthorizationFilterContext context, string message)
    // {

    //     context.HttpContext.Response.StatusCode = 403;
    //     context.Result = new RedirectToRouteResult(new RouteValueDictionary
    // {
    //     { "controller", "Error" },
    //     { "action", "Unauthorized" },
    //     { "message", message } 
    // });
    // }
}

