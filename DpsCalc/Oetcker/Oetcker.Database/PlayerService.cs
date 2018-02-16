using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oetcker.Models.Models;

namespace Oetcker.Data
{
    public static class PlayerService
    {
        private Player _player;
        public Player GetPlayer()
        {
            var player = XmlSerializer<Player>.GetContent("Player");
        }
    }
}
