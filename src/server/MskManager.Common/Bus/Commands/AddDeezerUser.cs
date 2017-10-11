using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Common.Bus.Commands
{
    public class AddDeezerUser : ICommand
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
    }
}
