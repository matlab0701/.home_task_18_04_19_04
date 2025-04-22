using System.Net;
using AutoMapper;
using Domain.DTOs.Orders;
using Domain.DTOs.Restauronts;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RestaurontService(DataContext context, Mapper mapper) : IRestaurontService
{
    public async Task<Response<GetRestaurantDto>> CreateAsync(CreateRestaurantDto request)
    {
        var restaurants = mapper.Map<Restaurant>(request);
        await context.Restaurants.AddAsync(restaurants);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetRestaurantDto>(restaurants);
        return result == 0 ?

        new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "Restaurant not add!")
        : new Response<GetRestaurantDto>(data);
    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var restaurant = await context.Restaurants.FindAsync(Id);

        if (restaurant == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Id not found!");
        }
        context.Restaurants.Remove(restaurant);
        var res = await context.SaveChangesAsync();

        return res == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "restaurant can`t deleted!")
        : new Response<string>("succes to delet restaurant");
    }

    public async Task<Response<List<GetRestaurantDto>>> GetAllAsync(RestaurontFilters filter)
    {
        var rest = context.Restaurants.AsQueryable();

        if (filter.Name != null)
        {
            rest = rest.Where(u => u.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        if (filter.Address != null)
        {
            rest = rest.Where(u => u.Address.ToLower().Contains(filter.Address.ToLower()));
        }

        if (filter.From != null)
        {
            rest = rest.Where(m => m.DeliveryPrice >= filter.From);
        }
        if (filter.To != null)
        {
            rest = rest.Where(m => m.DeliveryPrice <= filter.To);
        }


        if (filter.From != null)
        {
            rest = rest.Where(r => r.Rating >= filter.Rating);
        }
        if (filter.To != null)
        {
            rest = rest.Where(r => r.Rating <= filter.Rating);
        }


        var mapped = mapper.Map<List<GetRestaurantDto>>(rest);

        var totalRecords = mapped.Count;
        var data = mapped
        .Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        return new PagedResponse<List<GetRestaurantDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
    }


    public async Task<Response<GetRestaurantDto>> GetRestaurontAsync(int Id)
    {
        var restauront = await context.Restaurants.FindAsync(Id);
        if (restauront == null)
        {
            return new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "Id not found!");
        }

        var data = mapper.Map<GetRestaurantDto>(restauront);

        return new Response<GetRestaurantDto>(data);
    }



    public async Task<Response<GetRestaurantDto>> UpDateAsync(int Id, UpdateRestaurantDto request)
    {
        var restauront = await context.Restaurants.FindAsync(Id);
        if (restauront == null)
        {
            return new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "Id not found!");
        }

        restauront.Name = request.Name;
        restauront.Address = request.Address;
        restauront.ContactPhone = request.ContactPhone;
        restauront.DeliveryPrice = request.DeliveryPrice;
        restauront.IsActive = request.IsActive;
        restauront.MinOrderAmount = request.MinOrderAmount;
        restauront.WorkingHours = request.WorkingHours;
        var res = await context.SaveChangesAsync();

        var data = mapper.Map<GetRestaurantDto>(restauront);

        return res == 0 ?
       new Response<GetRestaurantDto>(HttpStatusCode.BadRequest, "restauront could not be Updated")
       : new Response<GetRestaurantDto>(data);
    }


    // task_1
    public async Task<Response<List<GetRestaurantDto>>> GetRestaurantactive()
    {
        var restaurant = await context.Restaurants.Where(r => r.IsActive == true).
        OrderByDescending(r => r.Rating)
        .ToListAsync();

        var data = mapper.Map<List<GetRestaurantDto>>(restaurant);

        return new Response<List<GetRestaurantDto>>(data);
    }
    // task_11
    public async Task<Response<List<TopRestaurantDto>>> AnalyticsRestaurantsTop()
    {
        var CurrentMonth = DateTime.Now.AddMonths(-1);
        var rest = await context.Orders.Where(o => o.CreatedAt.Date >= CurrentMonth).
        Join(context.Restaurants,
        order => order.RestaurantId,
        restaurant => restaurant.Id,
        (order, restaurant) => new { order, restaurant }
        )
        .GroupBy(x => x.restaurant.Id, x => x.order, (restaurantId, orders) => new TopRestaurantDto()
        {
            RestaurantId = restaurantId,
            OrderCount = orders.Count(),
            RestaurantName = orders.FirstOrDefault().Restaurant.Name

        }).OrderByDescending(x => x.OrderCount)
        .Take(10)
        .ToListAsync();
        return new Response<List<TopRestaurantDto>>(rest);
    }

    // task_12
    public async Task<Response<List<RestaurantRevenueDto>>> AnalyticsRestaurantsRevenue()
    {
        var startDate = DateTime.UtcNow.AddDays(-7);
        var data = await context.Orders
            .Where(o => o.CreatedAt.Date >= startDate)
            .GroupBy(o => new
            {
                RestaurantId = o.RestaurantId,
                Date = o.CreatedAt.Date
            })
            .Select(g => new RestaurantRevenueDto()
            {
                RestaurantId = g.Key.RestaurantId,
                Date = g.Key.Date,
                TotalRevenue = g.Sum(o => o.TotalAmount)
            })
            .ToListAsync();
        return new Response<List<RestaurantRevenueDto>>(data);
    }

    // task_13
    public async Task<Response<List<RestaurantsDishesPopularDto>>> RestaurantsDishesPopular()
    {
        var restaurants = await context.OrderDetails
          .Include(n => n.Order)
          .Include(n => n.MenuItem)
          .GroupBy(n => new
          {
              n.Order.RestaurantId,
              n.MenuItem.Name
          })
          .Select(n => new
          {
              n.Key.RestaurantId,
              DishName = n.Key.Name,
              Count = n.Count()
          })
          .ToListAsync();

        var top = restaurants
            .GroupBy(n => n.RestaurantId)
            .SelectMany(n => n
                .OrderByDescending(n => n.Count)
                .Take(3))
            .Select(n => new RestaurantsDishesPopularDto
            {
                RestaurantId = n.RestaurantId,
                DishName = n.DishName,
                Count = n.Count
            })
            .ToList();
        return new Response<List<RestaurantsDishesPopularDto>>(top);

    }







}
