using System.Threading.Tasks;

namespace ScheduleMaster.Services.Interfaces
{
    public interface IDataService
    {
        void Save<T>(T obj, string path);
        Task SaveAsync<T>(T obj, string path);

        T Load<T>(string path);
        Task<T> LoadAsync<T>(string path);

    }
}