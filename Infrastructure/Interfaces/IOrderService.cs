using Domain.DTOs.Orders;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
     Task<Response<List<GetOrderDto>>> GetAllAsync(OrderFilters filter);
    Task<Response<GetOrderDto>> CreateAsync(CreateOrderDto request);
    Task<Response<GetOrderDto>> GetOrderAsync(int Id);
    Task<Response<GetOrderDto>> UpDateAsync(int Id, UpdateOrderDto request);
    Task<Response<string>> DeleteAsync(int Id);
}
