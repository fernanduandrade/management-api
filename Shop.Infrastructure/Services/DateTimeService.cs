using Shop.Application.Common.Interfaces;

namespace Shop.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
