using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StormBackend.Data;
using StormBackend.Repository.Contacts;

namespace StormBackend.Repository
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly AppDBContext _context;
        private readonly IUserRepository _user;
        private readonly IContactRepository _contact;
        private readonly IChatRepository _chat;
        private readonly IGroupRepository _group;
        private readonly IGroupMembershipRepository _groupMembership;
        private readonly IMessageRepository _message;
        private readonly IChatMembershipRepository _chatMembership;

        public RepositoryManager(AppDBContext context, 
            IUserRepository user, 
            IContactRepository contact, 
            IChatRepository chat, 
            IGroupRepository group, 
            IGroupMembershipRepository groupMembership, 
            IMessageRepository message,
            IChatMembershipRepository chatMembership
            )
        {
            _context = context;
            _user = user;
            _contact = contact;
            _chat = chat;
            _group = group;
            _groupMembership = groupMembership;
            _message = message;
            _chatMembership = chatMembership;
        }

        public IUserRepository User => _user;

        public IContactRepository Contact => _contact;

        public IChatRepository Chat => _chat;

        public IGroupRepository Group => _group;

        public IGroupMembershipRepository GroupMembership => _groupMembership;

        public IMessageRepository Message => _message;

        public IChatMembershipRepository ChatMembership { get; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}