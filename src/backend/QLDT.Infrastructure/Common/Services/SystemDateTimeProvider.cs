using QLDT.Domain.Common;

namespace QLDT.Infrastructure.Common.Services;

internal sealed class SystemDateTimeProvider : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
