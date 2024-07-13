using Api.Common.Abstractions;

namespace Api.Features.Clients.V1.CreateClient;

public partial class AddClientCommand : ICommand<AddClientResponse>
{
    public AddClientCommand(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }

}

