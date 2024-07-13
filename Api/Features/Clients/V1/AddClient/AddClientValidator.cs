using FluentValidation;

namespace Api.Features.Clients.V1.CreateClient;

public class AddClientValidator : AbstractValidator<AddClientCommand>
{
    public AddClientValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
