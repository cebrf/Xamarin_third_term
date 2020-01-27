using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Models;

namespace SQLite.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Events>().Wait();
            _database.CreateTableAsync<Users>().Wait();
        }

        public Task<List<Events>> GetEventsAsync()
        {
            return _database.Table<Events>().ToListAsync();
        }
        public Task<int> SaveEventsAsync(Events events)
        {
            return _database.InsertAsync(events);
        }
    }
}