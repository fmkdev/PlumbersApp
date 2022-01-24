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
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IJobService _jobService;

        public CustomerController(ICustomerService adminService, IWebHostEnvironment webHostEnvironment, IJobService jobService)
        {
            _customerService = adminService;
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
        public IActionResult Create(CreateCustomerRequestModel model, IFormFile photo)
        {
            string customerImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "CustomerImages");
            Directory.CreateDirectory(customerImagePath);
            string contentType = photo.ContentType.Split('/')[1];
            string customerImage = $"APT{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(customerImagePath, customerImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            model.CustomerPhoto = customerImage;
            var customer = _customerService.Create(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult CustomerProfile()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == 0)
            {
                return RedirectToAction("Login");
            }
            var customer = _customerService.Get(id);
            return View(customer);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Update()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var customer = _customerService.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Update(UpdateCustomerRequestModel model, IFormFile photo)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            string customerImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "CustomerImages");
            Directory.CreateDirectory(customerImagePath);
            string contentType = photo.ContentType.Split('/')[1];
            string customerImage = $"APT{Guid.NewGuid()}.{contentType}";
            string fullPath = Path.Combine(customerImagePath, customerImage);
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
            model.CustomerPhoto = customerImage;
            var customer = _customerService.Update(model, id);
            return RedirectToAction("CustomerProfile");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginCustomerRequestModel model)
        {
            var customer = _customerService.Login(model);
            if (customer != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, $"{customer.FirstName} {customer.LastName}"),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new Claim("image", customer.CustomerPhoto),
                    new Claim(ClaimTypes.Role, "Customer")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction("CustomerProfile", "Customer");
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
        public IActionResult CreateJob()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateJob(CreateJobRequestModel model)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var job = _jobService.CreateJob(model, id);
            return RedirectToAction("ViewJobs");

        }
        [HttpGet]
        public IActionResult ViewJobs()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var jobs = _jobService.ViewCustomerJobs(id);
            return View(jobs);
        }
        [HttpGet]
        public IActionResult ViewDoneJobs()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var jobs = _jobService.GetAllCustomerDoneJobs(id);
            return View(jobs);
        }
        
        [HttpGet]
        public IActionResult SubmitReport(int id)
        {
            var job = _jobService.GetJob(id);
            return View();
        }
        [HttpPost]
        public IActionResult SubmitReport(CustomerReportModel customerreport, int id)
        {
            var jobs = _jobService.SubmitCustomerReport(customerreport, id);
            return RedirectToAction(nameof(ViewDoneJobs));
        }
    
    }
}