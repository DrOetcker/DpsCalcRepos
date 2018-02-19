using Oetcker.Models.Models;

namespace Oetcker.Data
{
    public static class PlayerService
    {
        #region Staticfields and Constants

        private static Player _player;

        #endregion

        #region Methods

        /// <summary>
        /// Diese Methode liefert den gespeicherten Spieler zurück
        /// </summary>
        /// <returns></returns>
        public static Player GetPlayer()
        {
            _player = XmlSerializer<Player>.GetContent("Player");
            return _player;
        }

        #endregion
    }
}
