using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Interfaces;
using BlogWithMyPrincess.Helpers;

namespace BlogWithMyPrincess.Services;

public class UserServices : IUserServices
{
    
    private readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<bool> CreateUser(string username, string password, string email)
    {
        //Verificamos que no haga falta ningun dato
        if(string.IsNullOrWhiteSpace(username)||  
           string.IsNullOrWhiteSpace(password)||
           string.IsNullOrWhiteSpace(email)) return false;
        
        //Verificamos el correo sea validoo
        var checker = new EmailChecker(email);

        //Verificamos que no exista el correo en otra cuenta
        if (!checker.IsValid)  return false;
        if (await _userRepository.ExistsByEmail(email))
            return false;
        
        //Creamos al usuario
        User newUser = new User();
        newUser.Username = username;
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        newUser.Email = checker.Email;
        
        //Lo aguardamos en la base de datos
        return await _userRepository.CreateUser(newUser);   
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await  _userRepository.GetUserByEmail(email);
    }

    public async Task<User?> GetUserByUId(int id)
    {
        return await _userRepository.GetUserByUId(id);
    }

    public async Task<bool> UpdateEmail(int idUser, string email)
    {
        return await _userRepository.UpdateEmail(idUser, email);
    }

    public async Task<bool> UpdatePassword(int idUser, string oldPassword,  string newPassword)
    {
        return await _userRepository.UpdatePassword(idUser, oldPassword, newPassword);
    }

    public async Task<bool> UpdateUsername(int idUser, string newUsername)
    {
        return await _userRepository.UpdateUsername(idUser, newUsername);
    }

    public async Task<bool> DeleteUser(int id, string password)
    {
        return await _userRepository.DeleteUser(id,  password);
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _userRepository.ExistsByEmail(email);
    }
}