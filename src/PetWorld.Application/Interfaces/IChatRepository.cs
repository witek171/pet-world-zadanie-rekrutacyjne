using PetWorld.Domain.Entities;

namespace PetWorld.Application.Interfaces;

public interface IChatRepository
{
    Task<ChatConversation> SaveConversationAsync(ChatConversation conversation);
    Task<IEnumerable<ChatConversation>> GetAllConversationsAsync();
}
