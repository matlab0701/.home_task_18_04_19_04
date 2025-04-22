using Domain.DTOs.Couriers;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICourierService
{
    Task<Response<List<GetCourierDto>>> GetAllAsync(CourierFilters filter);
    Task<Response<GetCourierDto>> CreateAsync(CreateCourierDto request);
    Task<Response<GetCourierDto>> GetUserAsync(int Id);
    Task<Response<GetCourierDto>> UpDateAsync(int Id, UpdateCourierDto request);
    Task<Response<string>> DeleteAsync(int Id);
}
