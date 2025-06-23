

using BlazorTecnicalTest.Interface;
using BlazorTecnicalTest.Models.Task;
using MongoDB.Driver;

namespace BlazorTecnicalTest.Repositories
{
    public class TaskRepositorie : ITaskService
    {
        private readonly IMongoCollection<TaskItem> _taskCollection;

        public TaskRepositorie(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDb:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDb:Database"]);
            _taskCollection = database.GetCollection<TaskItem>("tasks");
        }

        public async Task<List<TaskItem>> GetAllAsync() =>
            await _taskCollection.Find(_ => true).ToListAsync();

        public async Task<TaskItem?> GetByIdAsync(string id) =>
            await _taskCollection.Find(t => t.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TaskItem task) =>
            await _taskCollection.InsertOneAsync(task);

        public async Task UpdateAsync(string id, TaskItem task) =>
            await _taskCollection.ReplaceOneAsync(t => t.Id == id, task);

        public async Task DeleteAsync(string id) =>
            await _taskCollection.DeleteOneAsync(t => t.Id == id);
    }
}
