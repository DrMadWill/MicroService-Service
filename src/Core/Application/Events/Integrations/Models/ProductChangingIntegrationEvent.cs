using DrMadWill.EventBus.Base.Events;
using DrMadWill.Layers.Core.Abstractions;

namespace Application.Events.Integrations.Models;

public class ProductChangingIntegrationEvent : IntegrationEvent,IHasDelete
{
    public Guid ProductId { get; set; }
    public string? Name_az { get; set; }
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
    public string? Image { get; set; }
    public double? OldPrice { get; set; }
    public double? Price { get; set; }
    public float? Discount { get; set; }
    public byte Star { get; set; }
    public bool Availability { get; set; } 
    public bool? IsDeleted { get; set; }
}