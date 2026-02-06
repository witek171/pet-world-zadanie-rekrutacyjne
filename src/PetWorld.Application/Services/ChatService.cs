using PetWorld.Application.DTOs;
using PetWorld.Application.Interfaces;
using PetWorld.Domain.Entities;

namespace PetWorld.Application.Services;

public class ChatService
{
	private readonly IAgentService _agentService;
	private readonly IChatRepository _chatRepository;

	public ChatService(IAgentService agentService, IChatRepository chatRepository)
	{
		_agentService = agentService;
		_chatRepository = chatRepository;
	}

	public async Task<ChatResponse> ProcessChatAsync(ChatRequest request)
	{
		AgentResponse agentResponse = await _agentService.ProcessQuestionAsync(request.Question);

		ChatConversation conversation = new(
			DateTime.UtcNow,
			request.Question,
			agentResponse.Response,
			agentResponse.IterationCount);

		await _chatRepository.SaveConversationAsync(conversation);

		return new ChatResponse(
			agentResponse.Response,
			agentResponse.IterationCount,
			conversation.CreatedAt);
	}

	public async Task<IEnumerable<ChatConversation>> GetHistoryAsync()
		=> await _chatRepository.GetAllConversationsAsync();
}