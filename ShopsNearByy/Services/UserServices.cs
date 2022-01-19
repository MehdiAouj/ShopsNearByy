using MongoDB.Driver;
using ShopsNearByy.Models;
using Microsoft.Extensions.Options;

namespace ShopsNearByy.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMongoCollection<UserModel> _userCollection;

        public UserServices(ShopsNearDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _userCollection =  database.GetCollection<UserModel>(settings.UserCollectionName);
        }
        public UserModel Create(UserModel userModel)
        {
            _userCollection.InsertOne(userModel);
            return userModel;
        }

        public void Delete(int id)
        {
             _userCollection.DeleteOne(userModel => userModel.Id == id);
        }

        public UserModel Get(int id)
        {
            return _userCollection.Find(userModel => userModel.Id == id).FirstOrDefault();
        }

        public object Get(string email)
        {
            return _userCollection.Find(userModel => userModel.Email == email).FirstOrDefault();
        }

        public void Update(int id, UserModel userModel)
        {
            _userCollection.ReplaceOne(userModel => userModel.Id == id, userModel);
        }
    }
}
