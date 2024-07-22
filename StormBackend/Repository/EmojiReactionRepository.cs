using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StormBackend.Data;
using StormBackend.Models;
using StormBackend.Repository.Contacts;

namespace StormBackend.Repository
{
    public class EmojiReactionRepository: RepositoryBase<EmojiReaction>, IEmojiReactionRepository
    {
        public EmojiReactionRepository(AppDBContext context) : base(context)
        {
        }

        public void CreateEmoji(EmojiReaction emojiReaction)
        {
            Create(emojiReaction);
        }

        public void DeleteEmoji(EmojiReaction emojiReaction)
        {
            Delete(emojiReaction);
        }

        public Task<EmojiReaction> GetEmoji(int messageId, string userId)
        {
            var result = FindByCondition(e => e.MessageId == messageId && e.UserId == userId, false)
                .FirstOrDefaultAsync();
            return result;
        }

        public Task<List<EmojiReaction>> GetReactions(int messageId)
        {
            var result = FindByCondition(e => e.MessageId == messageId, false)
                .ToListAsync();
            return result;
        }

        public void UpdateEmoji(EmojiReaction emojiReaction)
        {
            Update(emojiReaction);
        }
    }
}