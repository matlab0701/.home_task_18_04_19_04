using Domain.DTOs.Users;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorApp.Pages.User;

public class Edit(IUserService userService) : PageModel
{
    [BindProperty]
    public UpdateUserDto UpdateUserDto { get; set; } = new();

    public List<string> Messages { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var responce = await userService.GetUserAsync(id);
        if (!responce.IsSuccess)
        {
            return StatusCode(responce.StatusCode, responce.Message!);
        }
        UpdateUserDto = new UpdateUserDto()
        {
            Id = responce.Data!.Id,
            Name = responce.Data.Name,
            Email = responce.Data.Email,
            Address = responce.Data.Address,
            Password = responce.Data.Password,
            Phone = responce.Data.Phone,
            Role = responce.Data.Role,

        };
        return Page();

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Messages = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).ToList();
            return Page();
        }

        var result = await userService.UpDateAsync(UpdateUserDto.Id, UpdateUserDto);

        if (result.IsSuccess)
        {
            return Redirect("/user/index/");

        }
        Messages.Add("some thing went wrong");
        return Page();
    }



}
