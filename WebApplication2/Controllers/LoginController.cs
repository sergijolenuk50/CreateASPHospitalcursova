//using AutoMapper;
//using Data.Data;
//using Data.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;


//namespace WebApplication2.Controllers
//{
//    public class LoginController : Controller
//    {
//        //private HospitalDbContext ctx = new HospitalDbContext();
//        private readonly HospitalDbContext ctx;
//        public LoginController(HospitalDbContext hospitalDbContext)
//        {
//            this.ctx = hospitalDbContext;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Create()
//        {
//            // ViewBag.PropertyName = value;
//            ViewBag.Doctors = new SelectList(ctx.Doctors.ToList(), "Id", "Id");
//            //LoadCategories();
//            ViewBag.CreateMode = true;

//            return View("Upsert");
//        }


//        // POST
//        [HttpPost]
//        public IActionResult Create(User model)
//        {
//            // TODO: add data validation
//            if (!ModelState.IsValid)
//            {
//                ViewBag.CreateMode = false;
//                LoadCategories();
//                return View("Upsert", model);
//            }

//            //var entyti = mapper.Map<Doctor>(model);
//            ctx.Users.Add(model);
//            ctx.SaveChanges();

//            return RedirectToAction("Index");
//        }
//        private void LoadCategories()
//        {
//            // ViewBag.PropertyName = value;
//            ViewBag.Doctors = new SelectList(ctx.Doctors.ToList(), "Id", "Id");
//        }
//    }
//}
