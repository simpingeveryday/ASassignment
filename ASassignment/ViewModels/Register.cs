using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ASassignment.ViewModels
{
    public class Register
    {
        [Required]
        [DataType(DataType.Text), MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text), MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Text), MaxLength(6)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Text), MaxLength(50)]
        public string NRIC { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.*(?=.{12,})(?=.*[a-zA-Z])(?=.*\d)(?=.*[!#$%&? ""]).*$", 
        ErrorMessage = "Password needs 12 characters, lower case, upper case, numbers and special characters.")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.Upload)]

        public string Resume { get; set; }
        [Required]
        
        public string WhoamI { get; set; }

    }
}

