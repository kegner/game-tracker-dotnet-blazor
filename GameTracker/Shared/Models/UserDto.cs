namespace GameTracker.Shared.Models
{
    public record UserDto
    {
        public long? Id { get; init; }
        public string? Username { get; init; }
        public string? Password { private get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
    }
}
