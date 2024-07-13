namespace Api.Features.Clients.V1.CreateClient;

public partial class AddClientResponse
{
    public AddClientResponse(int clientId, string firstName, string lastName)
    {
        ClientId = clientId;
        FirstName = firstName;
        LastName = lastName;
    }

    public int ClientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


