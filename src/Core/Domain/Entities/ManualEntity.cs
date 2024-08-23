using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DrMadWill.Layers.Core.Abstractions;

namespace Domain.Entities;

public class ManualEntity<T> : IOriginEntity<T>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public virtual T Id { get; set; }
}