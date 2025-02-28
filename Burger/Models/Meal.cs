namespace Burger.Models
{
    public class Meal
    {
        public int Id { get; set; }
        [MaxLength(255), MinLength(2, ErrorMessage = "At Least 2 Chars")]
        public string Name { get; set; }

        [MaxLength(500), MinLength(2, ErrorMessage = "At Least 2 Chars")]
        public string Description { get; set; }

        public double Price { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }

}
