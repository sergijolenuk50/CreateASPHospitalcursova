using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Core.Services;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.SeedExtensions;

namespace WebApplication2.Controllers
{
    [Authorize (Roles = Roles.ADMIN)]
    public class DoctorsController : Controller
    {
       // static List<Doctor>  doctor;
           //private HospitalDbContext ctx = new HospitalDbContext();
        private readonly IMapper mapper;
        private readonly HospitalDbContext ctx;
        private readonly IFileServices fileServices;
        public DoctorsController(IMapper mapper, HospitalDbContext hospitalDbContext, IFileServices fileServices) {
            this.mapper = mapper;
            this.ctx = hospitalDbContext;
            this.fileServices = fileServices;
            //doctor = new List<Doctor>() {
            //    new Doctor() {Id = 1, FirstName ="Grec", Name = "Stepan", LastName = "Ivanivuch" , Birthday = new DateTime(1985,05,01), Categori = "LOR", Work_experience =5},
            //    new Doctor() {Id = 1, FirstName ="Ivanov", Name = "Stanislav", LastName = "Ivanivuch" , Birthday = new DateTime(1988,11,12), Categori = "TERAPEVT", Work_experience =6},
            //    new Doctor() {Id = 1, FirstName ="Sidorov", Name = "Sergii", LastName = "Romanovich" , Birthday = new DateTime(1981,06,25), Categori = "SURGEON", Work_experience =10},
            //    };
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var doctors = ctx.Doctors
                .Include(x => x.Category)
                .Where(x => !x.Archived)
                .ToList();
            return View(mapper.Map<List<DoctorDto>>(doctors));
        }

        public IActionResult Archive()
        {
            // .. load data from database ..
            var doctors = ctx.Doctors
                .Include(x => x.Category) // LEFT JOIN
                .Where(x => x.Archived)
                .ToList();

            return View(mapper.Map<List<DoctorDto>>(doctors));
        }

        // GET: 
        [HttpGet]
        public IActionResult Create()
        {
            // ViewBag.PropertyName = value;
            ViewBag.Categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");
            //LoadCategories();
            ViewBag.CreateMode = true;

            return View("Upsert");
        }

        
        // POST
        [HttpPost]
        public async Task<IActionResult> Create(DoctorDto model)
        {
            // TODO: add data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = false;
                LoadCategories();
                return View("Upsert", model);
            }
            
            var entyti = mapper.Map<Doctor>(model);
            entyti.ImageUrl = await fileServices.SaveDoctorImage(model.Image);

          
            ctx.Doctors.Add(entyti);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            var doctors = ctx.Doctors.Find(id);

            if (doctors == null) return NotFound();

            LoadCategories();
            ViewBag.CreateMode = false;
            return View("Upsert", mapper.Map<DoctorDto>(doctors));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorDto model)
        {
            // TODO: add data validation
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = false;
                LoadCategories();
                return View("Upsert", model);
            }
            if (model.Image != null)
            {
                model.ImageUrl = await fileServices.EditDoctorImage(model.ImageUrl, model.Image);
            }
            ctx.Doctors.Update(mapper.Map<Doctor>(model));
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }










        public async Task<IActionResult> Delete(int id)
        {
            var doctors = ctx.Doctors.Find(id);

            if (doctors == null) return NotFound();
            await fileServices.DeleteDoctorImage(doctors.ImageUrl);

            ctx.Doctors.Remove(doctors);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult ArchiveItem(int id)
        {
            var doctors = ctx.Doctors.Find(id);

            if (doctors == null) return NotFound();

            doctors.Archived = true;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult RestoreItem(int id)
        {
            var doctors = ctx.Doctors.Find(id);

            if (doctors == null) return NotFound();

            doctors.Archived = false;
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }
        private void LoadCategories()
        {
            // ViewBag.PropertyName = value;
            ViewBag.Categories = new SelectList(ctx.Categories.ToList(), "Id", "Name");
        }
    }
}
