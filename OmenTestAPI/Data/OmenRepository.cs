using MongoDB.Driver;
using OmenModels;
using OmenTestAPI.Interfaces;
using System.Linq.Expressions;

namespace OmenTestAPI.Data
{
    public class OmenRepository : IOmenRepository
    {
        private readonly IMongoCollection<Starship> _starshipCollection;
        private readonly IMongoCollection<ShipModule> _moduleCollection;
        private readonly IMongoCollection<StarshipHull> _starshipHullCollection;
        private readonly IMongoCollection<StarshipClass> _starshipClassCollection;

        private readonly ReplaceOptions _replaceOptions = new ReplaceOptions { IsUpsert = true };

        public OmenRepository(IConfiguration config)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(config["Global:ConnectionStrings:OmenTestDb"]);
            MongoClient client = new MongoClient(settings);
            IMongoDatabase database = client.GetDatabase("test");

            _starshipCollection = database.GetCollection<Starship>(nameof(Starship));
            _moduleCollection = database.GetCollection<ShipModule>(nameof(ShipModule));
            _starshipHullCollection = database.GetCollection<StarshipHull>(nameof(StarshipHull));
            _starshipClassCollection = database.GetCollection<StarshipClass>(nameof(StarshipClass));
        }

        //Get List
        public async Task<List<Starship>> GetStarshipListAsync() => await _starshipCollection.Find(_ => true).ToListAsync();
        public async Task<List<ShipModule>> GetShipModuleListAsync() => await _moduleCollection.Find(_ => true).ToListAsync();
        public async Task<List<StarshipClass>> GetStarshipClassListAsync() => await _starshipClassCollection.Find(_ => true).ToListAsync();
        public async Task<List<StarshipHull>> GetStarshipHullListAsync() => await _starshipHullCollection.Find(_ => true).ToListAsync();

        //Get by filter
        public async Task<List<Starship>> GetStarshipByFilter(Expression<Func<Starship, bool>> filter) => await _starshipCollection.Find(filter).ToListAsync();

        //Create One
        public async Task<Starship> Create(Starship item)
        {
            await _starshipCollection.InsertOneAsync(item);
            return item;
        }

        public async Task Create(ShipModule item) => await _moduleCollection.InsertOneAsync(item);
        public async Task Create(StarshipClass item) => await _starshipClassCollection.InsertOneAsync(item);
        public async Task Create(StarshipHull item) => await _starshipHullCollection.InsertOneAsync(item);

        //Replace
        public async Task Replace(Starship item) => await _starshipCollection.ReplaceOneAsync(x => x.Id == item.Id, item, _replaceOptions);
        public async Task Replace(ShipModule item) => await _moduleCollection.ReplaceOneAsync(x => x.Id == item.Id, item, _replaceOptions);

        //Delete
        public async Task DeleteStarshipById(string id) => await _starshipCollection.DeleteOneAsync(x => x.Id == id);
        public async Task DeleteShipModuleById(string id) => await _moduleCollection.DeleteOneAsync(x => x.Id == id);
    }
}
