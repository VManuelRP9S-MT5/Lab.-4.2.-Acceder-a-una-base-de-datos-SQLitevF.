using MauiApp11.Models;
using MauiApp11.Persistence.Configuration;
using SQLite;

namespace MauiApp11.Persistence
{
    public class ShopListitemDataBase
    {
        private SQLiteAsyncConnection _database;

        public ShopListitemDataBase()
        {

        }

        private async Task Init()
        {
            if (_database is not null)
            {
                return;
            }
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            await _database.CreateTableAsync<ShopListItem>();
        }

        public async Task<int> SaveShopListItem(ShopListItem item)
        {
            await Init();
            if (item.ID != 0)
            {
                //el articulo a comprar ya existe, asi que solo es una actualizacion
                return await _database.UpdateAsync(item);
            }
            else
            {
                //es un articulo nuevo
                return await _database.InsertAsync(item);
            }
        }

        public async Task<IEnumerable<ShopListItem>> GetShopListItem()
        {
            await Init();
            return await _database.Table<ShopListItem>().ToListAsync();
        }

        public async Task<ShopListItem> GetShopListItem(int id)
        {
            await Init();
            return await _database.Table<ShopListItem>()
                .Where(item => id == item.ID)
                .FirstOrDefaultAsync();
        }
    }
}
