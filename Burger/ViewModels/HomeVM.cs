namespace Burger.ViewModels
{
    public class HomeVM
    {
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Meal>? Meals { get; set; }
        public ReservationViewModel? ReservationViewModel { get; set; }
    }
}
