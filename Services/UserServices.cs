using BlogWithMyPrincess.Dtos.Users;
using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Interfaces;
using BlogWithMyPrincess.Helpers;

namespace BlogWithMyPrincess.Services;

public class UserServices : IUserServices
{
    
    private readonly IUserRepository _userRepository;
    private readonly ICommentsServices _commentsServices;
    private readonly IPostRepository _postRepository;

    public UserServices(IUserRepository userRepository,  ICommentsServices commentsServices,  IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _commentsServices = commentsServices;
        _postRepository = postRepository;
    }
    
    public async Task<UserToken?> CreateUser(string username, string password, string email)
    {
        //Verificamos que no haga falta ningun dato
        if(string.IsNullOrWhiteSpace(username)||  
           string.IsNullOrWhiteSpace(password)||
           string.IsNullOrWhiteSpace(email)) return null;
        
        //Verificamos el correo sea validoo
        var checker = new EmailChecker(email);

        //Verificamos que no exista el correo en otra cuenta
        if (!checker.IsValid)  return null;
        if (await _userRepository.ExistsByEmail(email))
            return null;
        
        //Creamos al usuario
        User newUser = new User();
        newUser.Username = username;
        newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        newUser.Email = checker.Email;
        
        //Lo aguardamos en la base de datos
        var user = await _userRepository.CreateUser(newUser);   
        if (user == null) return null;
        var token = GenerateJwtToken.Generate(user);

        return new UserToken
        {
            Token = token,
            User = new UserInfo
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email
            }
        };
    }

    public async Task<UserToken?> LoginUser(string email, string password)
    {
        //Verificamos que no haga falta ningun dato
        if(string.IsNullOrWhiteSpace(password)||
           string.IsNullOrWhiteSpace(email)) return null;
        var user = await _userRepository.LoginUser(email, password);
        if (user == null) return null;

        var token = GenerateJwtToken.Generate(user);

        return new UserToken
        {
            Token = token,
            User = new UserInfo
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email
            }
        };

    }

    public async Task<UserInfo?> LoginUserWithToken(int id)
    {
        var user = await _userRepository.LoginUserWithToken(id);
        if (user == null) return null;
        
        var token = GenerateJwtToken.Generate(user);
        
        return new 
            UserInfo
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email
            };
    }

    public async Task<BackUser?> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null) return null;

        var comment = await _commentsServices.GetCommentByUserId(user.Id);
        var post = await _postRepository.GetAllPostsByAuthor(user.Id);
        
        BackUser bUser = new BackUser
        {
            userId =  user.Id,
            email = user.Email,
            username = user.Username,
            comments = comment.Select(p => new BackComments{
                Id = p.Id,
                commentId = p.commentId,
                postId = p.postId,
                Content = p.Text,
                ImageUrl = p.ImageUrl,
                Time = p.DateCreated
            }).ToList(),
            posts = post.Select(p => new BackPost
            {
                Id = p.Id,
                Content = p.Text,
                ImageUrl =  p.ImageUrl,
                Time = p.DateCreated
            }).ToList()
        };
        
        return bUser;
    }

    public async Task<BackUser?> GetUserByUId(int id)
    {
        var user = await _userRepository.GetUserByUId(id);
        if (user == null) return null;

        var comment = await _commentsServices.GetCommentByUserId(id);
        var post = await _postRepository.GetAllPostsByAuthor(id);
        
        BackUser bUser = new BackUser
        {
            userId = user.Id,
            email = user.Email,
            username = user.Username,
            comments = comment.Select(p => new BackComments{
                Id = p.Id,
                commentId = p.commentId,
                postId = p.postId,
                Content = p.Text,
                ImageUrl = p.ImageUrl,
                Time = p.DateCreated
            }).ToList(),
            posts = post.Select(p => new BackPost
            {
                Id = p.Id,
                Content = p.Text,
                ImageUrl =  p.ImageUrl,
                Time = p.DateCreated
            }).ToList()
        };
        
        return bUser;
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