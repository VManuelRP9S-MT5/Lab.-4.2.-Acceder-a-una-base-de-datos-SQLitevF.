using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp11.Models;
using MauiApp11.Persistence;
using System.Collections.ObjectModel;

namespace MauiApp11.ViewModels
{
    public partial class MainViewModels : ObservableObject
    {
        [ObservableProperty]
        private string _itemName;

        [ObservableProperty]
        public ObservableCollection<ShopListItem> _ShopList;

        private ShopListitemDataBase _database;

        public MainViewModels()
        {
            _database = new ShopListitemDataBase();
            LoadShopListAsync();
        }

        [RelayCommand]
        private async Task Add()
        {
            if (string.IsNullOrEmpty(ItemName))
            {
                return;
            }

            ShopListItem item = new ShopListItem
            {
                Name = ItemName,
                IsBought = false
            };

            await _database.SaveShopListItem(item);

            ShopList.Add(item);
        }

        private async void LoadShopListAsync()
        {
            var items = await _database.GetShopListItem();
            if (items is not null && items.Any<ShopListItem>())
            {
                ShopList = new ObservableCollection<ShopListItem>();
                foreach (var item in items)
                {
                    ShopList.Add(item);
                }
            }

        }
    }
}
