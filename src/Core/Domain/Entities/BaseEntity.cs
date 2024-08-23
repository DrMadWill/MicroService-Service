using DrMadWill.Layers.Core;
using DrMadWill.Layers.Core.Abstractions;

namespace Domain.Entities;

public abstract class BaseEntity<T> : IBaseEntity<T>
{
    public virtual T? Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool? IsDeleted { get; set; }
}