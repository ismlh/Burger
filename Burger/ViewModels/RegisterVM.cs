using System.ComponentModel.DataAnnotations;

namespace Burger.ViewModels
{
    public class RegisterVM
    {
        [MaxLength(250)]
        [MinLength(2, ErrorMessage = "Name Must Be At Least 2 char")]
        public string  userName{ get; set; }

        [MaxLength(250)]
        [MinLength(2, ErrorMessage = "Name Must Be At Least 2 char")]
        public string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01(0|1|2|5)[0-9]{8}$",ErrorMessage ="Must Be Correct Mobile Number")]
        public string PhoneNumber { get; set; }


    }
}
