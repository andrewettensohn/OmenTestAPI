using OmenModels;
using System.Linq.Expressions;

namespace OmenTestAPI.Interfaces
{
    public interface IOmenRepository
    {
        //Get List
        Task<List<Starship>> GetStarshipListAsync();
        Task<List<ShipModule>> GetShipModuleListAsync();
        Task<List<StarshipClass>> GetStarshipClassListAsync();
        Task<List<StarshipHull>> GetStarshipHullListAsync();

        //Filter
        Task<List<Starship>> GetStarshipByFilter(Expression<Func<Starship, bool>> filter);

        //Create One
        Task<Starship> Create(Starship item);
        Task Create(ShipModule item);
        Task Create(StarshipClass item);
        Task Create(StarshipHull item);

        //Replace
        Task Replace(Starship item);
        Task Replace(ShipModule item);

        //Delete
        Task DeleteStarshipById(string id);
        Task DeleteShipModuleById(string id);
    }
}
