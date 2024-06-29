using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Manager.Application.Common.Behaviors;

public class LoggingBehavior<TRequest>(ILogger<TRequest> logger) : IRequestPreProcessor<TRequest> where TRequest : notnull
{

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        logger.LogInformation("CleanArchitecture Request: {Name} {@Request}",
            requestName, request);
        
        return Task.CompletedTask;
    }
}
