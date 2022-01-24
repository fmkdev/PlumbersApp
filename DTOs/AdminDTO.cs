using System.ComponentModel.DataAnnotations;

namespace PlumbingService.DTOs
{
    public class AdminDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string AdminPhoto { get; set; }
    }
    public class CreateAdminRequestModel
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
        [Required(ErrorMessage = "Confirm Password")]
        [DataType(DataType.Password, ErrorMessage ="")]
        [Compare("Password", ErrorMessage ="Confirm Password")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Upload)]
        public string AdminPhoto { get; set; }
    }
    public class UpdateAdminRequestModel
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
        [DataType(DataType.Upload)]
        public string AdminPhoto { get; set; }
    }
    public class LoginAdminRequestModel
    {
        [Required(ErrorMessage = "Enter Email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Enter Correct Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password, ErrorMessage ="")]
        public string Password { get; set; }

    }
}