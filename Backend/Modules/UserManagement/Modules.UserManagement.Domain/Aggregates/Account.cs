using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SharedUtils.Domain.Aggregates;

namespace Modules.UserManagement.Domain.Aggregates;
[Index(nameof(Username), IsUnique = true)]
public class Account : AggregateRoot
{
    public Account(string username, string email, string password) : base()
    {
        Username = username;
        Email = email;
        Password = password;
        AddDomainEvent(new AccountCreatedEvent(Id));
    }

    [Required]
    [MaxLength(50)]
    public string Username { get; private set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }

    [Required]
    [MaxLength(80)]
    public string Password { get; private set; }
}