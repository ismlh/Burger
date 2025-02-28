namespace Burger.Services
{
    public interface IReservationsService:IRepository<Reservations>
    {
        Task<IEnumerable<Reservations>> ReservationWithUser();
    }
}
