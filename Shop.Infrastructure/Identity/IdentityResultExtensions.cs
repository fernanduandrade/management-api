using Microsoft.AspNetCore.Identity;
using Shop.Application.Common.Models;

namespace Shop.Infrastructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : Result.Failure(result.Errors.Select((er => er.Description)));
    }
}