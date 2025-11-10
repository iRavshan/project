namespace UserService.Domain.Entities;

public class Organization
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public string? Address { get; set; } = String.Empty;

    public User? CreatedBy { get; set; } = null!;
    public ICollection<User?> Admins { get; set; } = new List<User>();
}