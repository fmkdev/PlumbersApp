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
using PlumbingService.DTOs;
using PlumbingService.Interfaces.IServices;

namespace PlumbingService.Controllers
{
    public class PlumberController : Controller
    {
        private readonly IPlumberService _plumberService;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IJobService _jobService;

        public PlumberController(IPlumberService plumberService, IWebHostEnvironment webHostEnvironment, IJobService jobService)
        {
            _plumberService = plumberService;
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
        public IActionResult Create(CreatePlumberRequestModel model, IFormFile photo)
        {
            string plumberImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "PlumberImages");
            Directory.CreateDirectory(plumberImagePath);
            string contentType = photo.ContentType.Split('/')[1];
            string plumberImage = $"APT{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(plumberImagePath, plumberImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            model.PlumberPhoto = plumberImage;
            var plumber = _plumberService.Create(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult PlumberProfile()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == 0)
            {
                return RedirectToAction("Login");
            }
            var plumber = _plumberService.Get(id);
            return View(plumber);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var plumber = _plumberService.Get(id);
            if (plumber == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdatePlumberRequestModel model, IFormFile photo)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            string plumberImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "PlumberImages");
            Directory.CreateDirectory(plumberImagePath);
            string contentType = photo.ContentType.Split('/')[1];
            string plumberImage = $"APT{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(plumberImagePath, plumberImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            model.PlumberPhoto = plumberImage;
            var plumber = _plumberService.Update(model, id);
            return RedirectToAction("PlumberProfile");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginPlumberRequestModel model)
        {
            var plumber = _plumberService.Login(model);
            if (plumber != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{plumber.FirstName} {plumber.LastName}"),
                    new Claim(ClaimTypes.Email, plumber.Email),
                    new Claim(ClaimTypes.NameIdentifier, plumber.Id.ToString()),
                    new Claim("image", plumber.PlumberPhoto),
                    new Claim(ClaimTypes.Role, "Plumber")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction("PlumberProfile", "Plumber");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Login");
        }

        [HttpGet]
        public IActionResult ViewAssignedJob()
        {
            var jobs = _jobService.GetAssignedJobs();
            return View(jobs);
        }

        [HttpGet]
        public IActionResult UpdateJobStatus(int id)
        {
            var PlumberId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var jobs = _jobService.UpdateJobStatusToAccept(id);
            var plumber = _jobService.UpdatePlumberId(id, PlumberId);
            return RedirectToAction("ViewJobs");
        }

        [HttpGet]
        public IActionResult ViewJobs()
        {
            var PlumberId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var jobs = _jobService.ViewPlumberJobs(PlumberId);
            return View(jobs);
        }

        [HttpGet]
        public IActionResult ViewAcceptJob()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var jobs = _jobService.GetAllPlumberAcceptJobs(id);
            return View(jobs);
        }

        [HttpGet]
        [Authorize(Roles=("Plumber"))]
        public IActionResult SubmitReport(int id)
        {
            var job = _jobService.GetJob(id);
            return View();
        }

        [HttpPost]
        public IActionResult SubmitReport(PlumberReportModel report, int id)
        {
           _jobService.SubmitPlumberReport(report, id);
            return RedirectToAction(nameof(ViewJobs));
        }
       
    }
}