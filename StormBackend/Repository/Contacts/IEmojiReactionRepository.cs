using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Models;

namespace StormBackend.Repository.Contacts
{
    public interface IEmojiReactionRepository: IRepositoryBase<EmojiReaction>
    {
        void CreateEmoji(EmojiReaction emojiReaction);
        void DeleteEmoji(EmojiReaction emojiReaction);
        Task<List<EmojiReaction>> GetReactions(int messageId);
        void UpdateEmoji(EmojiReaction emojiReaction);
        Task<EmojiReaction> GetEmoji(int messageId, string userId);
    }
}