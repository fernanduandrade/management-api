using Manager.Application.Common.Interfaces;

namespace Manager.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
