using APBD_T9_s33596.ViewModels;
using APBD_T9_s33596.ViewModels.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_T9_s33596.Controllers
{
    [Authorize(Roles = "Admin")]  
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAll();

            var viewModels = users.Select(u => new UserInfoViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role,
                CreatedAtFormatted = u.CreatedAt.ToString("yyyy-MM-dd")
            }).ToList();

            return View(viewModels);
        }

    }
}
