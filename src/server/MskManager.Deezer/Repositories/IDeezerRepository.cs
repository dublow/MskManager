using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MskManager.Deezer.Repositories
{
    public interface IDeezerRepository
    {
        object AddPlaylist(string userId, string title);
    }
}
