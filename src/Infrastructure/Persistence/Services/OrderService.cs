
namespace Persistence.Services;

// public class OrderService : BaseService  , IOrderService
// {
//     private readonly IReadRepository<Order, Guid> _read;
//     private readonly IWriteRepository<Order, Guid> _write;
//     public OrderService(IUnitOfWork unitOfWork, IQueryRepositories queryRepositories,IMapper mapper, ILogger<IBaseService> logger) : 
//         base(unitOfWork, queryRepositories,mapper,logger)
//     {
//         _read = queryRepositories.Repository<Order, Guid>();
//         _write = unitOfWork.Repository<Order, Guid>();
//     }
//
//     public async Task<SourcePaged<OrderDto>> GetAll(GetAllOrderReq req)
//     {
//         IQueryable<Order> query = req.Filter == null
//             ? _read.GetAllQueryable()
//             : req.Filter.Filtered(_read.GetAllQueryable());
//
//         query = query.Where(s => req.IsAdmin || (req.UserId == s.UserId));
//         query = query.Where(s => s.StatusId != Status.Draft.Id);
//         query = query.Include(s => s.Status)
//             .Include(s => s.User)
//             .Include(s => s.Items)!.ThenInclude(s => s.Auction)!.ThenInclude(s => s!.Product)
//             .Include(s => s.Items)!.ThenInclude(s => s.Product);
//
//         var (list,pageModel) = await SourcePaged<Order>.PagedSourceAsync(query, req);
//
//         var result = list.Select(Mapper.Map<OrderDto>).ToList();
//
//         foreach (var order in result)
//         {
//             LanguageHelper.GetLocalized(list.First(s => s.Id == order.Id).Status, order.Status, req.Lang);
//         }
//         return SourcePaged<OrderDto>.Paged(result,pageModel);
//     }
//
//     public async Task<OrderDto?> Get(GetOrderReq req)
//     {
//         var order = await _read.GetAllQueryable()
//             .Include(s => s.Status)
//             .Include(s => s.User)
//             .Include(s => s.Items)!.ThenInclude(s => s.Auction)!.ThenInclude(s => s!.Product)
//             .Include(s => s.Items)!.ThenInclude(s => s.Product)
//             .FirstOrDefaultAsync(s => s.Id == req.Id && ( req.IsAdmin || (req.UserId == s.UserId) ));
//         if (order == null) return null;
//         
//         var result = Mapper.Map<OrderDto>(order);
//
//         LanguageHelper.GetLocalized(order.Status, result.Status,  req.Lang);
//         foreach (var item in result.Items)
//         {
//             if(item.ModelId == (int)OrderItemModelEnum.Product)
//                 LanguageHelper.GetLocalized(order.Items.First(s => s.Id == item.Id).Product, item.Product, req.Lang);
//             else LanguageHelper.GetLocalized(order.Items.First(s => s.Id == item.Id).Auction.Product, item.Auction.Product, req.Lang);
//         }
//         
//         return result;
//     }
//
// }