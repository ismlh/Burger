namespace Burger.ViewModels
{
    public class UpdateUserData
    {
        [MaxLength(250)]
        [MinLength(2, ErrorMessage = "Name Must Be At Least 2 char")]
        public string userName { get; set; }

        [MaxLength(250)]
        [MinLength(2, ErrorMessage = "Name Must Be At Least 2 char")]
        public string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01(0|1|2|5)[0-9]{8}$", ErrorMessage = "Must Be Correct Mobile Number")]
        public string PhoneNumber { get; set; }


    }
}
