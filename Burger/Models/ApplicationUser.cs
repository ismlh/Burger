
namespace Burger.Models
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(250)]
        [MinLength(2,ErrorMessage ="Name Must Be At Least 2 char")]
        public string FullName { get; set; }

        
        public virtual ICollection<Reservations> Reservations { get; set; }

    }
}
