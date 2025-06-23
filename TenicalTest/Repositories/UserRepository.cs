using BlazorTecnicalTest.Data;
using BlazorTecnicalTest.Models.Task;
using BlazorTecnicalTest.Models.User;
using MongoDB.Driver;

namespace BlazorTecnicalTest.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UserRepository(MongoDbContext context)
        {
            _usuarios = context.DataBase.GetCollection<Usuario>("usuarios");
        }

        public async Task<bool> ExistByEmail(string email)
        {
            return await _usuarios.Find(u => u.Email == email).AnyAsync();
        }

        public async Task CrearAsync(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task<List<Usuario>> GetAllAsync() =>
            await _usuarios.Find(_ => true).ToListAsync();
    }
}
