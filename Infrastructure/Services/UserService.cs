using System.Net;
using AutoMapper;
using Domain.DTOs.Users;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext context, IMapper mapper) : IUserService
{
    public async Task<Response<GetUserDto>> CreateAsync(CreateUserDto request)
    {
        var users = mapper.Map<User>(request);
        await context.Users.AddAsync(users);
        var result = await context.SaveChangesAsync();
        var data = mapper.Map<GetUserDto>(users);
        return result == 0 ?

        new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not add!")
        : new Response<GetUserDto>(data);
    }

    public async Task<Response<string>> DeleteAsync(int Id)
    {
        var user = await context.Users.FindAsync(Id);

        if (user == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Id not found!");
        }
        context.Users.Remove(user);
        var res = await context.SaveChangesAsync();

        return res == 0 ?
        new Response<string>(HttpStatusCode.BadRequest, "User can`t deleted!")
        : new Response<string>("succes to delet user");
    }

    public async Task<Response<List<GetUserDto>>> GetAllAsync(UserFilters filter)
    {
        try
        {
            var validFilter = new ValidFilters(filter.PageNumber, filter.PageSize);
            var users = context.Users.AsQueryable();


            if (filter.Name != null)
            {
                users = users.Where(u => u.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.Email != null)
            {
                users = users.Where(u => u.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            if (filter.Password != null)
            {
                users = users.Where(u => u.Password.Length >= 8 && u.Password.Length < 20);
            }
            var mapped = mapper.Map<List<GetUserDto>>(users);

            var totalRecords = mapped.Count;

            var data = mapped
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize).ToList();

            return new PagedResponse<List<GetUserDto>>(data, validFilter.PageNumber, validFilter.PageSize,
                   totalRecords);
        }
        catch (System.Exception message)
        {
            System.Console.WriteLine(message);
            throw;
        }
    }

    public async Task<Response<GetUserDto>> GetUserAsync(int Id)
    {
        var user = await context.Users.FindAsync(Id);
        if (user == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, "Id not found!");
        }

        var data = mapper.Map<GetUserDto>(user);

        return new Response<GetUserDto>(data);

    }

    public async Task<Response<GetUserDto>> UpDateAsync(int Id, UpdateUserDto request)
    {
        var exist = await context.Users.FindAsync(Id);
        if (exist == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, "Id not found!");
        }

        exist.Name = request.Name;
        exist.Address = request.Address;
        exist.Email = request.Email;
        exist.Phone = request.Phone;
        exist.Password = request.Password;
        exist.Role = request.Role;

        var res = await context.SaveChangesAsync();
        var user = mapper.Map<GetUserDto>(exist);

        return res == 0 ?
        new Response<GetUserDto>(HttpStatusCode.BadRequest, "User could not be Updated")
        : new Response<GetUserDto>(user);

    }
    // task_5
    public async Task<Response<List<UserOrderCountDto>>> GetUserOrderCountDto()
    {
        var user = await context.Users
        .GroupJoin(
            context.Orders,
            user => user.Id,
            order => order.UserId,
            (user, order) => new UserOrderCountDto()
            {
                UserId = user.Id,
                UserName = user.Name,
                OrderCount = order.Count()
            }).ToListAsync();
        return new Response<List<UserOrderCountDto>>(user);
    }
   

    // task_18
    public async Task<Response<List<RetainedUserDto>>> RetainedUser()
    {
        var monthAgo = DateTime.Now.AddMonths(-1);

        var result = await context.Orders
            .Where(o => o.CreatedAt >= monthAgo)
            .GroupBy(o => o.UserId)
            .Where(g => g.Count() > 5)
            .Select(g => new
            {
                UserId = g.Key,
                OrdersCount = g.Count()
            })
            .Join(context.Users,
                o => o.UserId,
                u => u.Id,
                (o, u) => new RetainedUserDto
                {
                    UserId = u.Id,
                    UserName = u.Name,
                    OrdersCount = o.OrdersCount
                })
            .ToListAsync();

        return new Response<List<RetainedUserDto>>(result);
    }
}
