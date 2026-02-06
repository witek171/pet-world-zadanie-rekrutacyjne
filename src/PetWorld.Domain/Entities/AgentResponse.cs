namespace PetWorld.Domain.Entities;

public class AgentResponse
{
	public AgentResponse(string response, int iterationCount, bool isApproved)
	{
		Response = response;
		IterationCount = iterationCount;
		IsApproved = isApproved;
	}

	public string Response { get; }
	public int IterationCount { get; }
	public bool IsApproved { get; }
}