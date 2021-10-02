using Auth.Application.Exceptions;
using Auth.Application.Ports.Repositories;
using Auth.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Repositories.MongoDB
{
    public class AuthRepository : IAuthRepository
    {
        private const string _usersCollectionName = "Users";

        private readonly IOptions<MongoDbSettings> _settings;
        public AuthRepository(IOptions<MongoDbSettings> settings)
        {
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            _settings = settings;
        }

        public async Task<User> GetUserByUserId(Guid userId)
        {
            var client = new MongoClient(_settings.Value.ConnectionString);

            var database = client.GetDatabase(_settings.Value.DatabaseName);

            var filter = new FilterDefinitionBuilder<User>().Where(u => u.Id == userId);

            var result = (await database.GetCollection<User>(_usersCollectionName).FindAsync(filter)).SingleOrDefault();

            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var client = new MongoClient(_settings.Value.ConnectionString);

            var database = client.GetDatabase(_settings.Value.DatabaseName);

            var filter = new FilterDefinitionBuilder<User>().Where(u => u.Email == email);

            var result = (await database.GetCollection<User>(_usersCollectionName).FindAsync(filter)).SingleOrDefault();

            return result;
        }

        public async Task UpdateUser(User user)
        {
            var client = new MongoClient(_settings.Value.ConnectionString);

            var database = client.GetDatabase(_settings.Value.DatabaseName);

            var filter = new FilterDefinitionBuilder<User>().Where(u => u.Id == user.Id);

            user.UpdateDate = DateTime.UtcNow;

            var result = await database.GetCollection<User>(_usersCollectionName).ReplaceOneAsync(filter, user);

            if (!result.IsAcknowledged)
                throw new UpdateUserException("User could not be updated. Result is not acknowledged");
        }

        public async Task CreateUser(User user)
        {
            var client = new MongoClient(_settings.Value.ConnectionString);

            var database = client.GetDatabase(_settings.Value.DatabaseName);

            var filter = new FilterDefinitionBuilder<User>().Where(u => u.Id == user.Id);

            await database.GetCollection<User>(_usersCollectionName).InsertOneAsync(user);
        }
    }
}
