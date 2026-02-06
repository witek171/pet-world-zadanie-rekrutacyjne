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
        var agentResponse = await _agentService.ProcessQuestionAsync(request.Question);
        
        var conversation = new ChatConversation
        {
            Question = request.Question,
            Answer = agentResponse.Response,
            IterationCount = agentResponse.IterationCount,
            CreatedAt = DateTime.UtcNow
        };
        
        await _chatRepository.SaveConversationAsync(conversation);
        
        return new ChatResponse(
            agentResponse.Response,
            agentResponse.IterationCount,
            conversation.CreatedAt
        );
    }

    public async Task<IEnumerable<ChatConversation>> GetHistoryAsync()
    {
        return await _chatRepository.GetAllConversationsAsync();
    }
}
