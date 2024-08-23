using Domain.Entities;
using DrMadWill.Layers.Core.Concretes;
using DrMadWill.Layers.Repository.Extensions;

namespace Application.Filters.Orders;

// public class OrderFilter: BaseFilter<BaseEntity<Guid>,Guid>
// {
//     public Guid ProductId { get; set; }
//     public IList<Guid>? ProductsIds { get; set; }
//     public string? UserId { get; set; }
//     public override IQueryable<Order> Filtered(IQueryable<Order> source)
//     {
//         if (Id.NotNull())
//             source = source.Where(s => s.Id == Id);
//         
//         if (Name.NotNull())
//             source = source.Where(s =>
//                 s.User.DisplayName.ToLower().Trim().Contains(Name) ||
//                 s.Items.Any(p =>
//                     p.Product.Name_az.ToLower().Trim().Contains(Name) ||
//                     p.Product.Name_en.ToLower().Trim().Contains(Name) ||
//                     p.Product.Name_ru.ToLower().Trim().Contains(Name)
//                 )
//             );
//
//         
//
//         
//         return source;
//     }
// }