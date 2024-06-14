using PetShop.Models;

namespace PetShop.Interfaces
{
    public interface IUserRepository
    {
        User Login(string email, string password);
        User GetUserById(int id);
        IEnumerable<User> GetUsers();
        User CreateUser(User user, string password);
        void UpdateUser(User user);
        void DeleteUserById(int id);
        void SetRefreshToken(User user, RefreshToken refreshToken);
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken();
        void OrderPet(int userId, int petId);

    }
}
