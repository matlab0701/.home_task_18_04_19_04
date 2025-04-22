using Domain.DTOs.Restauronts;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IRestaurontService
{
     Task<Response<List<GetRestaurantDto>>> GetAllAsync(RestaurontFilters filter);
    Task<Response<GetRestaurantDto>> CreateAsync(CreateRestaurantDto request);
    Task<Response<GetRestaurantDto>> GetRestaurontAsync(int Id);
    Task<Response<GetRestaurantDto>> UpDateAsync(int Id, UpdateRestaurantDto request);
    Task<Response<string>> DeleteAsync(int Id);
}
