using Api.Common.Abstractions;

namespace Api.Features.Clients.V2.CreateClient;

public partial class CreateClientCommand : ICommand<CreateClientResponse>
{
    public CreateClientCommand(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }

}

