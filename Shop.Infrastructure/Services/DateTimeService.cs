using Shop.Application.Common.Interfaces;

namespace Shop.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTimeService Now => DateTime.UtcNow;
}
