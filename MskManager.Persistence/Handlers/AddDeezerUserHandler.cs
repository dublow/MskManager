using System;
using System.Threading.Tasks;
using MskManager.Common.Bus.Commands;
using MskManager.Persistence.Model;
using MskManager.Persistence.Repositories;
using NServiceBus;

namespace MskManager.Persistence.Handlers
{
    public class AddDeezerUserHandler : IHandleMessages<AddDeezerUser>
    {
        private readonly IDeezerUserRepository _deezerUserRepository;

        public AddDeezerUserHandler(IDeezerUserRepository deezerUserRepository)
        {
            _deezerUserRepository = deezerUserRepository;
        }

        public Task Handle(AddDeezerUser message, IMessageHandlerContext context)
        {
            if(message == null)
                throw new ArgumentNullException(nameof(message));

            if (!_deezerUserRepository.Exists(message.Id))
            {
                var deezerUser = new DeezerUser(message.Id, message.AccessToken);
                _deezerUserRepository.Add(deezerUser);
            }
                
            return Task.CompletedTask;
        }
    }
}
