using Microsoft.EntityFrameworkCore;
using OmenModels;
using OmenModels.Interfaces;
using OmenTestAPI.Interfaces;

namespace OmenTestAPI.Data
{
    public class OmenRepository : IOmenRepository
    {
        private readonly OmenContext _context;
        public OmenRepository(OmenContext context)
        {
            _context = context;
        }

        public DbSet<T> GetTable<T>() where T : class, IGuidId => _context.Set<T>();

        public void AddBaseModel<T>(T model) where T : class, IGuidId
        {
            GetTable<T>().Add(model);
            _context.SaveChanges();
        }

        public void UpdateBaseModel<T>(T model) where T : class, IGuidId
        {
            GetTable<T>().Update(model);
            _context.SaveChanges();
        }

        public List<T> GetBaseModelList<T>() where T : class, IGuidId => GetTable<T>().ToList();

        public List<Starship> GetStarshipList()
        {
            return _context.Starships.Include(x => x.Modules).Include(x => x.StarshipClass).Include(x => x.Hull).ToList();
        }
    }
}
