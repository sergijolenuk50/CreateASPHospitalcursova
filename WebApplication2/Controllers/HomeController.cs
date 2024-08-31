using AutoMapper;
using Core.Dtos;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        //ILogger<HomeController> logger
        private readonly HospitalDbContext context;
        private readonly IMapper mapper;

        public HomeController(IMapper mapper, HospitalDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //public HomeController()
        //{
        //   // _logger = logger;
        //}

        public IActionResult Index()
        {
            var doctor = context.Doctors
            .Include(x => x.Category)
            .ToList();
            return View(mapper.Map<List<DoctorDto>>(doctor)); // search for view in: ~/Views/Home/Index
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Biography()
        {
            return View();
        }
        public IActionResult Life()
        {
            return View();
        }
        public IActionResult Fact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
