namespace PetWorld.Domain.Entities;

public class CriticFeedback
{
    public bool Approved { get; set; }
    public string Feedback { get; set; } = string.Empty;
}
