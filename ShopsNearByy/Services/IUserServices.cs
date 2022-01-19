using ShopsNearByy.Models;

namespace ShopsNearByy.Services
{
    public interface IUserServices
    {
        UserModel Get(int id);
        UserModel Create(UserModel userModel);
        public void Update(int id, UserModel userModel);
        public void Delete(int id);
        object Get(string id);
    }
}
