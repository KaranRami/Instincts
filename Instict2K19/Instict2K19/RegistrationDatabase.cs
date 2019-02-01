using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Instict2K19.DataModel;
using SQLite;

namespace Instict2K19
{
    public class RegistrationDatabase
    {
        readonly SQLiteAsyncConnection database;
        public readonly string Path;
        public RegistrationDatabase(string dbPath)
        {
            Path = dbPath;
               database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<RegisterModel>().Wait();
        }

        public Task<List<RegisterModel>> GetItemsAsync()
        {
            return database.Table<RegisterModel>().ToListAsync();
        }
        public Task<double> GetTotalAsync()
        {
            return database.ExecuteScalarAsync<double>("SELECT SUM(FeesCharged) FROM [RegisterModel]");
        }
        public Task<List<RegisterModel>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<RegisterModel>("SELECT * FROM [RegisterModel] WHERE [Done] = 0");
        }

        public Task<RegisterModel> GetItemAsync(string id)
        {
            return database.Table<RegisterModel>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(RegisterModel item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(RegisterModel item)
        {
            return database.DeleteAsync(item);
        }
    }
}
