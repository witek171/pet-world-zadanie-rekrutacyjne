using PetWorld.Domain.Entities;

namespace PetWorld.Application.Interfaces;

public interface IAgentService
{
	Task<AgentResponse> ProcessQuestionAsync(string question);
}