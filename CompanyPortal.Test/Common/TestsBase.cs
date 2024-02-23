using AutoMapper;
using CompanyPortal.Core.Common;
using CompanyPortal.Core.Providers;
using CompanyPortal.Data.Common;
using MediatR;
using Moq;

namespace CompanyPortal.Test.Common;

public abstract class TestsBase<TEntity> where TEntity : EntityBase
{
    protected readonly Mock<IRepository<TEntity>> _mockRepository = new();
    protected readonly Mock<IUnitOfWork> _mockUow = new();
    protected readonly Mock<IMediator> _mockMediator = new();
    protected readonly Mock<IUserPrincipalsProvider> _mockPrincipal = new();
    protected readonly IMapper _mapper;
    protected const string UserId = "581d37c6-8cfe-461f-974e-a1c4d1102cb5";

    protected TestsBase()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()))
            .CreateMapper();
        _mockPrincipal.Setup(x => x.GetCurrentUserId()).Returns(UserId);
    }
}