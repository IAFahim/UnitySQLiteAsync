using System.IO;
using SQLite;
using UnityEngine;

namespace UnitySQLiteAsync._addOn.SQL
{
    public static class Sql
    {
        private static SQLiteAsyncConnection _connection;

        public static SQLiteAsyncConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    var path = Path.Combine(Application.persistentDataPath, "db.sqlite");
                    _connection = new SQLiteAsyncConnection(path);
                }

                return _connection;
            }
        }
    }
}