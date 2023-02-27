using System.IO;
using SQLite;
using UnityEngine;

namespace UnitySQLiteAsync._addOn.SQL
{
    internal static class Sql
    {
        private static SQLiteConnection _connection;

        public static SQLiteConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    var path = Path.Combine(Application.persistentDataPath, "db.sqlite");
                    _connection = new SQLiteConnection(path);
                }

                return _connection;
            }
        }
    }
}