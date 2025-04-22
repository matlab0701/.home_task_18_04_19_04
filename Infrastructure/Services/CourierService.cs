using System.Net;
using AutoMapper;
using Domain.DTOs.Couriers;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourierService(DataContext context, Mapper mapper) : ICourierService
{
    public async Task<Response<GetCourierDto>> CreateAsync(CreateCourierDto request)
    {
        var courier = mapper.Map<Courier>(request);
        await context.Couriers.AddAsync(courier);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetCourierDto>(courier);
        return result == 0 ?
        new Response<GetCourierDto>(HttpStatusCode.BadRequest, "courier not added!")
        : new Response<GetCourierDto>(data);
    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var courier = await context.Couriers.FindAsync(Id);
        if (courier == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Id is not found!");
        }
        context.Couriers.Remove(courier);
        var result = await context.SaveChangesAsync();

        return result == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "courier could`t delete")
        : new Response<string>("courier deleted!");
    }

    public async Task<Response<List<GetCourierDto>>> GetAllAsync(CourierFilters filter)
    {
        var courier = context.Couriers.AsQueryable();
        if (filter.Status != 0)
        {
            courier = courier.Where(c => c.Status == filter.Status);
        }


        if (filter.From != null)
        {
            courier = courier.Where(c => c.Rating >= filter.Rating);
        }
        if (filter.To != null)
        {
            courier = courier.Where(c => c.Rating <= filter.Rating);
        }

        var mapped = mapper.Map<List<GetCourierDto>>(courier);
        var totalRecord = mapped.Count;
        var data = mapped.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToList();
        return new PagedResponse<List<GetCourierDto>>(data, filter.PageNumber, filter.PageSize, totalRecord);

    }

    public async Task<Response<GetCourierDto>> GetUserAsync(int Id)
    {
        var courier = await context.Couriers.FindAsync(Id);
        if (courier == null)
        {
            return new Response<GetCourierDto>(HttpStatusCode.NotFound, "Id is not found!");
        }

        var data = mapper.Map<GetCourierDto>(courier);

        return new Response<GetCourierDto>(data);
    }



    public async Task<Response<GetCourierDto>> UpDateAsync(int Id, UpdateCourierDto request)
    {
        var courier = await context.Couriers.FindAsync(Id);
        if (courier == null)
        {
            return new Response<GetCourierDto>(HttpStatusCode.NotFound, "Id is not found!");
        }

        courier.UserId = request.UserId;
        courier.Status = request.Status;
        courier.TransportType = request.TransportType;
        courier.CurrentLocation = request.CurrentLocation;

        var res = await context.SaveChangesAsync();
        var data = mapper.Map<GetCourierDto>(courier);

        return res == 0 ?
         new Response<GetCourierDto>(HttpStatusCode.BadRequest, "courier not updated!")
        : new Response<GetCourierDto>(data);
    }


    // task_8
    public async Task<Response<List<GetCourierDto>>> GetCourierstop_rated()
    {
        var courier = await context.Couriers
        .Where(c => c.Rating > 0)
        .OrderByDescending(c => c.Rating)
        .Take(5)
        .Include(c => c.User)
        .ToListAsync();
        var data = mapper.Map<List<GetCourierDto>>(courier);
        return new Response<List<GetCourierDto>>(data);

    }
    //task_19
    public async Task<Response<List<CouriersPerformanceDto>>> CouriersPerformance()
    {
        var cur = await context.Orders.Where(o => o.DeliveredAt != null)
        .Join(context.Couriers,
        o => o.CourierId, c => c.Id,
        (o, c) => new { o, c })
        .GroupBy(x => x.c)
        .Select(g => new CouriersPerformanceDto()
        {
            CourierId = g.Key.Id,
            AvgTime = g.Average(x => (x.o.DeliveredAt.Value - x.o.CreatedAt).TotalMinutes),
            Rating = g.Average(x => x.c.Rating)
        }).ToListAsync();

        return new Response<List<CouriersPerformanceDto>>(cur);

    }

    // Task_20
    public async Task<Response<List<CourierEarningsDto>>> CouriersEarnings()
    {
        var posMonth = DateTime.Now.AddMonths(-1);
        var result = await context.Orders.
        Where(c => c.CreatedAt >= posMonth).
        GroupBy(o => o.Courier)
        .Select(g => new
        {
            CourierId = g.Key,
            TotalEarnings = g.Sum(x => x.TotalAmount)
        }).ToListAsync();

        var data = mapper.Map<List<CourierEarningsDto>>(result);
        return new Response<List<CourierEarningsDto>>(data);
    }
}
