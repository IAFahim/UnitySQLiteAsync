using System.IO;
using SQLite;
using UnityEngine;

namespace UnitySQLiteAsync._addOn.GameDB
{
    internal static class SqlAsync
    {
        private static SQLiteAsyncConnection _connection;

        public static SQLiteAsyncConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    var path = Path.Combine(Application.persistentDataPath, "dbAsync.sqlite");
                    _connection = new SQLiteAsyncConnection(path);
                }

                return _connection;
            }
        }
    }
}