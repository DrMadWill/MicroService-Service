using Application.Events.Integrations.Models;
using DrMadWill.EventBus.Base.Abstractions;
using DrMadWill.Layers.Repository.Abstractions.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Events.Integrations.Handlers;

public class ProductChangingHandler : IIntegrationEventHandler<ProductChangingIntegrationEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public ProductChangingHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Handle(ProductChangingIntegrationEvent @event)
    {
        var scope = _serviceProvider.CreateScope();
        using var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
        //await unitOfWork?.SynchronizationData<ProductChangingIntegrationEvent,Product , Guid>(@event)!;
    }
    
    
}