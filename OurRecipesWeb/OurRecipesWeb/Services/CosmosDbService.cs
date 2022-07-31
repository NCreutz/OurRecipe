namespace OurRecipesWeb.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using OurRecipesWeb.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Recipe Recipe)
        {
            await this._container.CreateItemAsync<Recipe>(Recipe, new PartitionKey(Recipe.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Recipe>(id, new PartitionKey(id));
        }

        public async Task<Recipe> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Recipe> response = await this._container.ReadItemAsync<Recipe>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Recipe>(new QueryDefinition(queryString));
            List<Recipe> results = new List<Recipe>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
        
        public async Task UpdateItemAsync(string id, Recipe Recipe)
        {
            await this._container.UpsertItemAsync<Recipe>(Recipe, new PartitionKey(id));
        }
    }
}
