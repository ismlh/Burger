namespace Burger.ViewModels
{
    public class ReservationViewModel
    {



        [MinLength(2, ErrorMessage = "Name Must Be At Least 2 char")]
        public string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^01(0|1|2|5)[0-9]{8}$", ErrorMessage = "Must Be Correct Mobile Number")]
        public string PhoneNumber { get; set; }

        public int NumOfpersons { get; set; }

        public DateTime date { get; set; }

        public string UserId { get; set; }


    }
}
