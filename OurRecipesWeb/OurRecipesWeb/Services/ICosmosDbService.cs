using System.Collections.Generic;
using System.Threading.Tasks;
using OurRecipesWeb.Models;

namespace OurRecipesWeb.Services
{
    

    public interface ICosmosDbService
    {
        Task<IEnumerable<Recipe>> GetItemsAsync(string query);

        IEnumerable<Recipe> GetAllItemsCached();
        Task<Recipe> GetItemAsync(string id);
        Task AddItemAsync(Recipe item);
        Task UpdateItemAsync(string id, Recipe item);
        Task DeleteItemAsync(string id);
    }
}
