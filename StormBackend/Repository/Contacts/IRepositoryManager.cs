using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormBackend.Repository.Contacts
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        IContactRepository Contact { get; }
        IChatRepository Chat { get; }
        IGroupRepository Group { get; }
        IGroupMembershipRepository GroupMembership { get; }
        IMessageRepository Message { get; }
        IChatMembershipRepository ChatMembership { get; }
        IEmojiReactionRepository EmojiReaction { get; }
        

        Task SaveAsync();
    }
}