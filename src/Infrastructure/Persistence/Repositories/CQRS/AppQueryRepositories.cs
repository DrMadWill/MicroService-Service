using AutoMapper;
using DrMadWill.Layers.Repository.Concretes.CQRS;
using Persistence.Context;
using AppContext = Persistence.Context.AppContext;

namespace Persistence.Repositories.CQRS;

public class AppQueryRepositories : QueryRepositories 
{
    public AppQueryRepositories(AppContext orgContext, IMapper mapper) :
        base(orgContext, mapper, typeof(ServiceRegistration))
    {
    }
}