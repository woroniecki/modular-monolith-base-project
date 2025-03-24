using FluentValidation;

namespace Modules.UserManagement.App.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        // Add validation rules here
    }
}
