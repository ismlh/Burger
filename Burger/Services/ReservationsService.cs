

namespace Burger.Services
{
    public class ReservationsService : Repository<Reservations>, IReservationsService
    {
        private readonly ApplicationDbContext _dbContext;

        public ReservationsService(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Reservations>> ReservationWithUser()
        {
            return await _dbContext.Reservations.Include(r => r.User).ToListAsync();
        }
    }
}
