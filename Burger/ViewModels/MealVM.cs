namespace Burger.ViewModels
{
    public class MealVM
    {
        [MaxLength(255), MinLength(2, ErrorMessage = "At Least 2 Chars")]
        public string Name { get; set; }

        [MaxLength(500), MinLength(2, ErrorMessage = "At Least 2 Chars")]
        public string Description { get; set; }

        public double Price { get; set; }

        public IFormFile? Image { get; set; }

        public int CategoryId { get; set; }

    }
}
