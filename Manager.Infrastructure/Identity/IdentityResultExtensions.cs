using Microsoft.AspNetCore.Identity;
using Manager.Application.Common.Models;

namespace Manager.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select((er => er.Description)));
    }
}