using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace MskManager.Common.Bus.Commands
{
    public class AddSong : ICommand
    {
        public string Title { get; set; }
        public string Artist { get; set; }
    }
}
