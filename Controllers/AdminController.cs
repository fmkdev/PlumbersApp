using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlumbingService.DTOs;
using PlumbingService.Interfaces.IServices;

namespace PlumbingService.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IJobService _jobService;

        public AdminController(IAdminService adminService, IWebHostEnvironment webHostEnvironment, IJobService jobService)
        {
            _adminService = adminService;
            _webHostEnvironment = webHostEnvironment;
            _jobService = jobService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateAdminRequestModel model, IFormFile photo)
        {
            string adminImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "AdminImages");
            Directory.CreateDirectory(adminImagePath);
            string contentType = photo.ContentType.Split('/')[1];
            string adminImage = $"APT{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(adminImagePath, adminImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            model.AdminPhoto = adminImage;
            var apprentice = _adminService.Create(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
             if(id == 0)
            {
                return RedirectToAction("Login");
            }
            var admin = _adminService.Get(id);
            return View(admin);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Update()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var admin = _adminService.Get(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Update(UpdateAdminRequestModel model, IFormFile photo)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
             string adminImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "AdminImages");
            Directory.CreateDirectory(adminImagePath);
            string contentType = photo.ContentType.Split('/')[1];
            string adminImage = $"APT{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(adminImagePath, adminImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            model.AdminPhoto = adminImage;
            var apprentice = _adminService.Update(model, id);
            return RedirectToAction("Profile");
        }
       
        [HttpGet]
         public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginAdminRequestModel model)
        {
            var admin = _adminService.Login(model);
            if (admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim("image", admin.AdminPhoto),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction("Profile");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password";
                return View();
            }
        }
         public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return View("Login");
        }

        [HttpGet]
        public IActionResult ViewAvailableJobs()
        {
            var jobs = _jobService.GetInitializedJob();
            return View(jobs);
        }
      
        [HttpGet]
        public IActionResult UpdateJobStatus(int id)
        {
            var jobs = _jobService.UpdateJobStatusToAssigned(id);
            return RedirectToAction("ViewAvailableJobs");
        }

        [HttpGet]
        public IActionResult ViewAllJobs()
        {
            var jobs = _jobService.GetAllCreatedJobs();
            return View(jobs);
        }
        [HttpGet]
        public IActionResult ViewAcceptJobs()
        {
            var jobs = _jobService.GetAllAcceptJobs();
            return View(jobs);
        }
        [HttpGet]
        public IActionResult ViewVerifiedJobs()
        {
            var jobs = _jobService.GetAllVerifiedJobs();
            return View(jobs);
        }
        [HttpGet]
        public IActionResult ViewCompletedJobs()
        {
            var jobs = _jobService.GetAllCompletedJobs();
            return View(jobs);
        }

        [HttpGet]
        public IActionResult UpdateJobStatusToAccept(int id)
        {
            var jobs = _jobService.UpdateJobStatusToAccept(id);
            return RedirectToAction("ViewCompletedJobs");
        }

        [HttpGet]
        public IActionResult UpdateJobStatusToCompleted(int id)
        {
            var jobs = _jobService.UpdateJobStatusToCompleted(id);
            return RedirectToAction("ViewCompletedJobs");
        }
        
    }
}