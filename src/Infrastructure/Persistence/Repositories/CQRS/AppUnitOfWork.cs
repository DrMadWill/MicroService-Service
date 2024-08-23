using AutoMapper;
using DrMadWill.Layers.Repository.Concretes.CQRS;
using Persistence.Context;
using AppContext = Persistence.Context.AppContext;

namespace Persistence.Repositories.CQRS;

public class AppUnitOfWork : UnitOfWork
{
    public AppUnitOfWork(AppContext orgContext,IMapper mapper) : 
        base(orgContext, typeof(ServiceRegistration),mapper)
    {
    }
}