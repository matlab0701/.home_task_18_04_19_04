using Domain.DTOs.OrderDetails;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderDetailService
{
    Task<Response<List<GetOrderDetailDto>>> GetAllAsync(OrderDetailFilters filter);
    Task<Response<GetOrderDetailDto>> CreateAsync(CreateOrderDetailDto request);
    Task<Response<GetOrderDetailDto>> GetOrderDetailAsync(int Id);
    Task<Response<GetOrderDetailDto>> UpDateAsync(int Id, UpdateOrderDetailDto request);
    Task<Response<string>> DeleteAsync(int Id);
}
