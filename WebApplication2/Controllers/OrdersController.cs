using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Core.Services;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    //public class OrdersController : Controller
    //{
    //}
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersServices ordersService;
        private readonly IMapper mapper;
        private readonly HospitalDbContext context;
        private readonly RoleManager<IdentityRole> userManager;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public OrdersController(IOrdersServices ordersService, IMapper mapper, HospitalDbContext context)
        {
            this.ordersService = ordersService;
            this.mapper = mapper;
            this.context = context;
            // this.userManager = userManager;
        }
        public IActionResult Index()
        {
            // userManager.CreateAsync(new IdentityRole());
            //var doctor = context.Doctors;
            //.Include(x => x.Category)
            //.ToList();
            //return View(mapper.Map<List<DoctorDto>>(doctor));
            return View(ordersService.GetOrders(UserId));
        }

        public IActionResult Create(int doctorId)
        {
            ordersService.Create(UserId, doctorId);
            return RedirectToAction(nameof(Index));
        }
    }
}
