using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Menues;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class MenuService(DataContext context, IMapper mapper) : IMenuService
{
    public async Task<Response<GetMenuDto>> CreateAsync(CreateMenuDto request)
    {
        var menu = mapper.Map<Menu>(request);
        await context.Menus.AddAsync(menu);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetMenuDto>(menu);
        return result == 0 ?
        new Response<GetMenuDto>(HttpStatusCode.BadRequest, "Menu could`t added!")
        : new Response<GetMenuDto>(data);


    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var menu = await context.Menus.FindAsync(Id);
        if (menu == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Id is not found!");
        }
        context.Menus.Remove(menu);
        var result = await context.SaveChangesAsync();

        return result == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "Menu could`t delete")
        : new Response<string>("Menu deleted!");


    }

    public async Task<Response<List<GetMenuDto>>> GetAllAsync(MenuFilters filter)
    {
        try
        {
            var menu = context.Menus.AsQueryable();
            if (filter.Name != null)
            {
                menu = menu.Where(m => m.Name.ToLower().Contains(filter.Name));
            }

            if (filter.From != null)
            {
                menu = menu.Where(m => m.Price >= filter.From);
            }
            if (filter.To != null)
            {
                menu = menu.Where(m => m.Price <= filter.To);
            }


            var mapped = mapper.Map<List<GetMenuDto>>(menu);
            var totalRecords = mapped.Count;

            var data = mapped
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).ToList();

            return new PagedResponse<List<GetMenuDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
        }
        catch (System.Exception message)
        {
            System.Console.WriteLine(message);
            throw;
        }

    }

    public async Task<Response<GetMenuDto>> GetMenuAsync(int Id)
    {
        var menu = await context.Menus.FindAsync(Id);
        if (menu == null)
        {
            return new Response<GetMenuDto>(HttpStatusCode.NotFound, "Id not found!");
        }

        var data = mapper.Map<GetMenuDto>(menu);

        return new Response<GetMenuDto>(data);
    }


    public async Task<Response<GetMenuDto>> UpDateAsync(int Id, UpdateMenuDto request)
    {
        var menu = await context.Menus.FindAsync(Id);
        if (menu == null)
        {
            return new Response<GetMenuDto>(HttpStatusCode.NotFound, "Id not found!");
        }

        menu.Name = request.Name;
        menu.Category = request.Category;
        menu.PhotoUrl = request.PhotoUrl;
        menu.Price = request.Price;
        menu.RestaurantId = request.RestaurantId;
        menu.Weight = request.Weight;
        menu.PreparationTime = request.PreparationTime;
        menu.IsAvailable = request.IsAvailable;

        var res = await context.SaveChangesAsync();
        var data = mapper.Map<GetMenuDto>(menu);

        return res == 0 ?
        new Response<GetMenuDto>(HttpStatusCode.BadRequest, "Menu could`t updated")
        : new Response<GetMenuDto>(data);
    }


    // task_2
    public async Task<Response<List<GetMenuDto>>> GetMenuAvailable()
    {
        var menu = await context.Menus.Where(m => m.Price < 1000).ToListAsync();
        var data = mapper.Map<List<GetMenuDto>>(menu);
        return new Response<List<GetMenuDto>>(data);
    }

    // task_3
    public async Task<Response<List<MenuCategoryAveragePriceDto>>> Menuby_category()
    {
        var menu = await context.Menus.GroupBy(m => m.Category)
        .Select(g => new MenuCategoryAveragePriceDto()
        {
            Category = g.Key,
            AverageCount = g.Average(a => a.Price)
        }).ToListAsync();
        var data = mapper.Map<List<MenuCategoryAveragePriceDto>>(menu);
        return new Response<List<MenuCategoryAveragePriceDto>>(data);
    }
    
    // task_10
    public async Task<Response<MenuPopularCategoryDto>> GetMenuPopularCategory()
    {
        var menu = await context.Menus.GroupBy(m => m.Category).Select(g => new MenuPopularCategoryDto()
        {
            Category = g.Key,
            Count = g.Count()

        }).OrderByDescending(x => x.Count)
        .FirstOrDefaultAsync();

        return new Response<MenuPopularCategoryDto>(menu);
    }



}