namespace PetWorld.Domain.Entities;

public class ChatConversation
{
	public ChatConversation(DateTime createdAt, string question, string answer, int iterationCount)
	{
		CreatedAt = createdAt;
		Question = question;
		Answer = answer;
		IterationCount = iterationCount;
	}

	public Guid Id { get; } = Guid.NewGuid();
	public DateTime CreatedAt { get; }
	public string Question { get; }
	public string Answer { get; }
	public int IterationCount { get; }
}