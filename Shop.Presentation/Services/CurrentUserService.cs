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
    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}