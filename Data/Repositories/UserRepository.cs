using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BlogWithMyPrincess.Interfaces;
using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BlogWithMyPrincess.Data.Repositories;

public class UserRepository : IUserRepository
{

    private readonly IBlogDbContext _context;

    public UserRepository(IBlogDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> CreateUser(User? user)
    {
        if (user == null) return null;
        
        //Agregamos a la tabla al nuebvo usuario
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> LoginUser(string email, string password)
    {
        var user = await GetUserByEmail(email);
        if (user == null) return null;
        if(user.isDeleted) return null;
        return !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) ? null : user;
    }

    public async Task<User?> LoginUserWithToken(int id)
    {
        //Verificar si el usuario existe y devolverlo
        var user = await _context.Users.FindAsync(id);
        return user?.isDeleted == false ? user : null;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return null;
        
        //Verificar si el usuario existe
        User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;
        if(user.isDeleted) return null;
        return user;
    }

    public async Task<User?> GetUserByUId(int id)
    {
        //Verificar si el usuario existe y devolverlo
        var user = await _context.Users.FindAsync(id);
        return user?.isDeleted == false ? user : null;
    }

    public async Task<bool> UpdateEmail(int idUser, string email)
    {
        var checker = new EmailChecker(email);
        if (string.IsNullOrWhiteSpace(email)) return false;
        
        if (!checker.IsValid) return false;
        
        //Verificamos que el usuario exista
        User? user = await _context.Users.FindAsync(idUser);
        if (user == null) return false;
        if(user.isDeleted) return false;
        
        //Verificamos que el nuevo email no exista con otro usuario
        bool userEmailExist =  await _context.Users.AnyAsync(u => u.Email == email && user.Id != idUser);
        if (userEmailExist) return false;
        
        //Actualizamos el email
        user.Email = email;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdatePassword(int idUser,string oldPassword ,string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword)) return false;
        
        //Verificamos que el usuario exista
        User? user = await _context.Users.FindAsync(idUser);
        if (user == null) return false;
        if(user.isDeleted) return false;
        
        //Comprobamos que le usuario puso su contraseña actual
        bool Ver = BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash);
        if (!Ver) return false;
        
        //Actualizamos la contraseña del usuario a su nueva contraseña
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUsername(int idUser, string newUsername)
    {
        if (string.IsNullOrWhiteSpace(newUsername)) return false;
        
        //Verificamos que el usuario exista
        User? user = await _context.Users.FindAsync(idUser);
        if (user == null) return false;
        if(user.isDeleted) return false;
        
        //Actualizamos a su nuevo nombre
        user.Username = newUsername;
        await _context.SaveChangesAsync();
        return true;
    }
    

    public async Task<bool> DeleteUser(int userId, string password)
    {
        //Verificamos que el usuario exista
        User? user = await _context.Users.FindAsync(userId);
        if (user == null) return false;
        if(user.isDeleted) return false;
        
        //Verificamos que es la misma contraseña
        bool isTheSamePassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!isTheSamePassword) return false;
        
        //Marcar que esta borrado
        user.isDeleted = true;
        user.DeletedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email && !a.isDeleted);
        if (user == null) return false;
        return true;
    }
}