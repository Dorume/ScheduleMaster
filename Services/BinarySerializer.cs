using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ScheduleMaster.Services.Interfaces;

namespace ScheduleMaster.Services
{
    internal class BinarySerializer : IDataService
    {
        public void Save<T>(T obj, string path)
        {
            using Stream stream = File.Open(path, FileMode.Create);
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, path);
        }

        public async Task SaveAsync<T>(T obj, string path)
        {
            await using Stream stream = File.Open(path, FileMode.Create);
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, path);
        }

        public T Load<T>(string path)
        {
            using Stream stream = File.Open(path, FileMode.OpenOrCreate);
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }

        public async Task<T> LoadAsync<T>(string path)
        {
            await using Stream stream = File.Open(path, FileMode.OpenOrCreate);
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
}
