using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlumbingService.Models.Entities;

namespace PlumbingService.DTOs
{
    public class PlumberDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public string PlumberPhoto { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
    public class CreatePlumberRequestModel
    {
        [Required(ErrorMessage = "Enter First Name")]
        [StringLength(maximumLength:25, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        [StringLength(maximumLength:25, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Enter Correct Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Enter Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Your Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password, ErrorMessage ="")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password, ErrorMessage ="")]
        [Compare("Password", ErrorMessage ="Confirm Password")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Upload)]
        public string PlumberPhoto { get; set; }
    }
    public class UpdatePlumberRequestModel
    {
        [Required(ErrorMessage = "Enter First Name")]
        [StringLength(maximumLength:25, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        [StringLength(maximumLength:25, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Enter Correct Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Phone Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="Enter Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password, ErrorMessage ="")]
        public string Password { get; set; }
        [DataType(DataType.Upload)]
        public string PlumberPhoto { get; set; }
    }
    public class LoginPlumberRequestModel
    {
        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Enter Correct Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password, ErrorMessage ="")]
        public string Password { get; set; }


    }
}