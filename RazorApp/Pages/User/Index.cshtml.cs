using Domain.DTOs.Users;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.User;

public class Index(IUserService userService) :PageModel
{
    public List<GetUserDto> Users{get;set;}=[];
    public List<string> Message{get;set;}=[];

     public async Task OnGetAsync()
     {
        var responce=await userService.GetAllAsync(new UserFilters());
        if (!responce.IsSuccess)
        {
            Message.Add("Some thing went wrong");
            return;
        }
        Users=responce.Data!;
     }

}
