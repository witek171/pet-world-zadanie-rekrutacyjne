namespace PetWorld.Domain.Entities;

public class ChatConversation
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int IterationCount { get; set; }
}
