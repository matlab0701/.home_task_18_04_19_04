using System.Net;
using System.Net.NetworkInformation;
using AutoMapper;
using Domain.DTOs.Orders;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;

public class OrderService(DataContext context, IMapper mapper) : IOrderService
{
    public async Task<Response<GetOrderDto>> CreateAsync(CreateOrderDto request)
    {
        var oders = mapper.Map<Order>(request);
        await context.Orders.AddAsync(oders);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetOrderDto>(oders);
        return result == 0 ?

        new Response<GetOrderDto>(HttpStatusCode.BadRequest, "oder not add!")
        : new Response<GetOrderDto>(data);
    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var order = await context.Orders.FindAsync(Id);

        if (order == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Id not found!");
        }
        context.Orders.Remove(order);
        var res = await context.SaveChangesAsync();

        return res == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "order can`t deleted!")
        : new Response<string>("succes to delet order");
    }

    public async Task<Response<List<GetOrderDto>>> GetAllAsync(OrderFilters filter)
    {

        var order = context.Orders.AsQueryable();

        if (filter.OrderStatus != 0)
        {
            order = order.Where(o => o.OrderStatus == filter.OrderStatus);
        }

        var mapped = mapper.Map<List<GetOrderDto>>(order);

        var totalRecords = mapped.Count;
        var data = mapped
        .Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        return new PagedResponse<List<GetOrderDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetOrderDto>> GetOrderAsync(int Id)
    {
        var order = await context.Orders.FindAsync(Id);
        if (order == null)
        {
            return new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Id not found!");
        }

        var data = mapper.Map<GetOrderDto>(order);

        return new Response<GetOrderDto>(data);
    }

    public async Task<Response<GetOrderDto>> UpDateAsync(int Id, UpdateOrderDto request)
    {

        var order = await context.Orders.FindAsync(Id);
        if (order == null)
        {
            return new Response<GetOrderDto>(HttpStatusCode.BadRequest, "Id not found!");
        }
        order.UserId = request.UserId;
        order.RestaurantId = request.RestaurantId;
        order.CourierId = request.CourierId;
        order.OrderStatus = request.OrderStatus;
        order.TotalAmount = request.TotalAmount;
        order.DeliveryAddress = request.DeliveryAddress;
        order.PaymentMethod = request.PaymentMethod;
        order.PaymentStatus = request.PaymentStatus;
        var res = await context.SaveChangesAsync();
        var data = mapper.Map<GetOrderDto>(order);

        return res == 0 ?
      new Response<GetOrderDto>(HttpStatusCode.BadRequest, "User could not be Updated")
      : new Response<GetOrderDto>(data);

    }

    //task_3
    public async Task<Response<List<OrderStatusCountDto>>> GetOrdersByStatus()
    {
        var orderCounts = await context.Orders
                 .GroupBy(o => o.OrderStatus)
                 .Select(g => new OrderStatusCountDto
                 {
                     OrderStatus = g.Key,
                     Count = g.Count()
                 })
                 .ToListAsync();

        var data = mapper.Map<List<OrderStatusCountDto>>(orderCounts);
        return new Response<List<OrderStatusCountDto>>(data);
    }
    // task_6
    public async Task<Response<List<GetOrderDto>>> GetOrdersByCourier(int courierId)
    {
        var orders = await context.Orders.GroupJoin(context.Couriers,
        o => o.CourierId,
        courier => courier.Id,
        (o, courier) => new { o, courier }
        ).Where(x => x.o.CourierId == courierId).ToListAsync();
        var data = mapper.Map<List<GetOrderDto>>(orders);
        return new Response<List<GetOrderDto>>(data);
    }

    //task_7
    public async Task<Response<GetOrderDto>> GetOrderToday()
    {
        var Today = DateTime.Today;
        var order = await context.Orders.Where
        (o => o.CreatedAt.Date == Today)
        .SumAsync(o => o.TotalAmount);

        var data = mapper.Map<GetOrderDto>(order);
        return new Response<GetOrderDto>(data);
    }

    // task_9
    public async Task<Response<List<GetOrderDto>>> GetOrdersExpensive()
    {
        var orderAverage = await context.Orders.AverageAsync(o => o.TotalAmount);
        var orders = await context.Orders.Where(o => o.TotalAmount > orderAverage).ToListAsync();
        var data = mapper.Map<List<GetOrderDto>>(orders);
        return new Response<List<GetOrderDto>>(data);
    }
    // task_14
    public async Task<Response<List<OrdersPeakHoursDto>>> OrdersPeakHours()
    {


        var result = await context.Orders
            .GroupBy(o => o.CreatedAt.Hour)
            .Select(g => new OrdersPeakHoursDto
            {
                Hour = g.Key,
                OrderCount = g.Count()
            })
            .OrderByDescending(x => x.OrderCount)
            .ToListAsync();
        return new Response<List<OrdersPeakHoursDto>>(result);

    }
    // task_15
    public async Task<Response<List<OrdersAverageCheckDto>>> OrdersAverageCheck()
    {
        var res = await context.Orders
        .GroupBy(o => o.DeliveryAddress)
        .Select(g => new OrdersAverageCheckDto()
        {
            DeliveryAddress = g.Key,
            CheckAverage = g.Average(x => x.TotalAmount)

        }).ToListAsync();

        return new Response<List<OrdersAverageCheckDto>>(res);
    }

    // task_16
    public async Task<Response<List<OrdersDeliveryTimeDto>>> OrdersDeliveryTime()
    {
        var order = await context.Orders.GroupBy(o => o.DeliveryAddress)
        .Select(g => new OrdersDeliveryTimeDto
        {
            Address = g.Key,
            Tome = g.Average(x => (x.DeliveredAt.Value - x.CreatedAt).TotalMinutes)
        }).ToListAsync();

        return new Response<List<OrdersDeliveryTimeDto>>(order);

    }






}
