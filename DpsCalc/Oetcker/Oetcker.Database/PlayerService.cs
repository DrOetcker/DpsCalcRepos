using System;
using System.Collections.Generic;
using System.Linq;
using Oetcker.Models.Models;

namespace Oetcker.Data
{
    public static class PlayerService
    {
        #region Staticfields and Constants

        private static List<Player> _players;
        private static Player _currentPlayer;

        #endregion

        #region Properties

        public static Action<Player> CurrentPlayerChanged { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Diese Methode liefert den aktuell geladenen Spieler zurück
        /// </summary>
        /// <returns></returns>
        public static Player GetCurrentPlayer()
        {
            return _currentPlayer ?? GetPlayers()?.FirstOrDefault();
        }

        /// <summary>
        /// Diese Methode liefert den gespeicherten Spieler zurück
        /// </summary>
        /// <returns></returns>
        public static List<Player> GetPlayers()
        {
            _players = XmlSerializer<List<Player>>.GetContent("Players");
            return _players;
        }

        public static void ResetCache()
        {
            _currentPlayer = null;
            _players = null;
        }

        /// <summary>
        /// Diese Methode liefert den aktuell geladenen Spieler zurück
        /// </summary>
        /// <returns></returns>
        public static Player SetCurrentPlayer(Player player)
        {
            if (null == _players)
                _players = XmlSerializer<List<Player>>.GetContent("Players");
            _currentPlayer = _players.FirstOrDefault(p => p.Name == player.Name);
            if (null == _currentPlayer)
                return null;
            CurrentPlayerChanged?.Invoke(_currentPlayer);
            return _currentPlayer;
        }

        #endregion
    }
}
