using Microsoft.Extensions.Localization;
using QLDT.Application.Common.Services;

namespace QLDT.Application.Localization;

public sealed record JsonStringLocalizerFactory(IMemoryCacheService Cache) : IStringLocalizerFactory
{
    public IStringLocalizer Create(Type resourceSource) => new JsonStringLocalizer(Cache);

    public IStringLocalizer Create(string baseName, string location) => new JsonStringLocalizer(Cache);
}

