using BlogWithMyPrincess.Dtos.Users;
using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface IUserServices
{
    public Task<UserToken?> CreateUser(string username, string password, string email);
    public Task<UserToken?> LoginUser(string email, string password);
    public Task<UserInfo?> LoginUserWithToken(int id);
    public Task<BackUser> GetUserByEmail(string email);
    public Task<BackUser> GetUserByUId(int id);
    public Task<bool> UpdateEmail(int idUser, string email);
    public Task<bool> UpdatePassword(int idUser, string oldPassword, string newPassword);
    public Task<bool> UpdateUsername(int idUser, string newUsername);
    public Task<bool> DeleteUser(int idUser, string password);
    public Task<bool> ExistsByEmail(string email);

}