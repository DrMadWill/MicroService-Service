using AutoMapper;
using DrMadWill.Layers.Repository.Abstractions.CQRS;
using DrMadWill.Layers.Services.Concretes;
using Microsoft.Extensions.Logging;

namespace Persistence.Services.Sys;

public class AppServiceManager : ServiceManager
{
    public AppServiceManager(IUnitOfWork unitOfWork, IQueryRepositories queryRepositories, IMapper mapper,ILoggerFactory loggerFactory) :
        base(unitOfWork, queryRepositories, mapper,loggerFactory, typeof(ServiceRegistration))
    {
    }
}