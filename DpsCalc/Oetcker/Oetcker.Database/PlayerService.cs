using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            using (var dbContext = new Database())
            {
                return dbContext.Players.Include("CurrentItemSet.PlayerItems").ToList();
            }
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
                _players = GetPlayers();
            _currentPlayer = _players.FirstOrDefault(p => p.Name == player.Name);
            if (null == _currentPlayer)
                return null;
            CurrentPlayerChanged?.Invoke(_currentPlayer);
            return _currentPlayer;
        }

        #endregion

        public static Player WritePlayer(Player createPlayer)
        {
            using (var dbContext = new Database())
            {
                foreach (var playerItem in createPlayer.CurrentItemSet.PlayerItems)
                {
                    dbContext.Items.Attach(playerItem);
                }
                dbContext.Players.AddOrUpdate(createPlayer);
                dbContext.SaveChanges();
                return dbContext.Players.FirstOrDefault(player => player.Name == createPlayer.Name);
            }
        }
    }
}
