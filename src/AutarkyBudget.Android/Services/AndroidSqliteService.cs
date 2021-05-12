using AutarkyBudget.Services;
using SQLitePCL;
using System;
using System.IO;

namespace AutarkyBudget.Droid.Services
{
    public class AndroidSqliteService : SqliteService
    {
        #region Methods

        public override void InitPlatform()
        {
            raw.SetProvider(new SQLite3Provider_e_sqlcipher());
            raw.FreezeProvider();
        }

        public override string GetPlatformDatabasePath(string databaseName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, databaseName);
        }

        public override bool IsDatabaseAlive(string filepath)
        {
            return File.Exists(filepath);
        }

        public override void DeleteDatabaseFromPlatform(string filepath)
        {
            File.Delete(filepath);
        }

        #endregion
    }
}
