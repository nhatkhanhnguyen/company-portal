using AutoMapper;

using CompanyPortal.Core.Common;
using CompanyPortal.Core.Enums;
using CompanyPortal.Core.Interfaces;
using CompanyPortal.Data.Common;
using CompanyPortal.Data.Database.Entities;
using CompanyPortal.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.CQRS.Settings.Queries;

public record GetHomepageSettingsQuery(bool ForceRefresh = false) : ICachedQuery<Result>
{
    public class Handler(IRepository<Setting> repository, IMapper mapper, ILogger<Handler> logger) : IRequestHandler<GetHomepageSettingsQuery, Result>
    {
        public async Task<Result> Handle(GetHomepageSettingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var settings = await repository.GetAll().Select(x => mapper.Map<SettingViewModel>(x)).ToListAsync(cancellationToken);

                var result = new HomePageSettingsViewModel
                {
                    WhyUsSettings = settings.Where(x => x.Type == SettingType.CarouselImage).ToList(),
                    StarredProductSettings = settings.Where(x => x.Type == SettingType.CarouselImage).ToList(),
                    CarouselSettings = settings.Where(x => x.Type == SettingType.CarouselImage).ToList()
                };
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result.Error("Có lỗi xảy ra khi đang đọc dữ liệu từ CSDL.");
            }
        }
    }

    public string Key => "site-settings";

    public TimeSpan? Expiration => null;
}
