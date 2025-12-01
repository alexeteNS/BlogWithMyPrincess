using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace BlogWithMyPrincess.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public List<Comments> CommentsList { get; set; } = new();
    public List<Post> PostList { get; set; } = new();

    public bool isDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

}