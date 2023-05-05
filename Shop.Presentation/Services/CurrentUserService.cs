using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Shop.Application.Common.Interfaces;

namespace Shop.Presentation.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor context) 
    {
        _httpContextAccessor = context;
    }

    public string? UserId
        => GetUserId();

    public string GetUserId()
    {
        var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var token = identity.Claims.Where(x => x.Type == "access_token").ToList();
        if (token.Count > 0)
        {
            var jwt = new JwtSecurityToken(token[0].Value);
            var claims = jwt.Claims;
        }
        return "";
    }
}