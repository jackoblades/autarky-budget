using AutarkyBudget.Models;
using AutarkyBudget.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Xamarin.Essentials;

namespace AutarkyBudget.Services
{
    public abstract class SqliteService : ISqliteService
    {
        #region Properties

        private static readonly object _lock = new object();
        private SQLiteConnection _connection;

        #endregion

        #region Constructors

        protected SqliteService()
        {
            if (_connection != null) return;

            Init();
        }

        #endregion

        #region Methods

        public abstract void InitPlatform();

        public abstract string GetPlatformDatabasePath(string databaseName);

        public abstract bool IsDatabaseAlive(string filepath);

        public abstract void DeleteDatabaseFromPlatform(string filepath);

        private void Init()
        {
            // Initialize platform specific code.
            InitPlatform();

            // Encrypt/Decrypt database.
            string key = RetrieveEncryptionKey();

            // Create/Open database.
            string filepath = GetPlatformDatabasePath("app.db");
            bool isNew = !IsDatabaseAlive(filepath);

            try // Connect and test database encryption. Ensure key is valid.
            {
                // Connect to database.
                _connection = BeginConnection(filepath, key);

                // This will try to query the SQLite schema database, if the key is correct then no exception is thrown.
                _connection.Query<int>("SELECT count(*) FROM sqlite_master");
            }
            catch (SQLiteException)
            {
                // If key is invalid for some reason, close the connection, and delete the database file.
                _connection?.Dispose();
                _connection = null;
                DeleteDatabaseFromPlatform(filepath);

                // Attempt to re-create database.
                _connection = BeginConnection(filepath, key);
            }
            finally
            {
                if (isNew) CreateTables();
            }
        }

        private SQLiteConnection BeginConnection(string filepath, string key)
        {
            var connectionString = new SQLiteConnectionString(filepath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create, true, key);
            return new SQLiteConnection(connectionString);
        }

        private void CreateTables(bool transactionStarted = false)
        {
            // If transaction isn't started, begin a new one.
            if (!transactionStarted)
            {
                _connection.BeginTransaction();
            }

            // Create tables.
            _connection.CreateTable<Item>();

            // Commit transaction.
            _connection.Commit();
        }

        private string RetrieveEncryptionKey()
        {
            // Retrieve pass-phrase if possible.
            string pass = SecureStorage.GetAsync("sql_cipher").Result;

            // If pass-phrase doesn't exist;
            if (string.IsNullOrWhiteSpace(pass))
            {
                // Create new pass-phrase.
                byte[] bytes = new byte[8];
                using (var provider = new RNGCryptoServiceProvider())
                {
                    provider.GetBytes(bytes);
                }
                pass = $"cipher_{BitConverter.ToUInt64(bytes, 0)}";
                SecureStorage.SetAsync("sql_cipher", pass);
            }

            return pass;
        }

        #region ISqliteService

        public IList<T> ReadList<T>() where T : new()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Table<T>().ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public IList<T> ReadList<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Table<T>().Where(predicate).ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public IList<T> ReadOrderedList<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> ordering) where T : new()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Table<T>().Where(predicate).OrderBy(ordering).ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public T Get<T>(object primaryKey) where T : new()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Get<T>(primaryKey);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public T ReadFirst<T>() where T : new()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Table<T>().FirstOrDefault();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public T ReadFirst<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Table<T>().Where(predicate).FirstOrDefault();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public int Insert(object value, Type type)
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Insert(value, type);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public int Update(object value, Type type)
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Update(value, type);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public int Upsert(object value, Type type)
        {
            lock (_lock)
            {
                try
                {
                    return _connection.InsertOrReplace(value, type);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public bool UpsertList<T>(List<T> values)
        {
            Type type = typeof(T);

            lock (_lock)
            {
                // Begin transaction.
                _connection.BeginTransaction();

                try // Insert all.
                {
                    foreach (T value in values)
                        _connection.InsertOrReplace(value, type);

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return false;
                }
                finally // Commit transaction.
                {
                    _connection.Commit();
                }
            }
        }

        public bool Delete<T>(object value)
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Delete<T>(value) > 0;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return false;
            }
        }

        public bool DeleteAll<T>()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.DeleteAll<T>() > 0;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

                return false;
            }
        }

        public int Count<T>() where T : new()
        {
            lock (_lock)
            {
                try
                {
                    return _connection.Table<T>().Count();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    return default;
                }
            }
        }

        public void Reset()
        {
            lock (_lock)
            {
                // Begin transaction.
                _connection.BeginTransaction();

                // Drop all tables.
                _connection.DropTable<Item>();

                // Recreate all tables.
                CreateTables(true);
            }
        }


        #endregion

        #endregion
    }
}
