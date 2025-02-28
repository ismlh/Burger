namespace Burger.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(255),MinLength(2,ErrorMessage ="At Least 2 Chars")]
        public string Name { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}
