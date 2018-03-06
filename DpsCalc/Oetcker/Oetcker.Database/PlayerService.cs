using System.Collections.Generic;
using Oetcker.Models.Models;

namespace Oetcker.Data
{
    public static class PlayerService
    {
        #region Staticfields and Constants

        private static List<Player> _player;

        #endregion

        #region Methods

        /// <summary>
        /// Diese Methode liefert den gespeicherten Spieler zurück
        /// </summary>
        /// <returns></returns>
        public static List<Player> GetPlayers()
        {
            _player = XmlSerializer<List<Player>>.GetContent("Players");
            return _player;
        }

        #endregion
    }
}
