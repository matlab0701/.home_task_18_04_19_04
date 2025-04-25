using Domain.DTOs.Users;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.User;

public class Create(IUserService userService) : PageModel
{
    [BindProperty]
    public CreateUserDto CreateUserDto{get;set;}=new ();
    public List<string> Message {get;set;}=new();

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
         if (!ModelState.IsValid)
         {
            Message=ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();
            return Page();
         }
         var responce=await userService.CreateAsync(CreateUserDto);
         if (responce.IsSuccess)
         {
            return Redirect("/user/index/");
         }
         Message.Add(responce.Message!);
         return Page();
    }
}
