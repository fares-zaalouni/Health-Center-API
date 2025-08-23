namespace SHC.Core.Projections;

public record FullName
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
