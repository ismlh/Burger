namespace Burger.ViewModels
{
    public class CategoryVM
    {
        [MaxLength(255), MinLength(2, ErrorMessage = "At Least 2 Chars")]
        public string Name { get; set; }
    }
}
