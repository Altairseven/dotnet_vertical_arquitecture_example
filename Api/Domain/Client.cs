using Api.Domain.Abstractions;

namespace Api.Domain;

public class Client : Entity<int>
{
    private Client(int clientId, string firstName, string lastName)
    {
        ClientId = clientId;
        FirstName = firstName;
        LastName = lastName;
    }

    public int ClientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public static Client NewClient(string firstName, string lastName)
    {
        return new Client(0, firstName, lastName);
    }

}
