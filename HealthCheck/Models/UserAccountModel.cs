using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthCheck.Models
{
    public class ValidatePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string password = value.ToString();

                // Add your password validation logic here
                if (password.Length < 8 || password.Length > 20)
                {
                    return new ValidationResult("Password must be between 8 and 20 characters.");
                }
                // Add more validation criteria if needed
            }

            // Return ValidationResult.Success if validation passes
            return ValidationResult.Success;
        }
    }

    public class UserAccountModel
    {
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Full Name should contain only alphabet characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile Number must be exactly 10 numeric characters with no space.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        [ValidatePassword(ErrorMessage = "Incorrect Passsword.")]
        public string Password { get; set; }

        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2007", ErrorMessage = "Invalid date of birth.")]
        public DateTime DOB { get; set; }

        public string ModelError { get; set; }
    }

}