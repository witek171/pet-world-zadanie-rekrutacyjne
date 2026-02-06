namespace PetWorld.Domain.Entities;

public class CriticFeedback
{
	public CriticFeedback(bool approved, string feedback)
	{
		Approved = approved;
		Feedback = feedback;
	}

	public bool Approved { get; }
	public string Feedback { get; }
}