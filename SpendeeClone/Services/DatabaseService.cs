using SQLite;
using SpendeeClone.Models;

namespace SpendeeClone.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "spendee.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transaction>().Wait();
        }

        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            return await _database.Table<Transaction>().ToListAsync();
        }

        public async Task<int> SaveTransactionAsync(Transaction transaction)
        {
            if (transaction.Id != 0)
                return await _database.UpdateAsync(transaction);
            else
                return await _database.InsertAsync(transaction);
        }

        public async Task<int> DeleteTransactionAsync(Transaction transaction)
        {
            return await _database.DeleteAsync(transaction);
        }
    }
}