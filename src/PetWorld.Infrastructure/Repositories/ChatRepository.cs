using Microsoft.EntityFrameworkCore;
using PetWorld.Application.Interfaces;
using PetWorld.Domain.Entities;
using PetWorld.Infrastructure.Data;

namespace PetWorld.Infrastructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly PetWorldDbContext _context;

    public ChatRepository(PetWorldDbContext context)
    {
        _context = context;
    }

    public async Task<ChatConversation> SaveConversationAsync(ChatConversation conversation)
    {
        _context.ChatConversations.Add(conversation);
        await _context.SaveChangesAsync();
        return conversation;
    }

    public async Task<IEnumerable<ChatConversation>> GetAllConversationsAsync()
    {
        return await _context.ChatConversations
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}
