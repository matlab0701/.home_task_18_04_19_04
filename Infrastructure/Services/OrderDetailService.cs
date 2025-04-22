using System.Net;
using AutoMapper;
using Domain.DTOs.OrderDetails;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class OrderDetailService(DataContext context, Mapper mapper) : IOrderDetailService
{
    public async Task<Response<GetOrderDetailDto>> CreateAsync(CreateOrderDetailDto request)
    {
        var exist = mapper.Map<OrderDetail>(request);
        await context.OrderDetails.AddAsync(exist);
        var res = await context.SaveChangesAsync();

        var data = mapper.Map<GetOrderDetailDto>(exist);

        return res == 0 ?
        new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "OrderDetails not added!")
        : new Response<GetOrderDetailDto>(data);

    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var orderDetail = await context.OrderDetails.FindAsync(Id);

        if (orderDetail == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Id not found!");
        }
        context.OrderDetails.Remove(orderDetail);
        var res = await context.SaveChangesAsync();

        return res == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "orderDetail can`t deleted!")
        : new Response<string>("succes to delet orderDetail");
    }

    public async Task<Response<List<GetOrderDetailDto>>> GetAllAsync(OrderDetailFilters filter)
    {
        var order = context.OrderDetails.AsQueryable();

        if (filter.Quantity != null)
        {
            order = order.Where(o => o.Quantity > 0);

        }

        if (filter.From != null)
        {
            order = order.Where(o => o.Price >= filter.From);
        }
        if (filter.To != null)
        {
            order = order.Where(o => o.Price <= filter.To);
        }


        var mapped = mapper.Map<List<GetOrderDetailDto>>(order);
        var totalRecords = mapped.Count;

        var data = mapped
        .Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize).ToList();
        return new PagedResponse<List<GetOrderDetailDto>>(data, filter.PageNumber, filter.PageSize, totalRecords);



    }

    public async Task<Response<GetOrderDetailDto>> GetOrderDetailAsync(int Id)
    {
        var ord = await context.OrderDetails.FindAsync(Id);
        if (ord == null)
        {
            return new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "Id not found!");
        }
        var data = mapper.Map<GetOrderDetailDto>(ord);
        return new Response<GetOrderDetailDto>(data);

    }

    public async Task<Response<GetOrderDetailDto>> UpDateAsync(int Id, UpdateOrderDetailDto request)
    {
        var ord = await context.OrderDetails.FindAsync(Id);
        if (ord == null)
        {
            return new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "Id not found!");
        }

        ord.MenuItemId = request.MenuItemId;
        ord.OrderId = request.OrderId;
        ord.Price = request.Price;
        ord.Quantity = request.Quantity;
        ord.SpecialInstructions = request.SpecialInstructions;
        var res = await context.SaveChangesAsync();
        var data = mapper.Map<GetOrderDetailDto>(ord);

        return res == 0 ?
        new Response<GetOrderDetailDto>(HttpStatusCode.BadRequest, "Order not updated")
          : new Response<GetOrderDetailDto>(data);
    }

}
