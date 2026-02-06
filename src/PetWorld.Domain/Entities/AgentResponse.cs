namespace PetWorld.Domain.Entities;

public class AgentResponse
{
    public string Response { get; set; } = string.Empty;
    public int IterationCount { get; set; }
    public bool IsApproved { get; set; }
}
