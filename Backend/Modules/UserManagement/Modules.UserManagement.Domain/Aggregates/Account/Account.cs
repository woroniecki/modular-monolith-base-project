﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Modules.UserManagement.Domain.Aggregates.Account.Entities;
using SharedUtils.Domain.Aggregates;

namespace Modules.UserManagement.Domain.Aggregates.Account;
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

    public List<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    public void AddRefreshToken(string token, DateTime expiryDate, string device, string ipAddress)
    {
        RefreshTokens.Add(new RefreshToken(token, expiryDate, device, ipAddress));
    }

    public void RevokeRefreshToken(string hashedToken)
    {
        var refreshToken = RefreshTokens.FirstOrDefault(rt => rt.HashedToken == hashedToken);
        if (refreshToken != null)
        {
            refreshToken.Revoke();
        }
    }

    public void RemoveExpiredOrUsedTokens()
    {
        RefreshTokens.RemoveAll(rt => rt.IsExpired() || rt.IsRevoked);
    }
}