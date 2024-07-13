namespace Api.Features.Clients.V2.CreateClient;

public partial class CreateClientResponse
{
    public CreateClientResponse(Guid clientId, string firstName, string lastName)
    {
        ClientId = clientId;
        FirstName = firstName;
        LastName = lastName;
    }

    public Guid ClientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


