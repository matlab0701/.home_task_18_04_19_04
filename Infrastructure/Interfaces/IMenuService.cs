using Domain.DTOs;
using Domain.DTOs.Menues;
using Domain.Filters;
using Domain.Responses;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IMenuService
{
     Task<Response<List<GetMenuDto>>> GetAllAsync(MenuFilters filter);
    Task<Response<GetMenuDto>> CreateAsync(CreateMenuDto request);
    Task<Response<GetMenuDto>> GetMenuAsync(int Id);
    Task<Response<GetMenuDto>> UpDateAsync(int Id, UpdateMenuDto request);
    Task<Response<string>> DeleteAsync(int Id);
}
