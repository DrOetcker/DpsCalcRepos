using MySql.Data.MySqlClient;
using static System.String;

namespace Oetcker.Database
{
    public class DbConnection
    {
        #region Staticfields and Constants

        private static DbConnection _instance = null;

        #endregion

        #region Fields

        private MySqlConnection _connection = null;

        private string _databaseName = string.Empty;

        #endregion

        #region Constructors

        private DbConnection()
        {
        }

        #endregion

        #region Properties

        public MySqlConnection Connection
        {
            get { return _connection; }
        }

        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        public string Password { get; set; }

        #endregion

        #region Methods

        public void Close()
        {
            _connection.Close();
        }

        public static DbConnection Instance()
        {
            if (_instance == null)
                _instance = new DbConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection != null)
                return true;

            if (IsNullOrEmpty(_databaseName))
                return false;
            var connstring = $"Server=localhost; database={_databaseName};uid=root;pwd=root";
            _connection = new MySqlConnection(connstring);
            _connection.Open();

            return true;
        }

        #endregion
    }
}
