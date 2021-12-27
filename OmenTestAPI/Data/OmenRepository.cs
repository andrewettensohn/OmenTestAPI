using MongoDB.Driver;
using OmenModels;
using OmenTestAPI.Interfaces;

namespace OmenTestAPI.Data
{
    public class OmenRepository : IOmenRepository
    {
        private readonly IMongoCollection<Starship> _starshipCollection;
        private readonly IMongoCollection<ShipModule> _moduleCollection;
        private readonly IMongoCollection<StarshipHull> _starshipHullCollection;
        private readonly IMongoCollection<StarshipClass> _starshipClassCollection;

        private readonly ReplaceOptions _replaceOptions = new ReplaceOptions { IsUpsert = true };

        public OmenRepository()
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString("mongodb+srv://test-user:msA1JZRmi9Y27IIB@cluster0.1umer.mongodb.net/test?retryWrites=true&w=majority");
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
    }
}
