using Newtonsoft.Json;

namespace OurRecipesWeb.Models
{
    public class Author
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}
