using FluentValidation;

namespace Modules.UserManagement.App.Commands.Login;
public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        // Add validation rules here
    }
}
