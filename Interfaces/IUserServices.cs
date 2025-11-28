using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface IUserServices
{
    public Task<bool> CreateUser(string username, string password, string email);
    public Task<User?> GetUserByEmail(string email);
    public Task<User?> GetUserByUId(int id);
    public Task<bool> UpdateEmail(int idUser, string email);
    public Task<bool> UpdatePassword(int idUser, string oldPassword, string newPassword);
    public Task<bool> UpdateUsername(int idUser, string newUsername);
    public Task<bool> DeleteUser(int idUser, string password);
    public Task<bool> ExistsByEmail(string email);

}