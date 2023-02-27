using System.IO;
using SQLite;
using UnityEngine;

namespace UnitySQLiteAsync._addOn.SQL
{
    internal static class SqlAsync
    {
        private static SQLiteAsyncConnection _asyncConnection;

        public static SQLiteAsyncConnection AsyncConnection
        {
            get
            {
                if (_asyncConnection == null)
                {
                    var path = Path.Combine(Application.persistentDataPath, "dbAsync.sqlite");
                    _asyncConnection = new SQLiteAsyncConnection(path);
                }

                return _asyncConnection;
            }
        }
    }
}