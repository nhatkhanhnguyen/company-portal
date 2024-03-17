using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;

namespace CompanyPortal.CQRS.Settings.Queries;

public record GetHomepageSettingsQuery(bool ForceRefresh = false) : ICachedQuery<Result<List<SettingViewModel>>>
{
    public class Handler(IRepository<Setting> repository, IMapper mapper, ILogger<Handler> logger)
    {

    }

    public string Key => "site-settings";

    public TimeSpan? Expiration => null;
}
