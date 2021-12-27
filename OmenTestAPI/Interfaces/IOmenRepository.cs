using OmenModels;

namespace OmenTestAPI.Interfaces
{
    public interface IOmenRepository
    {
        //Get List
        Task<List<Starship>> GetStarshipListAsync();
        Task<List<ShipModule>> GetShipModuleListAsync();
        Task<List<StarshipClass>> GetStarshipClassListAsync();
        Task<List<StarshipHull>> GetStarshipHullListAsync();

        //Create One
        Task<Starship> Create(Starship item);
        Task Create(ShipModule item);
        Task Create(StarshipClass item);
        Task Create(StarshipHull item);

        //Replace
        Task Replace(Starship item);
    }
}
