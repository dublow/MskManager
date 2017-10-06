using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MskManager.Common.Bus.Commands;
using NServiceBus;

namespace MskManager.Deezer.Handlers
{
    public class AddSongHandler : IHandleMessages<AddSong>
    {
        public Task Handle(AddSong message, IMessageHandlerContext context)
        {
            Console.Out.WriteLineAsync($"message received {message.Artist} {message.Title}");
            return Task.CompletedTask;
        }
    }
}
