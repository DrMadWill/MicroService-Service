using DrMadWill.Layers.Core;
using DrMadWill.Layers.Core.Abstractions;

namespace Application.DTOs;

public class BaseDTO<T> : IBaseDto<T>
{
    public T Id { get; set; }
}