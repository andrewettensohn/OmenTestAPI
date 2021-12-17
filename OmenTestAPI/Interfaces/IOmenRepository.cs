using OmenModels;
using OmenModels.Interfaces;

namespace OmenTestAPI.Interfaces
{
    public interface IOmenRepository
    {
        void AddBaseModel<T>(T model) where T : class, IGuidId;

        List<T> GetBaseModelList<T>() where T : class, IGuidId;

        void UpdateBaseModel<T>(T model) where T : class, IGuidId;

        List<Starship> GetStarshipList();
    }
}
